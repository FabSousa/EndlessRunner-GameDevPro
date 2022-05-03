using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerController player;
    [SerializeField] PlayerAnimationController playerAnimationController;

    [SerializeField] private MainHUD mainHud;

    [SerializeField] private MusicPlayer musicPlayer;

    [SerializeField] private Animator animator;



    [Header("Variables")]
    [SerializeField] private float reloadGameDelay = 3;

    [SerializeField]
    [Range(0, 5)]
    private int countdownSeconds = 5;

    [SerializeField]
    [Min(10)]
    private float minSpeed = 10;
    [SerializeField]
    [Min(10)]
    private float maxSpeed = 100;
    [SerializeField]
    [Min(0)]
    private float maxSpeedDelay = 10;

    public float Speed { get; private set; }

    [SerializeField] private float baseScoreMultiplier = 1;
    private float score;

    public float TravelledDistance => player.transform.position.z - player.InitialPosition.z;

    private float startGameTime;

    private bool isGameRunning = false;

    public int Score => Mathf.RoundToInt(score);

    public static int CherryCount { get; set; } = 0;

    private void Awake()
    {
        SetWaitForStartGameState();
    }

    private void Update()
    {
        if (isGameRunning)
        {
            float timePercent = (Time.time - startGameTime) / maxSpeedDelay;
            Speed = Mathf.Lerp(minSpeed, maxSpeed, timePercent);

            score += baseScoreMultiplier * Speed * Time.deltaTime;
        }
    }

    private void SetWaitForStartGameState()
    {
        player.enabled = false;
        mainHud.ShowStartGameOverlay();
        musicPlayer.PlayStartMenuMusic();
    }

    public void OnGameOver()
    {
        Speed = 0;
        isGameRunning = false;
        StartCoroutine(ReloadGameCoroutine());
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCor());
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private IEnumerator StartGameCor()
    {
        musicPlayer.PlayMainTrackMusic();
        yield return StartCoroutine(mainHud.PlayStartGameCountdown(countdownSeconds));
        yield return StartCoroutine(playerAnimationController.PlayStartGameAnimation());
        startGameTime = Time.time;
        isGameRunning = true;
        player.enabled = true;
        Speed = minSpeed;
    }

    private IEnumerator ReloadGameCoroutine()
    {
        yield return new WaitForSeconds(1);
        musicPlayer.PlayGameOverMusic();
        yield return new WaitForSeconds(reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CherryCount = 0;
    }
}
