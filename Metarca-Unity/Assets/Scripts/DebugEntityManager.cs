using LiteNetLib;
using Metarca.Shared;
using System.Collections.Generic;
using UnityEngine;

public class DebugEntityManager : MonoBehaviour
{
    [SerializeField] private DebugEntityHandler entityPrefab;
    [SerializeField] private Transform entitiesTransform;
    private readonly Dictionary<byte, DebugEntityHandler> entities = new();

    private void Awake()
    {
        ClientManager.packetProcessor.RegisterNestedType<DebugEntity>();
        ClientManager.packetProcessor.SubscribeReusable<DebugEntityPacket>(OnDebugEntityPacket);

        ClientManager.PeerDisconnectedEvent += DespawnAll;
    }

    private void OnDebugEntityPacket(DebugEntityPacket packet)
    {
        foreach (var debugEntity in packet.Entities)
        {
            var id = debugEntity.Id;
            var position = new Vector2(debugEntity.X, debugEntity.Y);

            if (!entities.TryGetValue(id, out var entity))
            {
                entity = Instantiate(entityPrefab, position, Quaternion.identity, entitiesTransform);
                entities[id] = entity;
            }

            entity.AddSnapshot(position);
        }
    }

    private void DespawnAll(DisconnectInfo _)
    {
        foreach (var entity in entities.Values)
        {
            Destroy(entity.gameObject);
        }

        entities.Clear();
    }
}
