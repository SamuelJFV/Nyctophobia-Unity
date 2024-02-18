using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject pressSpace;
    [SerializeField] GameObject skipUi;
    [SerializeField] GameObject moveUi;
    [SerializeField] GameObject cutsceneUi;
    [SerializeField] Collider2D bedroomCollider;
    [SerializeField] BedroomMinigame bedroomMinigame;
    bool spaceIsPressed;
    bool escIsPressed;

    #region Singleton
    public static UiManager Instance { get; private set; }
    private void Awake() 
    {        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    #endregion

    void Update()
    {
        UiHandler();
    }
    private void UiHandler()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Application.Quit();
            AudioManager.Instance.PlaySound(1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.RestartGame();
            AudioManager.Instance.PlaySound(1);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !spaceIsPressed)
        {
            SetupGameUi();
            GameManager.pausedGame = false;
            AudioManager.Instance.PlaySound(1);
            spaceIsPressed = true;
            
            if (GameManager.checkpointId != 1)
            {
                skipUi.SetActive(false);
                GameManager.Instance.StartGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !escIsPressed && !GameManager.pausedGame && GameManager.checkpointId == 1)
        {
            GameManager.Instance.StartGame();
            bedroomMinigame.SkipCutscene();
            skipUi.SetActive(false);
            escIsPressed = true;
        }
    }

    void SetupGameUi()
    {
        cutsceneUi.GetComponent<Image>().enabled = true;
        skipUi.SetActive(true);
        pressSpace.SetActive(false);

        bedroomCollider.enabled = true;
    }

    public void SetMoveSprite(bool value)
    {
        moveUi.SetActive(value);
        cutsceneUi.SetActive(!value);
    }
}
