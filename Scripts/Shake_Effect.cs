using System.Collections;
using UnityEngine;

///<summary> 
/// It manages the camera shake caused by the explosion
///</summary>
public class Shake_Effect : MonoBehaviour
{
    #region public variables
    [Header("Camera Settings")]
    [SerializeField]
    private bool camera_shaking = true;                             // ** Camera Shaking Switch
    [SerializeField]
    private float intensity = 0.5f;                                 // ** Shaking Intensity
    [SerializeField]
    private float duration = 0.5f;                                  // ** Shaking Duration
    #endregion

    ///<summary> 
    /// Coroutine that animates the camera shaking along the duration time
    ///</summary>
    public IEnumerator Shake () 
    {
        var initial_position = transform.localPosition;
        float time_elapsed = 0f;
       
        while (time_elapsed < duration) 
        {
            float x = initial_position.x + Random.Range(-1f, 1f) * intensity;
            float y = initial_position.y + Random.Range(-1f, 1f) * intensity;
            transform.localPosition = new Vector3(x, y, initial_position.z);
            time_elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = initial_position;
    }

    #region getters and setters
    public bool CameraShaking {
        get => camera_shaking;
        set => camera_shaking = value;
    }
    #endregion
}
