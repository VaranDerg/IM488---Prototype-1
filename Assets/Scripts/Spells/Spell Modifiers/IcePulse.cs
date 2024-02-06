using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePulse : MonoBehaviour
{
    [SerializeField]
    float maxScale = 1.5f;

    [SerializeField]
    float minScale = 1f;

    [SerializeField]
    float scaleRate = 1;

    float deltaScale;

    float scaleMult = 1;

    bool isIncreasing = true;

    Vector3 startScale;

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
        //Debug.Log("Delta Scale: " + deltaScale);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Ice Pulse");
        if (scaleMult >= maxScale)
        {
            //Debug.Log("Hit max");
            isIncreasing = false;
        }
        else if(scaleMult <= minScale)
        {
            //Debug.Log("Hit min");
            isIncreasing = true;
        }

        deltaScale = (maxScale - minScale) * scaleRate * Time.fixedDeltaTime;
        scaleMult += isIncreasing ? deltaScale : -deltaScale;
        //Debug.Log("New Scale: " + scaleMult);

        SetScale();
    }

    public void ResetToMax()
    {
        scaleMult = maxScale;
        SetScale();
    }

    public void ResetToMin()
    {
        scaleMult = minScale;
        SetScale();
        //Debug.Log("Reset To Min");
    }

    private void SetScale()
    {
        transform.localScale = startScale * scaleMult;
    }
}
