using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TvMinigame : MonoBehaviour
{
    Transform playerTransform;
    [SerializeField] TvMonsterController[] tvMonsterControllers;
    [SerializeField] GameObject tvOn;
    [SerializeField] GameObject tvOff;
    int totalTime = 800;
    int timeInterval = 100;
    int[] flickeringTimes;
    bool poweredTv = true;
    bool startHunting;
    bool playerPresence;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            
            GameManager.checkpointId = 3;
            GameManager.Instance.StartTimer(totalTime);
            InitializeFlickeringTimes();
            InitializeMonsters();
            playerPresence = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.StopTimer();
            SetActiveTv(true);
            StopMinigame();
        }
    }

    void Update()
    {
        if (playerPresence && GameManager.pausedGame)
        {
            SetActiveTv(false);
            StopMinigame();
        }
    }

    void FixedUpdate()
    {
        if (playerPresence)
        {
            for (int i = 0; i < flickeringTimes.Length; i++)
            {
                if (GameManager.Instance.timer == flickeringTimes[i])
                {
                    ToggleMonster();
                    ToggleTv();
                    return;
                }
            }
        }
    }

    void StopMinigame()
    {
        playerPresence = false;
        foreach (var monsterController in tvMonsterControllers)
        {
            monsterController.gameObject.SetActive(false);
        }
        Destroy(this);
    }
    void InitializeFlickeringTimes()
    {
        poweredTv = true;
        flickeringTimes = new int[totalTime / timeInterval];

        for (int i = 1; i < flickeringTimes.Length - 1; i++)
        {
            flickeringTimes[i] = i * timeInterval + Random.Range( - timeInterval / 2, timeInterval / 2);
        }
        flickeringTimes[^1] = totalTime - 25;
        SetActiveTv(true);
    }
    private void InitializeMonsters()
    {
        foreach (var monsterController in tvMonsterControllers)
        {
            monsterController.huntingTime = totalTime;
        }
        SetActiveMonster(false);
    }

    void ToggleTv()
    {
        poweredTv = !poweredTv;
        SetActiveTv(poweredTv);
    }

    void SetActiveTv(bool value)
    {
        poweredTv = value;
        tvOn.SetActive(value);
        tvOff.SetActive(!value);
    }

    void ToggleMonster()
    {
        startHunting = !startHunting;
        SetActiveMonster(startHunting);
    }

    void SetActiveMonster(bool hunting)
    {
        startHunting = hunting;

        foreach (var monsterController in tvMonsterControllers)
        {
            monsterController.OpenEyes(hunting);
            monsterController.playerPosition= playerTransform.position;
            monsterController.isHunting = hunting;
        }
    }
}
