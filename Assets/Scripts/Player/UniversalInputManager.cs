using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class UniversalInputManager : MonoBehaviour
{
    //public static UniversalInputManager Instance;
    [SerializeField] Controller _associatedController;

    [SerializeField]
    InputDevice[] devices;
    // Start is called before the first frame update
    void Start()
    {
        InputParent.Instance.AssignInputManager(this);
        DontDestroyOnLoad(gameObject);

        devices = GetComponent<PlayerInput>().devices.ToArray();
        /*Debug.Log("Connected Devices:");

        //bool hasKeyboard = false;
        foreach (InputDevice d in devices)
        {
            Debug.Log(d.displayName);
            /*if(d.displayName == "Keyboard")
            {
                hasKeyboard = true;
                continue;
            }*/
        //}

        /*if (hasKeyboard)
        {
            InputDevice[] devices2 = new InputDevice[devices.Length + 1];
            devices2[devices.Length] = Mouse.current;

            devices = devices2;

            Debug.Log("Added Mouse");
        }*/
            

        //EstablishSingleton();
    }

    public bool IsMNK()
    {
        if (devices[0] == null)
            return false;

        return devices[0].displayName == "Mouse" || devices[0].displayName == "Keyboard";
    }

    public void Nothing()
    {
        //DON'T DELETE
        //FOR MORE INFORMATION:
            //SEE TF2 COCONUT
    }

/*    void EstablishSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            
            return;
        }
        Destroy(gameObject);

    }*/

    public void SetSelected(GameObject obj, GameObject playerRoot = null)
    {
        MultiplayerEventSystem eventSystem = GetComponent<MultiplayerEventSystem>();
        eventSystem.SetSelectedGameObject(obj);

        if (playerRoot != null)
            eventSystem.playerRoot = playerRoot;
    }

    public void EnableInputDevices()
    {
        foreach (InputDevice device in devices)
        {
            if (device == null)
                continue;
            InputSystem.EnableDevice(device);
        }
    }

    public void DisableInputDevices()
    {
        foreach (InputDevice device in devices)
        {
            InputSystem.DisableDevice(device);
        }
    }

    public void AssignAssociatedController(Controller newController)
    {
        if(newController != null)
            _associatedController = newController;
    }

    private bool HasValidPlayer()
    { 
        if (_associatedController != null)
        {
            return true;
        }
        return false;
    }
    

    public void WASDInput(InputAction.CallbackContext context)
    {
        //Try and keep repeated calls commented out before finalizing.
        //Debug.Log("Direction");
        if(HasValidPlayer())
        {
            _associatedController.MoveInput(context);
        }
    }

    public void DashInput(InputAction.CallbackContext context)
    {
        
        if (HasValidPlayer())
        {
            _associatedController.DashInput(context);
        }
    }

    public void EnableInput()
    {
        //GetComponent<PlayerInput>().enabled = true;
        GetComponent<PlayerInput>().actions.FindActionMap("UI").Enable();
        GetComponent<MultiplayerEventSystem>().playerRoot = null;
    }

    public void DisableInput()
    {
        GetComponent<PlayerInput>().actions.FindActionMap("UI").Disable();
        GetComponent<MultiplayerEventSystem>().playerRoot = gameObject;
    }
}

public enum InputType
{
    MNK,
    CONTROLLER1,
    CONTROLLER2
}
