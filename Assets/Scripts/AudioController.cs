using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public static AudioController instance;

    public AudioSource ammo, enemyDeath, enemyShoot, playerShoot, health, playerHurt;

    private void Awake() {
        instance = this;
    }

    public void playAmmoPickup() {
        ammo.Stop();
        ammo.Play();
    }

    public void playHealthPickup() {
        health.Stop();
        health.Play();
    }

    public void playEnemyDeath() {
        enemyDeath.Stop();
        enemyDeath.Play();
    }

    public void playEnemyShoot() {
        enemyShoot.Stop();
        enemyShoot.Play();
    }

    public void playPlayerShoot() {
        playerShoot.Stop();
        playerShoot.Play();
    }

    public void playPlayerHurt() {
        playerHurt.Stop();
        playerHurt.Play();
    }

}
