using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene : MonoBehaviour
{
    [SerializeField] Animator backgroundAnimator;
    [SerializeField] Animator fadeAnimator;
    [SerializeField] RoomController parentsRoom;
    int animationTime = 625;
    int lightTime = 590;
    int fadeTime = 200;
    bool gameIsEnding;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.checkpointId = 1;
            GameManager.Instance.HidePlayer(true);
            gameIsEnding = true;
            StartCutscene();
        }
    }

    void FixedUpdate()
    {
        if (gameIsEnding)
        {
            if (GameManager.Instance.timer == lightTime)
            {
                parentsRoom.SetLight(1.00f);
                AudioManager.Instance.PlaySound(1);
            }
            else if (GameManager.Instance.timer == fadeTime)
            {
                fadeAnimator.enabled = true;
            }
            else if (GameManager.Instance.timer < 0)
            {
                GameManager.Instance.RestartGame();
            }
        }
    }

    void StartCutscene()
    {
        GameManager.Instance.StartTimer(animationTime + fadeTime);
        GameManager.Instance.FreezePlayer(true);
        backgroundAnimator.enabled = true;
    }
}
