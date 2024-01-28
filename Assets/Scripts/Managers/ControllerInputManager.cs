using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerInputManager : MonoBehaviour
{
    public static ControllerInputManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void AssignControllerSelected(GameObject obj)
    {
        EventSystem.current.SetSelectedGameObject(null);


        EventSystem.current.SetSelectedGameObject(obj);
    }
}
