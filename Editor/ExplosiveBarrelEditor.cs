using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects] // allows you to select and edit multiple objects
[CustomEditor(typeof(ExplosiveBarrelSO))]
public class ExplosiveBarrelEditor : Editor {
    SerializedObject so;
    SerializedProperty propRadius;
    SerializedProperty propDamage;
    SerializedProperty propColor;

    void OnEnable() { // called when object selected
        so = serializedObject;
    }
    public override void OnInspectorGUI() {
        ExplosiveBarrelSO barrel = target as ExplosiveBarrelSO;
        barrel.radius = EditorGUILayout.FloatField( "Radius", barrel.radius );
        barrel.damage = EditorGUILayout.FloatField( "Damage", barrel.damage );
        barrel.color = EditorGUILayout.ColorField( "Color", barrel.color );

    }
}

/*  NOTE: 
public float thing1; // serialized, visible, public
float thing2; // not serialized, hidden, private
[SerializeField] float thing3; // serialized, visible, private
[HideInInspector] public float thing4; // serialized, hidden, public

Uses explicit positioning using Rect:
GUI
EditorGUI

Uses implicit positioning, auto-layout:
GUILayout
EditorGUILayout

// test code for different editor UI elements
CODE:
    public enum Gear { Sword, Shield, MagicRing }
    Gear gear;
    float radius;
    float damage;

    public override void OnInspectorGUI() {
        // Displays the inspector as it is
        //base.OnInspectorGUI();

        // label UI
        GUILayout.Label( "Test" );
        GUILayout.Label( "Test", GUI.skin.button );
        GUILayout.Label( "Test", EditorStyles.boldLabel );

        // GUILayout.Button returns a bool and can be used to execute code when it is clicked
        if ( GUILayout.Button( "DO SOMETHING" )) 
            Debug.Log( " DID SOMETHING" );
        
        // To showcase enums, use EditorGUILayout since you can't show enums in the in-game UI.
        gear = (Gear) EditorGUILayout.EnumPopup( gear ); //typecasted because it wasn't assigning for some reason

        // Horizontal UI
        GUILayout.BeginHorizontal();
        GUILayout.Label( "Radius Stat: " + (int)radius, GUILayout.Width( 110)); // NOTE: Use GUILayout.Width() to clamp the width of the UI element
        radius = GUILayout.HorizontalSlider( radius, 0f, 256f );
        GUILayout.EndHorizontal();

        using( new GUILayout.HorizontalScope()){ // much safer since you do not need to specify the end
            GUILayout.Label( "Damage Stat:" );
            damage = GUILayout.HorizontalSlider( damage, 0f, 256f );
            if ( GUILayout.Button( "Check" )) 
                Debug.Log( damage );
        }

        // Obj field
        EditorGUILayout.ObjectField( null, typeof(Transform), true);

        // Adding spacing between elements
        GUILayout.Space( 20 );

        // Vertical UI
        using( new GUILayout.VerticalScope( EditorStyles.helpBox)) {
            using( new GUILayout.HorizontalScope()){ // much safer since you do not need to specify the end
            GUILayout.Label( "Damage Stat:" );
            damage = GUILayout.HorizontalSlider( damage, 0f, 256f );
            if ( GUILayout.Button( "Check" )) 
                Debug.Log( damage );
            }
        }
    }
*/
