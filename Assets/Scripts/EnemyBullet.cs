using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public int damage;
    public float bulletSpeed = 3f;

    public Rigidbody2D bulletRigidbody;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start() {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
        direction = direction * bulletSpeed;
    }

    // Update is called once per frame
    void Update() {
        bulletRigidbody.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            PlayerController.instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
