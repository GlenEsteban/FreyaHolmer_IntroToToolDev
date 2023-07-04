using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ExplosiveBarrelSO : ScriptableObject {
    [Range ( 1, 10)] public float radius = 1;
    public float damage = 10;
    public Color color = Color.red;
}
