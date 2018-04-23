using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayEndedOnly : MonoBehaviour
{
    Text label;

    RunnerManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<RunnerManager>();
        gameManager.GameStarted.AddListener(OnGameStarted);
        gameManager.GameEnded.AddListener(OnGameEnded);

        label = GetComponent<Text>();
    }


    public void OnGameEnded()
    {
        label.enabled = true;
    }

    public void OnGameStarted()
    {
        label.enabled = false;
    }
}