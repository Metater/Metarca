using Metarca.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UdpNetManager netManager;

    private void Awake()
    {
        netManager = new UdpNetManager(7777);
        netManager.OnReceivedUdpPacket += OnReceivedUdpPacket;
    }

    private void Update()
    {

    }

    private void OnReceivedUdpPacket()
    {
        switch (receivedPacket)
        {
            case TestPacket packet:
                print(packet.Test);
                break;
        }
    }
}
