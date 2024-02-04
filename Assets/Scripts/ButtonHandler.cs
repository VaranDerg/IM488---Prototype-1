using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour
{
    public bool isPressed;
    private bool prevPressedState;
    public UnityEvent onPressed;
    public UnityEvent onReleased;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed && prevPressedState != isPressed)
            Pressed();
        if (!isPressed && prevPressedState != isPressed)
            Released();
    }

    void Pressed()
    {
        prevPressedState = isPressed;
        onPressed.Invoke();
    }

    void Released()
    {
        prevPressedState = isPressed;
        onReleased.Invoke();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "PlasmoOne" || col.gameObject.name == "PlasmoTwo")
            isPressed = true;
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "PlasmoOne" || col.gameObject.name == "PlasmoTwo")
            isPressed = false;
    }
}
