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
        foreach(var runner in FindObjectsOfType<RunnerObject>())
        {
            runner.Remove();
        }

        for (int i = -manager.ScreenAmountX; i < manager.ScreenAmountX / 2; i++)
        {
            GameObject nextBlock = RunnerObject.GetRunnerObject(startingBlock.gameObject);
            nextBlock.transform.position = new Vector3(i, 0, 0);
            nextBlock.SetActive(true);
        }
        generatorManager.LastBlockX = manager.ScreenAmountX / 2 - 1;
    }
}