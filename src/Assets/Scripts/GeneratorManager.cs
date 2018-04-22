using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{

    private Vector2 resolution;

    public int ScreenAmountX { get; private set; }
    public int ScreenAmountY { get; private set; }


    StartingGenerator startingGenerator;


    // Use this for initialization
    void Start()
    {
        handleResolutionChanges();

        startingGenerator = GetComponent<StartingGenerator>();
        startingGenerator.Generate();
    }

    // Update is called once per frame
    void Update()
    {
       

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