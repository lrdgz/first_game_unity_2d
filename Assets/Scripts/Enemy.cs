using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Transform player;
    [SerializeField] int health = 1;
    [SerializeField] float speed = 1;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;    
    }

    private void Update()
    {
        Vector2 direction = player.position - transform.position;
        transform.position += (Vector3)direction * Time.deltaTime * speed;
    }

    public void TakeDamage() {
        health--;
    }
}
