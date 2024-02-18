using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EyesMenu : MonoBehaviour
{
    [SerializeField] GameObject eyePrefab;
    [SerializeField] GameObject[] eyes;
    int totalTime;
    int timeInterval;
    float width = 1600.00f;
    float height = 800.00f;
    int index;
    void Start()
    {
        index = 0;
        totalTime = 200;
        timeInterval = totalTime / (eyes.Length - 1);
        GameManager.Instance.StartTimer(totalTime);
    }

    void FixedUpdate()
    {
        if (index == eyes.Length)
        {
            GameManager.Instance.SetTimer(totalTime);
            index = 0;
        }
        
        if (GameManager.Instance.timer % timeInterval == 0)
        {
            SetActiveEye(true, index);

            SetActiveEye(false, index + 2);

            index ++;

            PlayRandomSound();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
        }
    }
    
    void PlayRandomSound()
    {
        // 1/10 chance of playing sound
        int randomInt = Random.Range(0, 9);

        if (randomInt == 0)
        {
            AudioManager.Instance.PlaySound(Random.Range(2,8));
        }
    }

    void SetActiveEye(bool value, int index)
    {
        if (index >= eyes.Length)
        {
            index -= eyes.Length;
        }
        ChangePosition(eyes[index]);
        eyes[index].SetActive(value);
    }

    void ChangePosition(GameObject eye)
    {
        Vector3 eyePosition = new Vector3(Random.Range( - width / 2.00f, width / 2.00f), Random.Range( - height / 2.00f, height / 2.00f), 0.00f);
        eye.GetComponent<RectTransform>().anchoredPosition = eyePosition;
    }
}
