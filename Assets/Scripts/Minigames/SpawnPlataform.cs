using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnPlataform : MonoBehaviour
{
    [Header("External Components")]
    [SerializeField] private GameObject player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] GameObject dirtyWater;
    [SerializeField] Animator lightAnimator;

    [Space(30)]

    [Header("Spawn Margins")]
    [SerializeField] private float triggerMargin;                       //Size of area to trigger the Platforms to Spwan 
    [SerializeField] private float[] corredorMargins = new float[2];    //Size of the corredor available to Spawn the Platforms

    [Space(30)]

    [Header ("Plataform setting")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int prefabToSpawn;                         //Numbers of Plataforms you wanna Spawn
    private float prefabCount = 0;                                      //Number of Plataforms Spawned
    private int spawnTime = 2;                                          //Wait 1/25 seconds between Spawns
    private int initialSpawnTime = 300;                                 //Wait 6 seconds before Spawns
    [SerializeField] private float randomOfset;                         //Random ofset between Plataforms     
    bool swpaningPlatforms = false;

    [Header ("Animation")]
    [SerializeField] Animator changeBackground;
    [SerializeField] Animator moveWall;

    //Draw the Margins
    void OnDrawGizmos()
    {
        //Set Trigger Points
        Vector3 topLeft = transform.position + new Vector3(-triggerMargin, 1 , 0);
        Vector3 topRight = transform.position + new Vector3(triggerMargin, 1 , 0);
        Vector3 bottomRight = transform.position + new Vector3(triggerMargin, -1 , 0);
        Vector3 bottomLeft = transform.position + new Vector3(-triggerMargin, -1 , 0);


        //Draw them in Red
        Gizmos.color = Color.red;

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);






        //Set Spawning Margins
        topLeft = new Vector3(corredorMargins[0], transform.position.y + 1, 0);
        topRight = new Vector3(corredorMargins[1], transform.position.y + 1, 0);
        bottomRight = new Vector3(corredorMargins[1], transform.position.y -1, 0);
        bottomLeft = new Vector3(corredorMargins[0], transform.position.y -1, 0);


        //Draw them in Blue
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);



        for (int i = 0; i < prefabToSpawn; i++)
        {
            float corredorSize = corredorMargins[1] - corredorMargins[0];
            float drawingXPosition = corredorMargins[0] + (corredorSize / prefabToSpawn) * i;

            topLeft = new Vector3(drawingXPosition - randomOfset - 1, transform.position.y + 1, 0);
            topRight = new Vector3(drawingXPosition + randomOfset + 1, transform.position.y + 1, 0);
            bottomRight = new Vector3(drawingXPosition + randomOfset + 1, transform.position.y - 1, 0);
            bottomLeft = new Vector3(drawingXPosition - randomOfset - 1, transform.position.y - 1, 0);


            //Draw them in Green
            Gizmos.color = Color.green;

            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }

    }

    private void Update()
    {
        //Run if inside Trigger Margin
        if (!swpaningPlatforms && Mathf.Abs(transform.position.x - player.transform.position.x) < triggerMargin)
        {
            //Spawn after Timmer
            GameManager.checkpointId = 2;
            GameManager.Instance.StartTimer(initialSpawnTime);
            GameManager.Instance.FreezePlayer(true);
            StartAnimation();
            swpaningPlatforms = true;
            dirtyWater.SetActive(true);
        }
        else if (swpaningPlatforms && GameManager.Instance.timer < 0)
        {
            SpawnPlatform();
        }
        else if (swpaningPlatforms && GameManager.Instance.timer < 250)
        {
            FlickeringLight();
        }
    }

    void StartAnimation()
    {
        changeBackground.enabled = true;
        moveWall.enabled = true;
    }

    void SpawnPlatform()
    {
        //Calculate Spawn Position
        float corredorSize = corredorMargins[1] - corredorMargins[0];
        float prefabSpacing = corredorSize / prefabToSpawn;

        Vector3 prefabPosition = transform.position;

        float prefabXPosition = prefabSpacing * prefabCount + Random.Range(-randomOfset, randomOfset);    //Calculate x position with number of Plataform
        
        prefabPosition.x = corredorMargins[0] + prefabXPosition;

        //Spawn Plataforms but skip the one to close to the Player
        if (Mathf.Abs(prefabPosition.x - transform.position.x) > triggerMargin)
        {
            Instantiate(prefab, prefabPosition, transform.rotation, transform);
            GameManager.Instance.SetTimer(spawnTime);
        }

        prefabCount++;


        if (prefabCount == prefabToSpawn)
        {
            GameManager.Instance.FreezePlayer(false);
            GameManager.Instance.StopTimer();
            moveWall.gameObject.SetActive(false);
            Destroy(this);
        }
    }

    void FlickeringLight()
    {
        lightAnimator.enabled = true;
    }
}
