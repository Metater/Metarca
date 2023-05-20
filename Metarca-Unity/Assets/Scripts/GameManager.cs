using UnityEngine;
using Metarca.Shared;
using LiteNetLib;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Application.runInBackground = true;
    }
}
