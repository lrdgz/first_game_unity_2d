using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float h;
    float v;

    Vector3 moveDirection;
    [SerializeField] float speed = 3;
    [SerializeField] Transform aim;
    [SerializeField] Camera camera;
    Vector2 facingDirection;
    [SerializeField] Transform bullletPrefab;
    bool gunLoaded = true;
    [SerializeField] float fireRate = 1;
    [SerializeField] int health = 10;


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
            Instantiate(bullletPrefab, transform.position, targetRotation);

            StartCoroutine(ReloadGun());

        }

    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            //Game Over
        }
    }

    IEnumerator ReloadGun() {
        yield return new WaitForSeconds(1/fireRate);
        gunLoaded = true;
    }
}
