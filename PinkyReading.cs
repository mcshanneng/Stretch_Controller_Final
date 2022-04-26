using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using UnityEngine.UI;
using static Unity.Mathematics.math;

public class PinkyReading : MonoBehaviour
{
    UduinoManager manager;
    public Transform transform;
//Variables to Change Text
    public Text minText;
    public Text maxText;
    public Text rawValTxt;
    public Text mappedValTxt;
//Value Variables
    private float min= 0;
    private float max= 1024;
//Calibration Start Values
    private float calibrationMin= 1024;
    private float calibrationMax= 0;
//Serialized Fields to change Min and Max for mapping
    [SerializeField] private float mapMin;
    [SerializeField] private float mapMax;

    private float pinkyValue; // Raw Input Value
    private int calibrateToggle; //Detects whether button is being pushed
    private float mappedVal; //mapped value of the raw inout

// Start is called before the first frame update
    void Start()
    {
    manager= UduinoManager.Instance;
    manager.pinMode(10, PinMode.Input); //assign pin 10 as input
    manager.pinMode(4,PinMode.Input_pullup); //assign pin 4 as pullup input
    }

// Update is called once per frame
    void Update()
    {
        
        pinkyValue= manager.analogRead(10);
        calibrateToggle=manager.digitalRead(4);
        mappedVal= remap(min, max, mapMin, mapMax, pinkyValue);// Remapping Values
        setText();
    //Rotation based on Map Val
        Quaternion target = Quaternion.Euler(mappedVal, 0f, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.time * 5.0f);

    //Calibrate while pin 4 is pressed
        if(calibrateToggle==1){
            Calibrate();
        }
        
        

    }

    void Calibrate()
    {
        if(pinkyValue<calibrationMin){// if pinky value is less than the calibration minimum (default is 1024), the lowest value recorded will be the min
            min=pinkyValue;
            calibrationMin=min;
        }
        if(pinkyValue>calibrationMax){// if pinky value is greater than the calibration maximum (default is 0), the greates value recorded will be the max
            max=pinkyValue;
            calibrationMax=max;
        }else{
            min=min;
            max=max;
        }
    }

    void setText(){
        minText.text = "Min: " + min.ToString();
        maxText.text = "Max: " + max.ToString();
        rawValTxt.text = "Raw Value: " + pinkyValue.ToString();
        mappedValTxt.text = "Mapped Value: " + mappedVal.ToString();
        


    }

}
