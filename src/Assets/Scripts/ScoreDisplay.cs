using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text label;

    RunnerManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<RunnerManager>();
        label = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        label.text = gameManager.GetScore().ToString();
    }
}