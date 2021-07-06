using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject Explosion;
    public bool canShoot;
    public float fireRate = .5f;
    private float shootCounter;
    public GameObject bullet;
    public Transform firePoint;

    public int health = 3;
    public float playerRange = 10f;
    public Rigidbody2D rigidbodyEnemy;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange) {
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
            rigidbodyEnemy.velocity = playerDirection.normalized * moveSpeed;
            if(canShoot) {
                shootCounter -= Time.deltaTime;
                if(shootCounter <= 0) {
                    AudioController.instance.playEnemyShoot();

                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shootCounter = fireRate;
                }
            }
        } else {
            rigidbodyEnemy.velocity = Vector2.zero;
        }
    }

    public void takeDamage() {
        health--;

        if (health <= 0) {
            AudioController.instance.playEnemyDeath();

            Destroy(gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);
        }
    }
}
