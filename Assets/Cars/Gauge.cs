using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public Transform dial;
    public float minAngle = -135;
    public float maxAngle = 135;

    public bool flip; 
    [Range(0,1)]
    public float smoothing = 0.1f;
    [Range(0, 1)]
    public float startAngle = 0;

    public float value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = Mathf.Clamp(value, 0f, 1f);
            float setVal;
            if (flip)
            {
                setVal = (1 - _value);
            }
            else
            {
                setVal = _value;
            }
            if(maxAngle>=minAngle)
                targetAngle = Mathf.Lerp(minAngle,maxAngle, setVal);
            else
                targetAngle = Mathf.Lerp(maxAngle, minAngle, setVal);
        }
    }

    private float angle;
    private float _value;
    private float targetAngle;
    
    // Start is called before the first frame update
    void Start()
    {
        angle = startAngle;
        targetAngle = startAngle;
        _value = Mathf.InverseLerp(minAngle, maxAngle, startAngle);
    }
    void Update()
    {
        //Debug.Log(targetAngle);
        dial.rotation = Quaternion.Lerp(dial.rotation, Quaternion.Euler(0, 0, targetAngle), (1 - smoothing));
    }
}
