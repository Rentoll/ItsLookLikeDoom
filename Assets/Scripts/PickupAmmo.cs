﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAmmo : MonoBehaviour {

    public int ammountOfAmmo = 25;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            PlayerController.instance.ammo += ammountOfAmmo;
            PlayerController.instance.updateText();

            AudioController.instance.playAmmoPickup();

            Destroy(gameObject);
        }
    }
}
