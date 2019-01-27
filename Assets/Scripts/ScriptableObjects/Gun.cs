using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Guns", order = 1)]
public class Gun : ScriptableObject {

    public Color color;
    public string gunName;
    public float fireRate;
    public float lineWidth;
}
