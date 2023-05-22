using LiteNetLib;
using Metarca.Shared;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private double lastInputSendTime = 0;

    private void Awake()
    {
        ClientManager.packetProcessor.RegisterNestedType<InputPacket.Data>();
    }

    private void Update()
    {
        if (!ClientManager.IsConnected)
        {
            return;
        }

        if (AccurateInterval.Elapsed(Time.unscaledTimeAsDouble, Constants.SecondsPerTick, ref lastInputSendTime))
        {
            InputPacket.WASD wasd = InputPacket.WASD.None;

            if (Input.GetKeyDown(KeyCode.W))
            {
                wasd |= InputPacket.WASD.W;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                wasd |= InputPacket.WASD.A;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                wasd |= InputPacket.WASD.S;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                wasd |= InputPacket.WASD.D;
            }


            InputPacket.Data data = new()
            {
                WASD = wasd
            };

            ClientManager.SendPacket(new InputPacket
            {
                Input = data
            }, DeliveryMethod.Unreliable);
        }
    }
}
