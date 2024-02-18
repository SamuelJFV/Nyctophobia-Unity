using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum HandDirection
{
    Up = 0,
    Down = 1,
    Diagonal = 2,
}

public class HallwayMinigame : MonoBehaviour
{
    [SerializeField] HallwayMonsterController monster;
    [SerializeField] GameObject clawPrefab;
    [SerializeField] GameObject spikePrefab;
    [SerializeField] GameObject bubblingPrefab;
    [SerializeField] int totalHands;
    [SerializeField] float maxSpacing;
    [SerializeField] float height;
    [SerializeField] int cutsceneTime;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Vector3 armPosition;
    [SerializeField] Quaternion armRotation;
    float spacing;
    bool playedCutscene;
    bool playerPresence;
    GameObject arm;
    GameObject bubbling;

    void OnDrawGizmos()
    {
        for (int i = 0; i < totalHands; i++)
        {
            Gizmos.color = new Color(1.00f, 1.00f, 0.00f, 0.5f);
            float s = i * maxSpacing / (1.00f + (float) i / totalHands);
            Gizmos.DrawCube(transform.position + spawnPosition + new Vector3( - s, height, 0.00f), 0.5f * Vector3.one);
            Gizmos.DrawCube(transform.position + spawnPosition + new Vector3( - s, 0.00f, 0.00f), 0.5f * Vector3.one);
        }
    }
    void Start()
    {
        SpawnHands();
    }

    void Update()
    {
        if (!playedCutscene && playerPresence && GameManager.Instance.timer < 0)
        {
            StopCutscene();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playedCutscene)
        {
            GameManager.checkpointId = 4;
            StartCutscene();
            monster.SpawnMonster();
        }
    }
    void SpawnHands()
    {
        HandDirection[] handDirections = (HandDirection[])System.Enum.GetValues(typeof(HandDirection));
        
        int index = 0;

        for (int i = 0; i < totalHands; i++)
        {
            spacing = maxSpacing / (1.00f + (float) i / totalHands);
            SpwanArm(i, handDirections[index], spacing);
            index = RandomIndex(handDirections.Length);
        }
    }

    int RandomIndex(int maxInt)
    {
        int randomInt =  Random.Range(0, maxInt + 1);

        if (randomInt >= maxInt)
        {
            return 0;
        }
        return randomInt;
    }
    void SpwanArm(int handIndex, HandDirection direction, float spacing)
    {
        switch (direction)
        {
            case HandDirection.Up:
                armPosition = transform.position + spawnPosition + new Vector3( - handIndex * spacing, 0.00f, 0.00f);
                armRotation = Quaternion.Euler(Vector3.zero);
                arm = Instantiate(clawPrefab, transform);

                bubbling = Instantiate(bubblingPrefab, transform);
                bubbling.transform.SetPositionAndRotation(armPosition + Vector3.up * 4.00f, armRotation);
                break;

            case HandDirection.Down:
                armPosition = transform.position + spawnPosition + new Vector3( - handIndex * spacing, height, 0.00f);
                armRotation = Quaternion.Euler(180.00f * Vector3.forward);
                arm = Instantiate(clawPrefab, transform);

                bubbling = Instantiate(bubblingPrefab, transform);
                bubbling.transform.SetPositionAndRotation(armPosition, armRotation);
                break;

            case HandDirection.Diagonal:
                armPosition = transform.position + spawnPosition + new Vector3( - handIndex * spacing, height + 3.00f, 0.00f);
                armRotation = Quaternion.Euler(Random.Range(170.00f, 190.00f) * Vector3.forward);
                arm = Instantiate(spikePrefab, transform);
                break;

            default:
                break;   
        }
        
        arm.transform.SetPositionAndRotation(armPosition, armRotation);
    }

    void StartCutscene()
    {
        playerPresence = true;
        GameManager.Instance.StartTimer(cutsceneTime);
        GameManager.Instance.FreezePlayer(true);
    }
    void StopCutscene()
    {
        GameManager.Instance.FreezePlayer(false);
        GameManager.Instance.StopTimer();
        playedCutscene = true;
        monster.StartChasing();
    }
}