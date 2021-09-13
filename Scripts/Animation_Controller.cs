using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary> 
/// It manages the animation events
///</summary>
public class Animation_Controller : MonoBehaviour
{
    ///<summary> 
    /// Once the current animation fnishes, it disables the object
    ///</summary>
    public void AnimationEnd() => gameObject.SetActive(false);
}
