using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnsyncHands : MonoBehaviour
{

    [SerializeField] private GameObject[] hands;

    [SerializeField] private float timmer = 0;
    [SerializeField] private int handNumber = 0;

    private void Start()
    {
        timmer = Random.Range(0.2f, 1.4f);
    }

    void Update()
    {
        if (timmer <= 0)
        {
            timmer = Random.Range(0.2f, 1.4f);
            if (handNumber < hands.Length)
            {
                hands[handNumber].SetActive(true);
                handNumber++;
            }
            else Destroy(this);
        }
        else
        {
            timmer -= Time.deltaTime;
        }
    }
}
