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

    public void TogglePlayerDevices(Player player, bool toggle)
    {
        switch(player){
            case(Player.one):
                TogglePlayerDevices(toggle, _P1Input);
                return;
            case (Player.two):
                TogglePlayerDevices(toggle, _P2Input);
                return;
        }
    }

    private void TogglePlayerDevices(bool toggle, UniversalInputManager player)
    {
        if (player == null)
            return;
        //Debug.Log("Player " + player.name + " enabled equals " + toggle);
        if (toggle)
            player.EnableInputDevices();
        else
            player.DisableInputDevices();
    }
}
