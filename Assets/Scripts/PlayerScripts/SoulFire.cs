using System.Collections;
using UnityEngine;

public class Soulfire : MonoBehaviour
{
    
    [SerializeField] float projectileSpeed;

    public static int ricochetCount = 0;
    int collisionCount = 0;
    bool hit;
    bool bounced;

    Camera camera;
    void Start()
    {
        
        //sets spawn and velocity of projetile on spawn
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        Vector2 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }


    private void Update()
    {

        if (collisionCount > ricochetCount)
        {
            Destroy(this.gameObject);
        }
    }


    //Destroys on contact, uses trigger instead of collision due to lack of realistic colliders
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Terrian") && Upgrade.ricochet == false)
        {
            Destroy(this.gameObject);
        }

        else if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrian") && Upgrade.ricochet == true)
        {
            hit = true;
            StartCoroutine(Bounce());
        }
    }

    IEnumerator Bounce()
    {
        if (!bounced && hit)
        {
            bounced = true;
            if (hit)
            {
                hit = false;
                collisionCount++;    
            }
            yield return new WaitForSeconds(0.2f);
            bounced = false;
        }
    }
}

