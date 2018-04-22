﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerObject : MonoBehaviour
{
    RunnerManager manager;
    GeneratorManager genManeger;
    [SerializeField] bool storeInObjectPool;

    public static ObjectPool<GameObject> Pool = new ObjectPool<GameObject>();

    private void Awake()
    {
        manager = FindObjectOfType<RunnerManager>();
        genManeger = FindObjectOfType<GeneratorManager>();
    }

    private void FixedUpdate()
    {
        transform.position -= manager.CurrentMoveSpeed * Time.deltaTime;

        if (transform.position.x < -genManeger.ScreenAmountX - 2)
        {
            if (storeInObjectPool)
            {
                gameObject.SetActive(false);
                Pool.Free(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}