using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayer : Character
{
    private PlayerControls controls;
   

    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();
        //Map and store input
        controls.Player.Movement.performed += ctx =>
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            MovementVector = new Vector3(input.y, 0, input.x);
            print("Working");
        };
    }

}
