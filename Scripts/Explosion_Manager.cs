using UnityEngine;

 ///<summary> 
 /// It manages all the parameters of the explosion
 ///</summary>
public class Explosion_Manager : MonoBehaviour
{
    #region attributes
    [Header("Explosion Attributes")]
    [SerializeField]
    private float radius = 12f;                                     // ** Impact Radius
    [SerializeField]
    private float strength = 10f;                                   // ** Impact Strength
    [SerializeField]
    private float countdown = 3f;                                   // ** Explosion Countdown Time
    [SerializeField]
    private bool fractured_objects = true;                          // ** Fractured Objects Switch    
    #endregion


    // Prevents contained variables from having negative value    
    private void OnValidate() {
        radius = Mathf.Clamp(radius, 0, float.MaxValue);
        strength = Mathf.Clamp(strength, 0, float.MaxValue);
        countdown = Mathf.Clamp(countdown, 0, float.MaxValue);
    }

    #region getters & setters
    public float Radius => radius;
    public float Strength => strength;
    public float Countdown 
    {
        get => countdown;
        set => countdown = value;
    }
    public bool Fractured_Object {
        get => fractured_objects;
        set => fractured_objects = value;
    }
    #endregion
}
