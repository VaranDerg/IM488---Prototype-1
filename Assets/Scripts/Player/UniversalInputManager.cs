using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UniversalInputManager : MonoBehaviour
{
    //public static UniversalInputManager Instance;
    [SerializeField] Controller _associatedController;
    // Start is called before the first frame update
    void Start()
    {
        InputParent.Instance.AssignInputManager(this);
        DontDestroyOnLoad(gameObject);
        //EstablishSingleton();
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
}
