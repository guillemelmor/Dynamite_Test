using UnityEngine;
using UnityEngine.UI;

///<summary> 
/// It manages the appearance of the button with respect to the mouse behavior
///</summary>
public class UI_Button : MonoBehaviour
{
    #region public variables
    [SerializeField]
    private Sprite default_icon, selected_icon;                                 // ** button icon sprites
    [SerializeField]
    private Color32 default_text_color = new Color32(238, 175, 100, 255);       // ** button default text color
    [SerializeField]
    private Color selected_text_color = Color.black;                            // ** button selected text color
    [SerializeField]
    private Text button_txt;                                                    // ** reference to the Text Component of the button
    [SerializeField]
    private Image button_icon;                                                  // ** reference to the icon of the button
    #endregion
    
    ///<summary> 
    /// It manages the appearance of the button when the mouse is pressing or is over it
    ///</summary>
    public void Button_Pressed() 
    {
        button_icon.sprite = selected_icon;
        button_txt.color = selected_text_color;
    }

    ///<summary> 
    /// It manages the appearance of the button when the mouse is leaves it
    ///</summary>
    public void Button_Unpressed() 
    {
        button_icon.sprite = default_icon;
        button_txt.color = default_text_color;
    }
}
