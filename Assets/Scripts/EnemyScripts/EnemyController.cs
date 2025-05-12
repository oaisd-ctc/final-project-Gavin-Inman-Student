 using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    protected NavMeshAgent agent;

    public static Transform player;
    protected float moveSpeed;
    protected float moveTime;
    protected float stopTime;
    protected bool isStopping = false;
    protected bool canStop = true;
    protected bool isMoving;

    [Header("Dash")]
    protected bool isDashing;
    protected bool canDash;
    protected float dashSpeed;
    protected float dashTime;
    protected float dashCoolDown;
    protected float pauseTime;

    [Header("Look")]
    protected GameObject rotatePoint;
    protected GameObject warning;
    protected GameObject attack;
    protected bool canLook;

    [Header("Health")]
    protected Bars healthBar;
    protected bool isDamaged;
    public float maxHealth;
    protected float health;
    protected float invincibility = 0.5f;

    [Header("Attacks")]
    protected float attackDistance;
    protected bool attacking;
    protected float distance;
    protected float warningTime = 0.5f;

    public float exp;

    void Start()
    {
        
    }


    //movement
    public void Movement()
    {
        if (isStopping == false && isDashing == false)
        {

            agent.speed = moveSpeed;
            if(attacking == true)
            {
                agent.SetDestination(agent.transform.position);
                isMoving = false;
            }
            else if (attacking == false)
            {
                agent.SetDestination(player.position);
                isMoving = true;
            }
        }
    }

    public IEnumerator Stop()
    {
        if (canStop == true && isDashing == false)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            canStop = false;
            yield return new WaitForSeconds(moveTime);
            isStopping = true;
            rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(stopTime);
            isStopping = false;
            canStop = true;
        }
    }

    //faces player
    public void Look()
    {
        if (canLook == true)
        {
            Vector3 dist = player.position - transform.position;
            float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg + 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            rotatePoint.transform.rotation = Quaternion.Slerp(transform.rotation, q, 180);
        }
    }


    //Navagates damage dealt
    public IEnumerator HealthManager(float damage)
    {
        if (isDamaged == false)
        {
            isDamaged = true;
            health -= damage;
            healthBar.SetAmmount(health);
            yield return new WaitForSeconds(invincibility);
            isDamaged = false;
        }

    }

    //Destroys Object
    public void Death()
    {
        if (health <= 0)
        {
            PlayerController.LevelManager(exp);
            GameManager.defeatedEnemys++;
            GameObject.Destroy(this.gameObject);
        }
    }

    //pause enemys on spawn
    public IEnumerator Freeze()
    {
        canLook = true;
        canDash = false;
        isStopping = true;
        attacking = true;
        yield return new WaitForSeconds(1);
        canDash = true;
        isStopping = false;
        attacking = false;
        isMoving = false;
    }

    //Dashes at player
    public IEnumerator Dash()
    {
        if (canDash == true && isStopping == false)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            canDash = false;
            isDashing = true;
            isStopping = true;
            canLook = false;
            if (isDashing)
            {
                agent.speed = 0;
                Vector2 pos = new Vector2((player.position.x - transform.position.x), (player.position.y - transform.position.y)).normalized;
                rb.velocity = (pos * dashSpeed);
            }
            yield return new WaitForSeconds(dashTime);
            isDashing = false;
            rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(pauseTime);
            agent.speed = moveSpeed;
            canLook = true;
            isStopping = false;
            yield return new WaitForSeconds(dashCoolDown);
            canDash = true;
        }
    }

    public void StartZ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 8);
    }
}
