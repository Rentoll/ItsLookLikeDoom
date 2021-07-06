using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : MonoBehaviour {
    public int ammountOfHealth = 25;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            PlayerController.instance.AddHealth(ammountOfHealth);
            PlayerController.instance.updateText();

            AudioController.instance.playHealthPickup();

            Destroy(gameObject);
        }
    }
}
