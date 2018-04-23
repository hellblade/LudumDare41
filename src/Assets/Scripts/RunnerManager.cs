using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    [SerializeField] float maxSpeed = 20;
    [SerializeField] float initialSpeed = 5;
    [SerializeField] float speedIncrease = 1.1f;
    [SerializeField] float distanceBeweenDifficulties = 100;
    [SerializeField] float difficultySpeedUp = 0.9f;

    public Vector3 CurrentMoveSpeed { get; private set; }

    public float Difficulty { get; private set; }

    float distanceTravelled = 0.0f;
    float lastDistance = 0;

    float currentSpeed;
    float nextSpeed;
    float nextDifficulty;

    private void Awake()
    {
        StartGame();
    }

    private void FixedUpdate()
    {
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
        CurrentMoveSpeed = new Vector3(initialSpeed, 0, 0);

        Difficulty = 1;
        nextDifficulty = distanceBeweenDifficulties;
    }

}