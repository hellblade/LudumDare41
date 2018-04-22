using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGenerator : MonoBehaviour
{
    [SerializeField] RunnerObject block;

    [SerializeField] RunnerManager gameManager;
    [SerializeField] GeneratorManager generatorManager;

    [SerializeField] float minVariation = -1.5f;
    [SerializeField] float maxVariation = 1.5f;

    [SerializeField] float minXVariation = 0;
    [SerializeField] float maxXVariation = 2;

    float currentLevel = 0;


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

    void GenerateNextBlock()
    {
        GameObject nextBlock = null;

        if (!RunnerObject.Pool.TryGet(ref nextBlock))
        {
            nextBlock = Instantiate(block).gameObject;
        }

        var xPosition = Mathf.Lerp(minXVariation, maxXVariation, Random.Range(0f, 1f));
        var yPosition = Mathf.Lerp(minVariation, maxVariation, Random.Range(0f, 1f));

        xPosition += generatorManager.LastBlockX;
        yPosition += currentLevel;

        nextBlock.transform.position = new Vector3(xPosition, yPosition, 0);
        nextBlock.SetActive(true);

        currentLevel = yPosition;
        generatorManager.LastBlockX = xPosition + 1;
    }
}