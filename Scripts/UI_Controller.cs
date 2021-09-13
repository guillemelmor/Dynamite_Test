using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

///<summary> 
/// It manages the different behaviors of the UI elements
///</summary>
public class UI_Controller : MonoBehaviour
{
    #region public variables
    [Header("UI Attributes")]
    [SerializeField]
    private Color32 default_text_color = new Color32(238, 175, 100, 255);           // ** Default Text Color of the UI elements
    [SerializeField]
    private Color selected_button_text_color = Color.black;                         // ** Text Color of the pressed buttons
    [SerializeField]
    private Color32 selected_input_color = new Color32(13, 218, 156, 255);          // ** Text Color of the input field

    [Header("Resources")]
    [SerializeField]
    private Texture2D cursor_arrow;
    [SerializeField]
    private Texture2D cursor_hand;
    [SerializeField]
    private Texture2D cursor_text;
    [SerializeField]
    private Sprite camera_on_default_icon, camera_off_default_icon, timer_default_icon, fragmented_on_default_icon, fragmented_off_default_icon;
    [SerializeField]
    private Sprite camera_on_selected_icon, camera_off_selected_icon, timer_selected_icon, fragmented_on_selected_icon, fragmented_off_selected_icon;

    [Header("References")]
    [SerializeField]
    private Explosion_Manager explosion_manager;
    [SerializeField]
    private Shake_Effect shake_effect;
    [SerializeField]
    private Text camera_txt, timer_txt, fragmented_txt;
    [SerializeField]
    private Image camera_icon, timer_icon, fragmented_icon;
    [SerializeField]
    private GameObject start_button, camera_shake_button, timer_field, reload_button, explode_button, fragmented_button;
    [SerializeField]
    private InputField timer_input;
    #endregion


    // It sets the initial camera-shake icon, the default cursor icon, and the initial countdown time in the input field
    private void Awake() 
    {
        if (!shake_effect.CameraShaking)
            camera_icon.sprite = camera_off_default_icon;
        else
            camera_icon.sprite = camera_on_default_icon;

        if (!explosion_manager.Fractured_Object)
            fragmented_icon.sprite = fragmented_off_default_icon;
        else
            fragmented_icon.sprite = fragmented_on_default_icon;

        Cursor.SetCursor(cursor_arrow, Vector2.zero, CursorMode.ForceSoftware);

        timer_input.text = "" + explosion_manager.Countdown;
    }

    ///<summary> 
    /// It swaps the value of the camera-shake bool and changes the camera-shake button icon
    ///</summary>
    public void Camera_Shake() 
    {
        shake_effect.CameraShaking = !shake_effect.CameraShaking;
        if (!shake_effect.CameraShaking)
            camera_icon.sprite = camera_off_default_icon;
        else
            camera_icon.sprite = camera_on_default_icon;
    }

    ///<summary> 
    /// It swaps the value of the fragmented bool and changes the fragmented button icon
    ///</summary>
    public void Fragmented_Object() {
        explosion_manager.Fractured_Object = !explosion_manager.Fractured_Object;
        if (!explosion_manager.Fractured_Object)
            fragmented_icon.sprite = fragmented_off_default_icon;
        else
            fragmented_icon.sprite = fragmented_on_default_icon;
    }

    ///<summary> 
    /// It hides the initial buttons and unhides the in-game buttons
    ///</summary>
    public void Start_Button() 
    {
        start_button.SetActive(false);
        camera_shake_button.SetActive(false);
        timer_field.SetActive(false);
        fragmented_button.SetActive(false);
        reload_button.SetActive(true);
        explode_button.SetActive(true);
    }

    ///<summary> 
    /// It takes the value from the input field and stores it in the Explosion Manager
    ///</summary>
    public void TextField_Input() 
    {
        string countdown = timer_input.text;
        float value = 0;

        CultureInfo culture_info = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        culture_info.NumberFormat.CurrencyDecimalSeparator = ".";

        if (countdown.Length == 0) 
        {
            explosion_manager.Countdown = 0;
            timer_input.text = "0";
        }          
        else if(float.TryParse(countdown, NumberStyles.Any, culture_info, out value))
            explosion_manager.Countdown = value;
        else
            timer_input.text = "" + explosion_manager.Countdown;
    }

    ///<summary> 
    /// It changes the mouse cursor into a hand
    ///</summary>
    public void Cursor_Over_Button() 
    {
        Cursor.SetCursor(cursor_hand, new Vector2(14, 14), CursorMode.ForceSoftware);
    }

    ///<summary> 
    /// It changes the mouse cursor into a feather
    ///</summary>
    public void Cursor_Over_Input() 
    {
        Cursor.SetCursor(cursor_text, Vector2.zero, CursorMode.ForceSoftware);
    }

    ///<summary> 
    /// It changes the mouse cursor into his default
    ///</summary>
    public void Cursor_Out() 
    {
        Cursor.SetCursor(cursor_arrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    ///<summary> 
    /// It changes the button image and text color once the mouse is over it
    ///</summary>
    public void Button_Down(GameObject button) 
    {
        UI_Button button_behaviour = button.GetComponent<UI_Button>();
        if (button_behaviour != null)
            button_behaviour.Button_Pressed();
    }
    ///<summary> 
    /// It sets the default values to the button image and text color
    ///</summary>
    ///
    public void Button_Up(GameObject button) {
        UI_Button button_behaviour = button.GetComponent<UI_Button>();
        if (button_behaviour != null)
            button_behaviour.Button_Unpressed();
    }

    ///<summary> 
    /// It changes the camera text color and updates the button icon once the mouse is over it
    ///</summary>
    public void Shake_Camera_Button_Down() 
    {
        camera_txt.color = selected_button_text_color;
        if (!shake_effect.CameraShaking)
            camera_icon.sprite = camera_off_selected_icon;
        else
            camera_icon.sprite = camera_on_selected_icon;
    }

    ///<summary> 
    /// It sets the default value to the camera text color and updates the button icon
    ///</summary>
    public void Shake_Camera_Button_Up() 
    {
        camera_txt.color = default_text_color;
        if (!shake_effect.CameraShaking)
            camera_icon.sprite = camera_off_default_icon;
        else
            camera_icon.sprite = camera_on_default_icon;
    }

    ///<summary> 
    /// It changes the fragmented text color and updates the button icon once the mouse is over it
    ///</summary>
    public void Fragmented_Button_Down() {
        fragmented_txt.color = selected_button_text_color;
        if (!explosion_manager.Fractured_Object)
            fragmented_icon.sprite = fragmented_off_selected_icon;
        else
            fragmented_icon.sprite = fragmented_on_selected_icon;
    }

    ///<summary> 
    /// It sets the default value to the fragmented text color and updates the button icon
    ///</summary>
    public void Fragmented_Button_Up() {
        fragmented_txt.color = default_text_color;
        if (!explosion_manager.Fractured_Object)
            fragmented_icon.sprite = fragmented_off_default_icon;
        else
            fragmented_icon.sprite = fragmented_on_default_icon;
    }
}
