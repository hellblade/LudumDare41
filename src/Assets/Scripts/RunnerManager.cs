using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    public Vector3 CurrentMoveSpeed { get; private set; }

    private void Awake()
    {
        if (CurrentMoveSpeed == Vector3.zero)
        {
            CurrentMoveSpeed = new Vector3(5, 0, 0);
        }
    }


}