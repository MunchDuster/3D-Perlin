using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DriveCar))]
public class CarUI : MonoBehaviour
{
    [Header("Main UI")]
    public Gauge speedUI;
    public Gauge turnUI;

    [Header("Optional UI")]
    public Text speedDisplay;
    public Text acclerationDisplay;


    private Dictionary<float, float> velocitySnags;
    private List<float> timeSnags;
    private new Rigidbody rigidbody;
    private DriveCar car;

    float GetAcceleration()
    {

        timeSnags.Add(Time.time);
        velocitySnags.Add(Time.time, rigidbody.velocity.magnitude / Time.deltaTime);
        while (timeSnags[0] < Time.time - 1)
        {
            timeSnags.RemoveAt(0);
            velocitySnags.Remove(0);
        }

        float total = 0;
        for (int i = 0; i < timeSnags.Count; i++)
        {
            total = velocitySnags[timeSnags[i]];
        }
        return total / velocitySnags.Count;
    }
    string FloorToDecimalPlace(float num, int places)
    {
        float placedNum = ((int)(num * Mathf.Pow(10, places))) / Mathf.Pow(10, places);
        string str = placedNum.ToString();

        return str;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        car = GetComponent<DriveCar>();

        timeSnags = new List<float>();
        velocitySnags = new Dictionary<float, float>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        speedUI.value = (car.speed + car.maxReverseSpeed) / (car.maxSpeed + car.maxReverseSpeed);
        turnUI.value = (car.turn / car.maxTurnAngle * 2) + 0.5f;

        if (speedDisplay != null)
        {
            speedDisplay.text = "car.speed:\n " + Mathf.FloorToInt(rigidbody.velocity.magnitude) + "m/s";
        }
        if (acclerationDisplay != null)
        {
            acclerationDisplay.text = "Acceleration:\n " + FloorToDecimalPlace(GetAcceleration(), 2) + "m/s²";
            
        }
    }
}
