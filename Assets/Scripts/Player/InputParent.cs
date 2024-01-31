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

    public void SetSelectedUIObject(Player player, GameObject obj, GameObject playerRoot = null)
    {

        /*UniversalInputManager playerInput = player switch
        {
            (Player.one) => _P1Input,
            (Player.two) => _P2Input,
            _ => _P1Input,
        };

        if (playerInput.IsMNK())
            obj = null;

        playerInput.SetSelected(obj);
        */
        switch (player)
        {
            case (Player.one):
                if (_P1Input.IsMNK())
                    return;

                _P1Input.SetSelected(obj, playerRoot);
                return;
            case (Player.two):
                if (_P2Input.IsMNK())
                    return;

                _P2Input.SetSelected(obj, playerRoot);
                return;
        }
    }

    public void AssertControlToPlayer(Player player, GameObject firstSelected, GameObject playerRoot = null)
    {
        switch (player)
        {
            case (Player.one):
                _P2Input.DisableInput();
                SetSelectedUIObject(Player.two, null);
                SetSelectedUIObject(Player.one, firstSelected, playerRoot);
                return;

            case (Player.two):
                _P1Input.DisableInput();
                SetSelectedUIObject(Player.one, null);
                SetSelectedUIObject(Player.two, firstSelected, playerRoot);
                return;
        }
    }

    public void EnableAllInputs()
    {
        _P1Input.EnableInput();
        if (_P2Input != null)
            _P2Input.EnableInput();
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
