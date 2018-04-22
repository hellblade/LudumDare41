using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGenerator : MonoBehaviour
{
    [SerializeField] RunnerObject block;

    [SerializeField] RunnerManager gameManager;
    [SerializeField] GeneratorManager generatorManager;

    [SerializeField] int maxDropsInRow = 3;

    float currentLevel = 0;
    HashSet<int> holes = new HashSet<int>();
    HashSet<int> rises = new HashSet<int>();


    private void Awake()
    {

    }

    private void Start()
    {
        StartCoroutine(GeneratorLoop());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator GeneratorLoop()
    {
        while (true)
        {
            // Generate if we are running low
            if (generatorManager.LastBlockX < generatorManager.ScreenAmountX * 3)
            {
                GenerateNextBlock();
            }

            yield return null;
        }
    }

    bool canHaveDrop(int location, int amountToGenerate)
    {
        int rangeStart = Mathf.Max(0, location - maxDropsInRow);
        int rangeEnd = Mathf.Min(amountToGenerate - 1, location + maxDropsInRow);

        for (int range = rangeStart; range < rangeEnd - maxDropsInRow; range++)
        {
            int count = 0;
            for (int i = 0; i < maxDropsInRow; i++)
            {
                if (holes.Contains(range + i))
                {
                    count++;
                }
            }

            if (count == maxDropsInRow)
            {
                return false;
            }
        }

        return true;
    }

    void GenerateNextBlock()
    {
        holes.Clear();
        rises.Clear();

        int amountToGenerate = (int)(generatorManager.ScreenAmountX * gameManager.Difficulty);

        int numberOfHoles = (int)(gameManager.Difficulty * 2);

        while (numberOfHoles > 0)
        {
            var index = Random.Range(0, amountToGenerate);

            if (canHaveDrop(index, amountToGenerate))
            {
                numberOfHoles--;
                holes.Add(index);
            }
        }


        // Generate
        for (int i = 0; i < amountToGenerate; i++)
        {
            var xPosition = generatorManager.LastBlockX;
            generatorManager.LastBlockX = xPosition + 1;

            if (holes.Contains(i))
                continue;

            GameObject nextBlock = null;

            if (!RunnerObject.Pool.TryGet(ref nextBlock))
            {
                nextBlock = Instantiate(block).gameObject;
            }

           
            nextBlock.transform.position = new Vector3(xPosition, currentLevel, 0);
            nextBlock.SetActive(true);           
        }
    }
}