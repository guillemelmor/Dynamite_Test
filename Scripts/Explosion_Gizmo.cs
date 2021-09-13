using UnityEditor;
using UnityEngine;

///<summary> 
/// It manages the explosion radius gizmo
///</summary>
public class Explosion_Gizmo : MonoBehaviour
{
    #region public variables
    [Header("Gizmo Attributes")]
    [SerializeField]
    private Color32 gizmo_color = new Color32(255, 166, 59, 50);   // ** Radius Gizmo Color
    [SerializeField]
    private Texture gizmo_icon;                                     // ** Gizmo Icon
    #endregion

    // Explosion Radius Gizmo
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    private void OnDrawGizmos() {
        Explosion_Manager explosion_manager = gameObject.GetComponent<Explosion_Manager>();
        Handles.color = gizmo_color;
        Handles.zTest = UnityEngine.Rendering.CompareFunction.Less;
        Handles.SphereHandleCap(0, transform.position, transform.rotation, explosion_manager.Radius, EventType.Repaint);
        Handles.Label(transform.position, gizmo_icon);
    }
}
