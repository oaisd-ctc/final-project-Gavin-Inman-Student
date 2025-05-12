using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    [SerializeField] float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 dist = EnemyController.player.position - transform.position;
        float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg - 0;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, q, 180);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 pos = new Vector2((EnemyController.player.position.x - transform.position.x), (EnemyController.player.position.y - transform.position.y)).normalized;
        rb.velocity = pos * projectileSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Terrian"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
