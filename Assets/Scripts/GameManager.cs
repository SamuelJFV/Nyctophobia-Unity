using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public struct Checkpoint
{
    public Vector3 playerPosition;
    public Vector3 cameraPosition;
}
public class GameManager : MonoBehaviour
{
    public static bool pausedGame;
    [SerializeField] Checkpoint[] checkpoints;
    public static int checkpointId = 1;
    public int timer;
    [SerializeField] bool timerIsRunning;
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;
    [SerializeField] SpriteRenderer playerSprite;
    PlayerController playerController;
    bool pressedSpace;

    #region Singleton
    public static GameManager Instance { get; private set; }
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
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerController = player.GetComponent<PlayerController>();
        pausedGame = true;
        FreezePlayer(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pressedSpace)
        {
            pressedSpace = true;
            LoadLastCheckpoint();
        }
    }
    void FixedUpdate()
    {
        if (timerIsRunning)
        {
            timer --;
        }
    }
    public void SetTimer(int time)
    {
        timer = time;
    }
    public void StartTimer(int time = 0)
    {
        SetTimer(time);
        timerIsRunning = true;
    }
    public void ResumeTimer()
    {
        timerIsRunning = true;
    }
    public void StopTimer()
    {
        timerIsRunning = false;
    }
    public void HidePlayer(bool value)
    {
        playerSprite.enabled = !value;
    }
    public void FreezePlayer(bool value, bool changeControllerEnabled = true)
    {
        if (playerController.enabled)
        {
            playerController.Stop();
        }

        UiManager.Instance.SetMoveSprite(!value);
        pausedGame = value;
        if (changeControllerEnabled)
        {
            playerController.enabled = !value;
        }
    }

    public void SetPlayerJumpCrouch(bool value)
    {
        playerController.canJumpCrouch = value;
        playerController.isGrounded = true;
    }

    public void LoadLastCheckpoint()
    {
        player.transform.position = checkpoints[checkpointId].playerPosition;
        mainCamera.transform.position = checkpoints[checkpointId].cameraPosition;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        checkpointId = 1;
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        FreezePlayer(false);
        StopTimer();
    }
}
