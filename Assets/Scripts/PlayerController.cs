using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    public Rigidbody2D playerRB;
    public Camera playerCamera;
    public GameObject bulletImpact;
    public Animator gunAnim;

    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 mouseInput;

    public int ammo;
    public int currentHealth;
    public int maxHealth = 100;
    public GameObject deadScreen;
    private bool isDead = false;

    public float mouseSensitivity = 1f;

    public Text healthText, ammoText;
    public Animator anim;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
        updateText();
    }

    // Update is called once per frame
    void Update() {
        if (!isDead) {
            //player movement
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            Vector3 moveHorizontal = transform.up * -moveInput.x;
            Vector3 moveVecrtical = transform.right * -moveInput.y;

            playerRB.velocity = (moveHorizontal - moveVecrtical) * moveSpeed;

            //player view 
            mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);
            playerCamera.transform.localRotation = Quaternion.Euler(playerCamera.transform.localRotation.eulerAngles + new Vector3(0, mouseInput.y, 0));

            //shooting
            if (Input.GetMouseButtonDown(0) && ammo > 0) {
                Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    //Debug.Log("Looking at " + hit.transform.name);
                    Instantiate(bulletImpact, hit.point, transform.rotation);
                    if (hit.transform.tag == "Enemy") {
                        hit.transform.parent.GetComponent<EnemyController>().takeDamage();
                    }
                }
                else {
                    Debug.Log("There nothing");
                }
                ammo--;

                AudioController.instance.playPlayerShoot();

                gunAnim.SetTrigger("Shoot");
                updateText();
            }
            if(moveInput != Vector2.zero) {
                anim.SetBool("IsMoving", true);
            } 
            else {
                anim.SetBool("IsMoving", false);
            }
        }
    }

    public void updateText() {
        healthText.text = currentHealth.ToString();
        ammoText.text = ammo.ToString();
    }

    public void TakeDamage(int damageAmount) {
        currentHealth -= damageAmount;

        AudioController.instance.playPlayerHurt();

        if (currentHealth <= 0) {
            deadScreen.SetActive(true);
            currentHealth = 0;
        }
        updateText();
    }

    public void AddHealth(int healAmount) {
        currentHealth += healAmount;
        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        updateText();
    }

}
