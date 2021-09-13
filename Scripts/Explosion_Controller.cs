using UnityEngine;

///<summary> 
/// It manages the dynamite explosion and his different components
///</summary>
public class Explosion_Controller : MonoBehaviour
{
    #region public variables
    [Header("Objects References")]
    [SerializeField]
    private Shake_Effect shake_effect;                      // ** reference to the shake script
    [SerializeField]
    private ParticleSystem explosion_vfx;                   // ** reference to the explosion particle system
    [SerializeField]
    private GameObject explosion_distortion;                // ** reference to the explosion distortion effect
    [SerializeField]
    private Animator explosion_light_animator;              // ** reference to the explosion light animator
    [SerializeField]
    private Renderer dynamite_fuse;                         // ** reference to the fuse material
    [SerializeField]
    private GameObject dynamite_fuse_animation;             // ** reference to the fuse flame animator
    [SerializeField]
    private GameObject explode_button;                      // ** reference to the explode button
    [SerializeField]
    private AudioClip explosion_sound;                      // ** reference to the explosion sound
    #endregion

    #region private variables
    private Explosion_Manager explosion_manager;              
    private Explosion_Effect explosion_effect;              
    private Animator dynamite_fuse_animator;
    private AudioSource explosion_audio_source;
    private bool pre_explosion = false;
    private float time_elapsed = 0f;
    private Rigidbody dynamite_rigidbody;
    #endregion

    // it initializes the private references
    private void Awake() {
        explosion_manager = gameObject.GetComponent<Explosion_Manager>();
        explosion_effect = gameObject.GetComponent<Explosion_Effect>();
        dynamite_fuse_animator = dynamite_fuse_animation.GetComponent<Animator>();
        explosion_audio_source = gameObject.GetComponent<AudioSource>();
        dynamite_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // it stores the in-game time in the scrolling shader once the pre-explosion starts
    private void FixedUpdate() {
        if (pre_explosion)
            time_elapsed += Time.deltaTime;
            dynamite_fuse.material.SetFloat("_GameTime", time_elapsed);
    }

    ///<summary> 
    /// It initializes the distortion effect translating it a little bit behind the explosion
    /// and once it has done that, it begins with the pre-explosion stage
    ///</summary>
    public void Init() {       
        Vector3 dir = Camera.main.transform.position - explosion_distortion.transform.position;
        dir.Normalize();
        explosion_distortion.transform.position += new Vector3(dir.x * -3, 0, dir.z * -3);

        ScheduledExplosion();
    }

    ///<summary> 
    /// It manages the pre-explosion stage, where the fuse starts burning. So it starts the fuse flame
    /// and material animation and schedules the explosion effect 
    ///</summary>
    private void ScheduledExplosion() {
        dynamite_fuse_animator.SetFloat("Countdown", 1 / explosion_manager.Countdown);
        dynamite_fuse_animator.Play("Fuse_Running");

        dynamite_fuse.material.SetFloat("_ScrollXSpeed", 0.5f / explosion_manager.Countdown);
        pre_explosion = true;

        Invoke(nameof(Explode), explosion_manager.Countdown);
    }

    ///<summary> 
    /// It forces the stop of the explosion simulation, canceling the pre-explosion stage
    /// and the explosion schedule
    ///</summary>
    private void CancelScheduledExplosion() {
        dynamite_fuse_animation.SetActive(false);

        dynamite_fuse.material.SetFloat("_ScrollXSpeed", 0);
        pre_explosion = false;

        CancelInvoke(nameof(Explode));       
    }

    ///<summary> 
    /// It triggers the explosion, avoiding the pre-explosion stage
    ///</summary>
    public void ExplodeButton() {
            CancelScheduledExplosion();
            Explode();     
    }

    ///<summary> 
    /// It manages the explosion effect, which includes the explosion vfx, the light animation, 
    /// the impact wave over the near objects, and the camera shake in case it is enabled
    ///</summary>
    private void Explode() 
    {
        pre_explosion = false;
        dynamite_fuse.material.SetFloat("_ScrollXSpeed", 0);

        explosion_vfx.Play();

        explosion_effect.Explode(explosion_manager.Radius / 2, explosion_manager.Strength * 400, explosion_manager.Fractured_Object);        

        explosion_light_animator.Play("Explosion_Running");

        explosion_audio_source.PlayOneShot(explosion_sound);

        if (shake_effect.CameraShaking)
            StartCoroutine(shake_effect.Shake());

        explode_button.SetActive(false);

        dynamite_rigidbody.isKinematic = true;
    }
}
 