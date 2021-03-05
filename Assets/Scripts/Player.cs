using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float h;
    float v;

    Vector3 moveDirection;
    public float speed = 3;
    [SerializeField] Transform aim;
    [SerializeField] Camera camera;
    Vector2 facingDirection;
    [SerializeField] Transform bullletPrefab;
    bool gunLoaded = true;
    [SerializeField] float fireRate = 1;
    [SerializeField] int health = 10;
    bool powerShotEnabled;
    bool invulnerable;
    [SerializeField] float invulnerableTime = 3;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        moveDirection.x = h;
        moveDirection.y = v;
        
        transform.position += moveDirection * Time.deltaTime * speed;

        //Movimiento de la mira
        facingDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized;

        if (Input.GetMouseButton(0) && gunLoaded) {
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Transform bulletClone = Instantiate(bullletPrefab, transform.position, targetRotation);
            if (powerShotEnabled) {
                bulletClone.GetComponent<Bullet>().powerShot = true;
            } 
            StartCoroutine(ReloadGun());

        }

    }

    public void TakeDamage()
    {

        if (invulnerable) {
            return;
        }


        health--;
        invulnerable = true;

        StartCoroutine(MakeVulnerableAgain());

        if (health <= 0)
        {
            //Game Over
        }
    }

    IEnumerator MakeVulnerableAgain() {
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }

    IEnumerator ReloadGun() {
        yield return new WaitForSeconds(1/fireRate);
        gunLoaded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp")) {
            switch (collision.GetComponent<PowerUp>().powerUpType) {
                case PowerUp.PowerUpType.FireRateIncrease:
                    fireRate++;
                    break;
                case PowerUp.PowerUpType.PowerShot:
                    powerShotEnabled = true;
                    break;
            }

            Destroy(collision.gameObject, 01f);
        }
    }
}
