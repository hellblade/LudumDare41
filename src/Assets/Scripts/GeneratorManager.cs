using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [SerializeField] RunnerManager gameManager;

    private Vector2 resolution;

    public int ScreenAmountX { get; private set; }
    public int ScreenAmountY { get; private set; }

    public float LastBlockX { get; set; }


    StartingGenerator startingGenerator;


    // Use this for initialization
    void Awake()
    {
        handleResolutionChanges();

        startingGenerator = GetComponent<StartingGenerator>();
        startingGenerator.Generate();
    }

    private void FixedUpdate()
    {
        LastBlockX -= gameManager.CurrentMoveSpeed.x * Time.deltaTime;
    }

    void handleResolutionChanges()
    {
        if (resolution.x == Screen.width || resolution.y == Screen.height)
            return;

        resolution.x = Screen.width;
        resolution.y = Screen.height;

        var height = 2 * Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;

        ScreenAmountX = Mathf.CeilToInt(width);
        ScreenAmountY = Mathf.CeilToInt(height);
    }

}