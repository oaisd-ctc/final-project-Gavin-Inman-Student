using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedSlime : RangedEnemy
{
    [SerializeField] Bars hBar;
    [SerializeField] GameObject arrow;
    [SerializeField] float dist;
    Animator animator;
    ParticleSystem ps;

    private void Start()
    {
        //movement
        moveSpeed = 1.2f;
        followDistance = 6f;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //dash
        dashSpeed = 8;
        dashCoolDown = 5;
        dashTime = 0.2f;
        //health
        maxHealth = 80;
        health = maxHealth;
        healthBar = hBar;
        healthBar.SetMax(maxHealth, health);

        exp = 70;

        //attack
        rotatePoint = this.transform.GetChild(0).gameObject;
        spawnPoint = rotatePoint.transform.GetChild(0);
        sps = 2.5f;
        attackDistance = 3;

        animator = GetComponent<Animator>();
        animator.Play("RangedSlime", 0, Random.value);
        ps = GetComponent<ParticleSystem>();

        StartZ();
        StartCoroutine(Freeze());

    }


    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        distance = Vector2.Distance(player.position, transform.position);

        dist = distance;
        
        Run();
        Follow();   
        Look();    
        StartCoroutine(RangedAttack(arrow));

        if (isMoving)
        {
            ps.Play();
        }
        else if (isMoving == false)
        {
            ps.Pause();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("pAttack"))
        {
            StartCoroutine(HealthManager(PlayerController.damage));
            if (health <= 0)
            {
                Death();
            }
        }

    }
}
