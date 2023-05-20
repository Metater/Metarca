using Metarca.Shared;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEntityHandler : MonoBehaviour
{
    private TimeManager timeManager;
    private readonly SortedList<double, TransformSnapshot> snapshots = new();

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }

    private void Update()
    {
        if (snapshots.Count == 0) return;

        SnapshotInterpolation.StepInterpolation(
            snapshots,
            timeManager.LocalTimeline,
            out var from,
            out var to,
            out var t
        );

        transform.localPosition = TransformSnapshot.Interpolate(from, to, t).position;
    }

    public void AddSnapshot(Vector2 position)
    {
        SnapshotInterpolation.InsertIfNotExists(snapshots, new TransformSnapshot(
            timeManager.RemoteTimeStamp + (Constants.SecondsPerTick * (Constants.TicksPerSecondMultiplier - 1)),
            Time.unscaledTimeAsDouble,
            position,
            Quaternion.identity,
            Vector3.one
        ));
    }
}
