using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    [SerializeField] float maxSpeed = 20;

    public Vector3 CurrentMoveSpeed { get; private set; }

    public float Difficulty { get; private set; }

    float distanceTravelled = 0.0f;


    float lastDistance = 0;

    private void Awake()
    {
        if (CurrentMoveSpeed == Vector3.zero)
        {
            CurrentMoveSpeed = new Vector3(5, 0, 0);
        }

        Difficulty = 10;
    }

    private void FixedUpdate()
    {
        distanceTravelled += CurrentMoveSpeed.x * Time.deltaTime;

        if (distanceTravelled - lastDistance > 100)
        {
            Difficulty++;
            lastDistance = distanceTravelled;
        }
    }

}