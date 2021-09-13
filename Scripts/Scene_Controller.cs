using UnityEngine;
using UnityEngine.SceneManagement;

///<summary> 
/// It manages the scene changes
///</summary>
public class Scene_Controller : MonoBehaviour
{
    ///<summary> 
    /// It reloads the current scene
    ///</summary>
    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
