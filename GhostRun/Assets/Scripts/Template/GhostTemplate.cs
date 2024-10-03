using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "GhostRun/Templates/Ghost",fileName ="Ghost",order = 0)]
public class GhostTemplate : ScriptableObject
{
    public int id;

    [Header("기본정보")]
    public string displayName;
    [Multiline] public string description;
    public GameObject prefab;
    public float power;
}
