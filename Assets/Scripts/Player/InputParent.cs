using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputParent : MonoBehaviour
{
    public static InputParent Instance;
    public UniversalInputManager _P1Input;
    public UniversalInputManager _P2Input;

    // Changed to awake for Execution Order - Liz
    void Awake()
    {
        EstablishSingleton();
    }

    public void AssignInputManager(UniversalInputManager inputManager)
    {
        if (_P1Input == null)
            _P1Input = inputManager;
        else if (_P2Input == null)
            _P2Input = inputManager;
        else Destroy(inputManager.gameObject);
    }

    void EstablishSingleton()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);

    }

    public bool AttachInput(Controller controller,Player player)
    {
        switch(player)
        {
            case(Player.one):
                if(_P1Input != null)
                {
                    _P1Input.AssignAssociatedController(controller);
                    return true;
                }
                return false;
                    
            case (Player.two):
                if (_P2Input != null)
                {
                    _P2Input.AssignAssociatedController(controller);
                    return true;
                }
                return false;
                    
        }
        return false;
    }
}
