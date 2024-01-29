using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] float speed;

    bool getDefensive;


    private void Start()
    {
        getDefensive = false;
    }
    // Update is called once per frame
    void Update()
    {
        print(getDefensive);
        if (getDefensive)
        {
            transform.Rotate(0, 1 * speed * Time.deltaTime, 0);

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "PhyInt")
        {
            //start spinning
            getDefensive = true;
            //apply damage
        }
    }


    private void OnTriggerExit(Collider other)
    {
         if (other.gameObject.tag == "Player" || other.gameObject.tag == "PhyInt")
        {
            //stop spining
            getDefensive = false;

            
        }
    }

}
