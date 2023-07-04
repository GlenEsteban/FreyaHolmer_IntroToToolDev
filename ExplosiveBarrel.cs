using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// See Notes Below on ExecuteAlways
[ExecuteAlways]
public class ExplosiveBarrel : MonoBehaviour {
    // NOTE: The Range attribute clamps the value of a variable. You should the Range attribute to prevent level designers from assigning invalid values
    static readonly int shPropColor = Shader.PropertyToID( "_Color" );

    public ExplosiveBarrelSO type;
    
    MaterialPropertyBlock mpb;
    MaterialPropertyBlock Mpb {
        get {
            if (mpb == null )
                mpb = new MaterialPropertyBlock();
            return mpb;
        }
    }

    // NOTE: Having multiple objects using OnDrawGizmos() can slow things down, so you should use OnDrawGizmosSelected() for large projects.
    
    #if UNITY_EDITOR
    void OnDrawGizmosSelected() {
        if (type == null) { return; }

        Handles.color = type.color;
        Handles.DrawWireDisc( transform.position, Vector3.up, type.radius);
        Handles.color = Color.white; // Resets the color back to default
    }
    #endif

    //  NOTE: OnValidate() is a useful method that is called everytime yoou modify a value in the inspector. 
    void OnValidate() => ApplyColor();

    // NOTE: This code pattern allows for the explosive barrel manager (manager) to easily keep track of all explosive barrels (objects)
    // ... even through destroying the object or through assembly reloads.
    void OnEnable() => ExplosiveBarrelManager.allExplosiveBarrels.Add(this);
    void OnDisable() => ExplosiveBarrelManager.allExplosiveBarrels.Remove(this);
    void ApplyColor() {
        if ( type == null) { return; }
        MeshRenderer meshRend = GetComponent<MeshRenderer>();
        Mpb.SetColor( shPropColor, type.color );
        meshRend.SetPropertyBlock( Mpb );
    }
}
/*  NOTE: The ExecuteAlways attribute makes instances of a script execute not only in Play Mode but also in editing. 
    WARNING: Be wary when using these code to modify an object's material or mesh with the ExecuteAlways attribute.
    CODE:
        GetComponent<MeshRenderer>().material.color = color; // This instantiates/duplicates the material. This LEAKS assets. It creates materials while
        ... in the editor, and it won't be cleaned after leaving the editor.
        GetComponent<MeshRenderer>().sharedMaterial.color = color; // This will modify the *asset* which you don't want to change in runtime.
        // This also applies to GetComponent<MeshFilter>().mesh & GetComponent<MeshFilter>().sharedMesh
*/

/*  NOTE: Using HideFlags is useful when creating material that will only be used in the editor and should not be saved.
    WARNING: Be wary of using HideFlags. Using it to hide a component, will make it invisible, and hard to debug.
    CODE:
        Shader shader = Shader.Find( "Default/Diffuse" );
        Material mat = new Material( shader ) { hideFlags = HideFlags.HideAndDontSave }; // This creates a material but also makes it so the asset isn't saved
*/
