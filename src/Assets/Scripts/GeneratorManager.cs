using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneratorManager : MonoBehaviour
{
    [SerializeField] RunnerManager gameManager;

    private Vector2 resolution;

    public int ScreenAmountX { get; private set; }
    public int ScreenAmountY { get; private set; }

    public float LastBlockX { get; set; }

    public UnityEvent ResolutionChanged = new UnityEvent();


    StartingGenerator startingGenerator;
    PlayerController player;


    // Use this for initialization
    void Awake()
    {
        HandleResolutionChanges();

        startingGenerator = GetComponent<StartingGenerator>();
        player = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        LastBlockX -= gameManager.CurrentMoveSpeed.x * Time.deltaTime;
        HandleResolutionChanges();
    }

    void HandleResolutionChanges()
    {
        if (resolution.x == Screen.width || resolution.y == Screen.height)
            return;

        resolution.x = Screen.width;
        resolution.y = Screen.height;

        var height = 2 * Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;

        ScreenAmountX = Mathf.CeilToInt(width);
        ScreenAmountY = Mathf.CeilToInt(height);

        var player = FindObjectOfType<PlayerController>();
        player.SetXPosition(-ScreenAmountX / 2 + 5);

        ResolutionChanged.Invoke();
    }

    public void StartGame()
    {
        startingGenerator.Generate();
        
        player.gameObject.SetActive(true);
        player.Reset();
        player.SetXPosition(-ScreenAmountX / 2 + 5);
    }

}