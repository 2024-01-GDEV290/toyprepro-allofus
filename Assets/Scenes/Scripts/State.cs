using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Test", menuName= "Test2")]
public class State : ScriptableObject
{
    //[SerializeField]
    public float Charge;
    public float chargeValue;
    public int allPowered = 0;
    //public PrefabAssetType gear;
    public Material wood;
    public Material carpet;
    public Material leather;
}
