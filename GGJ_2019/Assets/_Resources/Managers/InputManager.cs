﻿using UnityEngine;
using UnityEngine.Assertions;

public class InputManager : MonoBehaviour
{
    private Player player = null;

    // Called before the first frame update.
    private void Start()
    {
        player = FindObjectOfType<Player>();
        Assert.IsNotNull(player);
    }

    // Called once per frame.
    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            player.Interact(player.GetComponent<PlayerMovement>().LookDirection);
        }
        if (Input.GetButtonDown("SpecialAction"))
        {
            player.SpecialAction();
        }
        if(Input.GetKeyDown("escape")) {//When a key is pressed down it see if it was the escape key if it was it will execute the code
            Application.Quit(); // Quits the game
        }
    }
}