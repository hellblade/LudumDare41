using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    [SerializeField] Transform target;
    
    void Update()
    {
        var newSpot = new Vector3(transform.position.x, target.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newSpot, 1);
    }

}