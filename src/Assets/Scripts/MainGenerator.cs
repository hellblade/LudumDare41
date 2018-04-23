﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGenerator : MonoBehaviour
{
    [SerializeField] RunnerObject block;
    [SerializeField] PlanterBox plantingBox;

    [SerializeField] RunnerManager gameManager;
    [SerializeField] GeneratorManager generatorManager;

    [SerializeField] int maxDropsInRow = 2;

    [SerializeField] float maxLevel = 5;
    [SerializeField] float minLevel = -2;

    float currentLevel = 0;
    HashSet<int> holes = new HashSet<int>();
    HashSet<int> rises = new HashSet<int>();
    int lastDifficulty;

    void OnGameStart()
    {
        lastDifficulty = 1;
    }

    private void Awake()
    {
        gameManager.GameStarted.AddListener(OnGameStart);
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
            if (gameManager.IsGameRunning && generatorManager.LastBlockX < generatorManager.ScreenAmountX * 3)
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

    const int maxTries = 25;

    void GenerateNextBlock()
    {
        int currentIntDifficulty = (int)gameManager.Difficulty;

        if (lastDifficulty != currentIntDifficulty)
        {
            lastDifficulty = currentIntDifficulty;
            float change = Random.Range(-1, 2);

            if (Mathf.Abs(change) != 0)
            {
                change *= Random.Range(0.5f, 1.0f);
                currentLevel = Mathf.Clamp(currentLevel + change, minLevel, maxLevel);
            }
          
        }

        holes.Clear();
        rises.Clear();

        int amountToGenerate = (int)(generatorManager.ScreenAmountX * 2);

        int numberOfHoles = Random.Range(0, Mathf.Max(generatorManager.ScreenAmountX / 2, (int)(gameManager.Difficulty * 2)));

        int tries = 0;

        while (tries++ < maxTries && numberOfHoles > 0)
        {
            var index = Random.Range(0, amountToGenerate);

            if (canHaveDrop(index, amountToGenerate))
            {
                numberOfHoles--;
                holes.Add(index);
            }
        }

        int numberOfRises = Random.Range(0, Mathf.Max(generatorManager.ScreenAmountX, (int)(gameManager.Difficulty * 3)));
        tries = 0;

        while (tries++ < maxTries && numberOfRises > 0)
        {
            var index = Random.Range(0, amountToGenerate);

            if (!holes.Contains(index))
            {
                numberOfRises--;
                rises.Add(index);
            }
        }

        // Create seperate platform for them
        int plantIndex;
        if (gameManager.ShouldCreateNextPlantBox(out plantIndex))
        {
            var level = currentLevel + Random.Range(2, 3);

            var xPos = Random.Range(0, amountToGenerate) + generatorManager.LastBlockX;

            GameObject nextBlock = RunnerObject.GetRunnerObject(block.gameObject);
            nextBlock.transform.position = new Vector3(xPos, level, 0);
            nextBlock.SetActive(true);

            plantingBox.SetIndex(plantIndex);
            plantingBox.transform.position = new Vector3(xPos + 1, level, 0);
            plantingBox.gameObject.SetActive(true);

            nextBlock = RunnerObject.GetRunnerObject(block.gameObject);
            nextBlock.transform.position = new Vector3(xPos + 2, level, 0);
            nextBlock.SetActive(true);
        }


        // Generate
        for (int i = 0; i < amountToGenerate; i++)
        {
            var xPosition = generatorManager.LastBlockX + 1;
            generatorManager.LastBlockX = xPosition;

            if (holes.Contains(i))
            {
                generatorManager.LastBlockX += 1;
                continue;
            }

            GameObject nextBlock = RunnerObject.GetRunnerObject(block.gameObject);

            float y = currentLevel;

            if (rises.Contains(i))
            {
                y += 1;
            }

            nextBlock.transform.position = new Vector3(xPosition, currentLevel, 0);
            nextBlock.SetActive(true);           
        }
    }
}