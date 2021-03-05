using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float speed = 5;

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;    
    }
}
