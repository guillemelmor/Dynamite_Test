using UnityEngine;

///<summary> 
/// It manages the affected objects by the explosion which can be breakable
///</summary>
public class Breakable_Object : MonoBehaviour
{
    #region public variables
    [SerializeField]
    private GameObject original_object;                     // ** object composed by only one piece
    [SerializeField]
    private GameObject fractured_object;                    // ** object composed by several pieces
    #endregion

    ///<summary> 
    /// It swaps the current gameobject which is the original for the fractured one
    /// in case that the bool is true
    ///</summary>
    public void Affected(bool isFractured) 
    {
        if (isFractured) 
        {
            original_object.SetActive(false);
            var position = original_object.transform.position;
            var rotation = original_object.transform.rotation;
            fractured_object.transform.position = position;
            fractured_object.transform.localRotation = rotation;
            fractured_object.SetActive(true);
        }           
    }

    ///<summary> 
    /// It apply the explosion force to the different pieces of the gameobject
    ///</summary>
    public void AffectedExplosionForce (float _strength, Vector3 _explosionPosition, float _radius) 
    {
        if (fractured_object.activeSelf) 
        {
            foreach (Transform piece in fractured_object.transform) 
            {
                Rigidbody _rigidbody = piece.GetComponent<Rigidbody>();
                if (_rigidbody != null)
                    _rigidbody.AddExplosionForce(_strength, _explosionPosition, _radius);
            }
        } 
        else 
        {
            Rigidbody _rigidbody = original_object.GetComponent<Rigidbody>();
            _rigidbody.AddExplosionForce(_strength, _explosionPosition, _radius);
        }
    }
}
