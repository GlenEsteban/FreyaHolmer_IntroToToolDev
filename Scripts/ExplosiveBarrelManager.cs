using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// NOTE: #if UNITY_EDITOR is a preprocessor command includes/excludes blocks of code based on the platform the code runs in. When building a game,
// ... using unity editor, which includes the Handles, can cause errors. This is why you should use this on blocks of code which uses unity editor.
#if UNITY_EDITOR
using UnityEditor;
#endif


public class ExplosiveBarrelManager : MonoBehaviour {
    public enum VisualLineStyles { Line, AAPolyLine, Bezier }
    public enum LineRender { InFront, Behind }
    [SerializeField] VisualLineStyles lineStyles;
    [SerializeField] LineRender lineRender;
    public static List<ExplosiveBarrel> allExplosiveBarrels = new List<ExplosiveBarrel>();

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        //  NOTE: You can alter whether Handles render the lines in front or behind objects using Handles.zTest.
        bool isInFront = lineRender == LineRender.InFront; 
        Handles.zTest = isInFront ? CompareFunction.Always: CompareFunction.LessEqual;

        DrawLine();
    }

    private void DrawLine()
    {
        foreach (ExplosiveBarrel barrel in allExplosiveBarrels)
        {
            if ( barrel.type == null) { return; }
            
            if ( lineStyles == VisualLineStyles.Line ) {
                Gizmos.DrawLine( transform.position, barrel.transform.position );
            }
            else if ( lineStyles == VisualLineStyles.AAPolyLine ) {
                Handles.DrawAAPolyLine( transform.position, barrel.transform.position );
            }
            else if ( lineStyles == VisualLineStyles.Bezier ) {
                float halfHeight = ( transform.position.y - barrel.transform.position.y ) * .5f;
                Vector3 offset = Vector3.up * halfHeight;
                Vector3 startTangent = transform.position - offset;
                Vector3 endTangent = barrel.transform.position + offset;

                Handles.DrawBezier(
                    transform.position,
                    barrel.transform.position,
                    startTangent,
                    endTangent,
                    barrel.type.color,
                    EditorGUIUtility.whiteTexture,
                    1f
                );
            }
        }
    }
#endif
}
