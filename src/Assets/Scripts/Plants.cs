using System;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Collections/Plants")]
public class Plants : ScriptableObject
{
    [SerializeField] Plant[] plants;
}