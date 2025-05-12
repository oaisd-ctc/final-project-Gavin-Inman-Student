using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : EnemyController
{
    protected bool isShooting;
    protected bool isRunning;
    protected bool isFollowing;
    protected Transform spawnPoint;
    protected float sps;
    protected float followDistance;

    public IEnumerator RangedAttack(GameObject projectile)
    {
        if (isRunning == false && attacking == false && isFollowing == false)
        {
            if (isShooting == false)
            {
                attacking = true;
                yield return new WaitForSeconds(0.5f);
                isShooting = true;
                Instantiate(projectile, spawnPoint.position, Quaternion.identity);
                attacking = false;
                yield return new WaitForSeconds(sps);
                isShooting = false;
            }
        }
    }

    public void Run()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (distance <= attackDistance && isStopping == false)
        {           
            if (attacking == false)
            {
                isRunning = true;
                isMoving = true;
                Vector2 pos = new Vector2((player.position.x - transform.position.x), (player.position.y - transform.position.y)).normalized * -1;
                agent.speed = 0;
                transform.Translate(pos * moveSpeed * Time.deltaTime);
            }
        }
        else if (isDashing == false && distance >= attackDistance)
        {
            isRunning = false;
            isMoving = false;
        }
    }

    public void Follow()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (attacking == true)
        {
            agent.speed = 0;
            isMoving = false;
            return;
        }
        else if (distance >= followDistance && isStopping == false && isRunning == false )
        {
            isFollowing = true;
            isMoving = true;
            agent.speed = moveSpeed;
            Movement();
        }
        else if (distance <= followDistance && isRunning == false)
        {
            agent.speed = 0;
            isMoving = false;
            isFollowing = false;
        }
    }
}
