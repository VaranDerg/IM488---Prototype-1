using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ControllerInputManager : MonoBehaviour
{
    public static ControllerInputManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        // Enable controls for both players
        Instance.EnableMNK();
        Instance.EnableGamepad();
    }

    public void AssignControllerSelected(GameObject obj)
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad.enabled == false)
            return;

        EventSystem.current.SetSelectedGameObject(null);


        EventSystem.current.SetSelectedGameObject(obj);
    }

    public void DisableGamepad()
    {
        //Gamepad[] gamepads = Gamepad.all.ToArray();
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null)
            return;

        InputSystem.DisableDevice(gamepad);

        //Debug.Log("Controller Disabled!");
    }

    public void EnableGamepad()
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null)
            return;

        InputSystem.EnableDevice(gamepad);

        //Debug.Log("Controller Enabled!");
    }

    public void DisableMNK()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;
        if (keyboard != null)
            InputSystem.DisableDevice(keyboard);

        if (mouse != null)
            InputSystem.DisableDevice(mouse);

        //Debug.Log("MNK Disabled!");
    }

    public void EnableMNK()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;
        if (keyboard != null)
            InputSystem.EnableDevice(keyboard);

        if (mouse != null)
            InputSystem.EnableDevice(mouse);

        //Debug.Log("MNK Enabled!");
    }

    public void TogglePlayer1Input(bool active)
    {
        InputParent.Instance._P1Input.enabled = active;
    }

    public void TogglePlayer2Input(bool active)
    {
        InputParent.Instance._P2Input.enabled = active;
    }

    
}
