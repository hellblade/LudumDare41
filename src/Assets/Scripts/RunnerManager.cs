using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunnerManager : MonoBehaviour
{
    const string BestScorePref = "BestScore";
    const string GameNumberPref = "GamesPlayed";

    [SerializeField] float maxSpeed = 20;
    [SerializeField] float initialSpeed = 5;
    [SerializeField] float speedIncrease = 1.1f;
    [SerializeField] float distanceBeweenDifficulties = 100;
    [SerializeField] float difficultySpeedUp = 0.9f;
    [SerializeField] UnityEvent gameStarted = new UnityEvent();
    [SerializeField] UnityEvent gameEnded = new UnityEvent();
    GeneratorManager generator;

    public Vector3 CurrentMoveSpeed { get; private set; }

    public float Difficulty { get; private set; }

    public int BestScore { get; private set; }

    public bool IsGameRunning { get; private set; }

    public UnityEvent GameStarted { get { return gameStarted; } }

    public UnityEvent GameEnded { get { return gameEnded; } }

    public int GamesPlayed { get; private set; }

    float distanceTravelled = 0.0f;
    float lastDistance = 0;

    float currentSpeed;
    float nextSpeed;
    float nextDifficulty;

    private void Awake()
    {
        BestScore = PlayerPrefs.GetInt(BestScorePref, 0);
        GamesPlayed = PlayerPrefs.GetInt(GameNumberPref, 0);
        generator = FindObjectOfType<GeneratorManager>();     
    }

    void Start()
    {
        StartGame();
        EndGame();
    }

    void Update()
    {
        if (IsGameRunning)
            return;

        if (Input.GetButtonDown("Jump"))
        {
            StartGame();
        }
    }

    private void FixedUpdate()
    {
        if (!IsGameRunning)
            return;

        distanceTravelled += CurrentMoveSpeed.x * Time.deltaTime;

        float distanceSinceLast = distanceTravelled - lastDistance;

        if (distanceSinceLast >= nextDifficulty)
        {
            Difficulty += 1f;
            lastDistance = distanceTravelled;

            nextDifficulty *= difficultySpeedUp;

            currentSpeed = nextSpeed;
            nextSpeed *= speedIncrease;
        }

        float percentToNext = distanceSinceLast / nextDifficulty;
        CurrentMoveSpeed = new Vector3(Mathf.Lerp(currentSpeed, nextSpeed, percentToNext * percentToNext), 0, 0);
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(distanceTravelled);
    }

    public void StartGame()
    {
        currentSpeed = initialSpeed;
        nextSpeed = initialSpeed * speedIncrease;
        distanceTravelled = 0;
        CurrentMoveSpeed = new Vector3(initialSpeed, 0, 0);

        Difficulty = 1;
        nextDifficulty = distanceBeweenDifficulties;

        generator.StartGame();
        IsGameRunning = true;

        if (gameStarted != null)
        {
            gameStarted.Invoke();
        }
    }

    public void EndGame()
    {
        IsGameRunning = false;
        CurrentMoveSpeed = Vector3.zero;
        GamesPlayed++;

        var player = FindObjectOfType<PlayerController>();
        player.gameObject.SetActive(false);

        var score = GetScore();

        if (score > BestScore)
        {
            BestScore = score;
            PlayerPrefs.SetInt(BestScorePref, BestScore);
        }
        PlayerPrefs.SetInt(GameNumberPref, GamesPlayed);
        PlayerPrefs.Save();

        if (gameEnded != null)
        {
            gameEnded.Invoke();
        }
    }

}