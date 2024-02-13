using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour
{
    /*
    public bool isPressed;
    private bool prevPressedState;
    public UnityEvent onPressed;
    public UnityEvent onReleased;*/
    [SerializeField] List<GameObject> _objToActivate = new List<GameObject>();
    int _playersCounter = 0;

    // Start is called before the first frame update
    /*
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
    }*/

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playersCounter++;
            if (_playersCounter == 1)
            {
                foreach (GameObject obj in _objToActivate)
                    obj.GetComponent<IMapElement>().Activate();
            }
                
        }   
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playersCounter--;
            if(_playersCounter == 0)
            {
                foreach (GameObject obj in _objToActivate)
                {
                    if (obj.GetComponent<Spikes>())
                        obj.GetComponent<IMapElement>().Activate();
                }
            }
            
        }
    }

    /*void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
            isPressed = true;
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
            isPressed = false;
    }*/
}
