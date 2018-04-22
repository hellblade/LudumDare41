using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingGenerator : MonoBehaviour
{
    [SerializeField] GeneratorManager generatorManager;
    [SerializeField] Transform startingBlock;
    GeneratorManager manager;

    private void Awake()
    {
        manager = GetComponent<GeneratorManager>();
    }

    public void Generate()
    {
        for (int i = -manager.ScreenAmountX; i < manager.ScreenAmountX * 2; i++)
        {
            Instantiate(startingBlock, new Vector3(i, 0, 0), Quaternion.identity);
        }
        generatorManager.LastBlockX = manager.ScreenAmountX * 2 - 1;
    }
}