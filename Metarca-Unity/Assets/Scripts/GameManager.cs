using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Application.runInBackground = true;
    }
}
