using Metarca.Shared;
using Metarca.Shared.Packets;
using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private SnapshotInterpolationSettings snapshotSettings;
    private readonly SortedList<double, TimeSnapshot> snapshots = new();
    private double remoteTimeStamp = 0;
    private double localTimeline = 0;
    private double localTimescale = 1;
    private ExponentialMovingAverage driftEma;
    private ExponentialMovingAverage deliveryTimeEma;

    public double RemoteTimeStamp => remoteTimeStamp;
    public double LocalTimeline => localTimeline;
    private double BufferTime => Constants.SecondsPerTick * snapshotSettings.bufferTimeMultiplier;

    private void Awake()
    {
        ClientManager.packetProcessor.SubscribeReusable<TimePacket>(OnTimePacket);

        driftEma = new(Constants.TicksPerSecond * snapshotSettings.driftEmaDuration);
        deliveryTimeEma = new ExponentialMovingAverage(Constants.TicksPerSecond * snapshotSettings.deliveryTimeEmaDuration);
    }

    private void Update()
    {
        if (ClientManager.NetManager.FirstPeer == null)
        {
            snapshots.Clear();
            remoteTimeStamp = 0;
            localTimeline = 0;
            localTimescale = 1;
            return;
        }

        if (snapshots.Count > 0)
        {
            SnapshotInterpolation.StepTime(Time.unscaledDeltaTime, ref localTimeline, localTimescale);
            SnapshotInterpolation.StepInterpolation(snapshots, localTimeline, out _, out _, out _);
        }
    }

    private void OnTimePacket(TimePacket packet)
    {
        remoteTimeStamp = packet.Time;
        TimeSnapshot snapshot = new(remoteTimeStamp, Time.unscaledTimeAsDouble);

        if (snapshotSettings.dynamicAdjustment)
        {
            snapshotSettings.bufferTimeMultiplier = SnapshotInterpolation.DynamicAdjustment(
                Constants.SecondsPerTick,
                deliveryTimeEma.StandardDeviation,
                snapshotSettings.dynamicAdjustmentTolerance
            );
        }

        SnapshotInterpolation.InsertAndAdjust(
            snapshots,
            snapshot,
            ref localTimeline,
            ref localTimescale,
            Constants.SecondsPerTick,
            BufferTime,
            snapshotSettings.catchupSpeed,
            snapshotSettings.slowdownSpeed,
            ref driftEma,
            snapshotSettings.catchupNegativeThreshold,
            snapshotSettings.catchupPositiveThreshold,
            ref deliveryTimeEma
        );
    }
}
