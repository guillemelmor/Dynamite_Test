using UnityEngine;

///<summary> 
/// It manages the impact wave caused by the explosion, pushing away the near objects
///</summary>
public class Explosion_Effect : MonoBehaviour 
{
    #region private variables
    private Breakable_Object breakable_object;
    #endregion

    ///<summary> 
    /// It pushes away all the objects inside the radio introduced as the attribute 
    ///</summary>
    public void Explode (float _radius, float _strength, bool fractured)
    {
        Vector3 _explosionPosition = transform.position;

        Collider[] obstacles_affected = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider hit in obstacles_affected) 
        {
            if (hit.tag.Equals("BreakableObject")) 
            {
                breakable_object = hit.transform.parent.GetComponent<Breakable_Object>();
                if (breakable_object != null) 
                {
                    breakable_object.Affected(fractured);
                    breakable_object.AffectedExplosionForce(_strength, _explosionPosition, _radius);
                }
            }
            else 
            {
                Rigidbody _rigidbody = hit.GetComponent<Rigidbody>();
                if (gameObject != hit.gameObject && _rigidbody != null)
                    _rigidbody.AddExplosionForce(_strength, _explosionPosition, _radius);
            }                           
        }

        transform.GetChild(0).gameObject.SetActive(false);
    }
}
