using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ExplosiveBarrelSO : ScriptableObject {
    [Range ( 1, 10)] public float radius = 1;
    public float damage = 10;
    public Color color = Color.red;

    // NOTE: Since the class has been Serialized, the variables are visible in the inspector. You declare a class or a list of a class.
    //public MyClass thing;
    //public List<MyClass> thing = new List<MyClass>();
}
// NOTE: the Serializable attribute can be used to serialize a class and its variables. Data types that are not serializable still won't be serialized.
// ... Serialization ignores inheritance. So, if a class inherits from a class that has this attribute, the class will not be serialized.
// CODE:
//  

