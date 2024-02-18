using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    bool pressedSpace;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pressedSpace)
        {
            pressedSpace = true;
            GameManager.Instance.LoadLastCheckpoint();
        }
    }
}
