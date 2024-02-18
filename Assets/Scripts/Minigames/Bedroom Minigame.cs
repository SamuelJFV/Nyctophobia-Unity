using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomMinigame : MonoBehaviour
{
    [SerializeField] Animator[] animators;
    [SerializeField] GameObject darkness;
    [SerializeField] GameObject monsterTeddy;
    [SerializeField] GameObject teddy;
    [SerializeField] GameObject skipUi;
    [SerializeField] Vector3 darknessPosition;
    bool cutscenePlaying;
    
    void Update()
    {
        if (GameManager.Instance.timer < 0 && cutscenePlaying)
        {
            GameManager.Instance.StartGame();
            StartMinigame();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.checkpointId = 1;
            StartCutscene();
        }
    }
    public void StartCutscene()
    {
        cutscenePlaying = true;
        GameManager.Instance.StartTimer(850);

        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].SetTrigger("Start");
        }
        darkness.SetActive(true);
    }

    public void SkipCutscene()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].speed = 100000;
        }
        darkness.transform.position = darknessPosition;
        StartMinigame();
    }

    public void StartMinigame()
    {
        cutscenePlaying = false;
        teddy.SetActive(false);
        monsterTeddy.SetActive(true);

        Destroy(this);
    }
}
