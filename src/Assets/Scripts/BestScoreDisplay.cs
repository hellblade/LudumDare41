using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreDisplay : MonoBehaviour
{
    Text label;
    string text;

    RunnerManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<RunnerManager>();   

        label = GetComponent<Text>();
        text = label.text;
    }

    void Start()
    {
        label.text = text + gameManager.BestScore.ToString();
    }

    public void OnGameEnded()
    {
        label.enabled = true;
        label.text = text + gameManager.BestScore.ToString();
    }

    public void OnGameStarted()
    {
        label.enabled = false;
    }
}