using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyParalaxe : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private float startPos;
    private float distance;

    private Vector3 SkyPos;

    [SerializeField] private float Scale;

    private void Start()
    {
        SkyPos = transform.position;
        startPos = player.transform.position.y;
    }

    void Update()
    {
        distance = player.transform.position.y - startPos;

        transform.position = SkyPos + new Vector3(0, distance * Scale, 0);
    }
}
