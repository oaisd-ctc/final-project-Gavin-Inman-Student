using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float projectileSpeed;
    
    void Start()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * projectileSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Terrian"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}

