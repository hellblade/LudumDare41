﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    [SerializeField] Transform target;
    
    void FixedUpdate()
    {
        transform.position = target.position;
    }

}