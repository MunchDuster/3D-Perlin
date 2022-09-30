using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : InputManager
{
    public override void Setup()
    {
        //Nothing to do, dummy function
    }
    public override void DoUpdate()
    {
        //Get user input
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
    }
}
