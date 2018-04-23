using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerObject : MonoBehaviour
{
    RunnerManager manager;
    GeneratorManager genManeger;
    [SerializeField] bool storeInObjectPool;

    static ObjectPool<GameObject> Pool = new ObjectPool<GameObject>();

    public static GameObject GetRunnerObject(GameObject source)
    {
        GameObject result = null;

        if (!Pool.TryGet(ref result))
        {
            result = Instantiate(source).gameObject;
        }

        return result;
    }

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
            Remove();
        }
    }

    public void Remove()
    {
        gameObject.SetActive(false);
        if (storeInObjectPool)
        {
            Pool.Free(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}