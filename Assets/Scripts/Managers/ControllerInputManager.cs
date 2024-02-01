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
    }

    

}
