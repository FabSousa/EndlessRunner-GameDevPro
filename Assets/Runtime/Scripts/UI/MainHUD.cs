using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MainHUDAudioController))]
public class MainHUD : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;

    [Header("Overlays")]
    [SerializeField] private GameObject startGameOverlay;
    [SerializeField] private GameObject hudOverlay;
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject countdownOverlay;
    [SerializeField] private GameObject settingsOverlay;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI cherryCountText;
    [SerializeField] private TextMeshProUGUI peanutCountText;
    [SerializeField] private TextMeshProUGUI totalCherries;
    [SerializeField] private TextMeshProUGUI totalPeanuts;
    [SerializeField] private TextMeshProUGUI lastScore;
    [SerializeField] private TextMeshProUGUI highScore;

    private MainHUDAudioController audioController;

    private void Awake()
    {
        audioController = GetComponent<MainHUDAudioController>();
    }

    private void LateUpdate()
    {
        scoreText.text = $"Score : {gameMode.Score}";
        distanceText.text = $"{Mathf.RoundToInt(gameMode.TravelledDistance)}m";
        cherryCountText.text = GameMode.CherryCount.ToString();
        peanutCountText.text = GameMode.PeanutCount.ToString();
        totalCherries.text = PlayerPrefs.GetInt(GameConsts.Cherries).ToString();
        totalPeanuts.text = PlayerPrefs.GetInt(GameConsts.Peanuts).ToString();
        lastScore.text = PlayerPrefs.GetInt(GameConsts.LastScore).ToString();
        highScore.text = PlayerPrefs.GetInt(GameConsts.HighScore).ToString();
    }

    public void StartGame()
    {
        gameMode.StartGame();
    }

    public void PauseGame()
    {
        ShowPauseOverlay();
        gameMode.PauseGame();
    }

    public void ResumeGame()
    {
        gameMode.ResumeGame();
        ShowHudOverlay();
    }

    public void ShowStartGameOverlay()
    {
        HideAllOverlays();
        startGameOverlay.SetActive(true);
    }

    public void ShowHudOverlay()
    {
        HideAllOverlays();
        hudOverlay.SetActive(true);
    }

    public void ShowPauseOverlay()
    {
        HideAllOverlays();
        pauseOverlay.SetActive(true);
    }

    public void ShowMainMenuOverlay()
    {
        HideAllOverlays();
        settingsOverlay.SetActive(true);
    }

    private void ShowCountdownOverlay()
    {
        HideAllOverlays();
        countdownOverlay.SetActive(true);
    }

    private void HideAllOverlays()
    {
        startGameOverlay.SetActive(false);
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(false);
        countdownOverlay.SetActive(false);
        settingsOverlay.SetActive(false);
    }

    public IEnumerator PlayStartGameCountdown(int countdownSeconds)
    {
        ShowCountdownOverlay();
        countdownText.gameObject.SetActive(false);

        if (countdownSeconds == 0)
        {
            yield break;
        }

        float timeToStart = Time.time + countdownSeconds;
        yield return null;
        countdownText.gameObject.SetActive(true);
        int previousRemainingTimeInt = countdownSeconds;
        while (Time.time <= timeToStart)
        {
            float remainingTime = timeToStart - Time.time;
            int remainingTimeInt = Mathf.FloorToInt(remainingTime);
            countdownText.text = (remainingTimeInt + 1).ToString();

            if (previousRemainingTimeInt != remainingTimeInt)
            {
                audioController.PlayCountdownAudio();
            }

            previousRemainingTimeInt = remainingTimeInt;

            float percent = remainingTime - remainingTimeInt;
            countdownText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, percent);
            yield return null;
        }

        audioController.PlayCountdownFinishedAudio();

        countdownText.gameObject.SetActive(false);

        ShowHudOverlay();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
