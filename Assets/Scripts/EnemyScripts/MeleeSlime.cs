using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

public class MeleeSlime : MeleeEnemy
{
    [SerializeField] Bars hBar;
    [SerializeField] GameObject smallSlime;
    ParticleSystem ps;

    bool spawned = false;

    Animator animator;

    private void Start()
    {
        //movement
        moveSpeed = 1;
        stopTime = 3;
        moveTime = 10;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //dash
        dashSpeed = 8;
        dashCoolDown = 5;
        dashTime = 0.2f;
        pauseTime = 1.3f;

        //health
        maxHealth = 100;
        health = maxHealth;
        healthBar = hBar;
        healthBar.SetMax(maxHealth, health);

        exp = 40;

        //attack
        rotatePoint = transform.GetChild(0).gameObject;
        warning = rotatePoint.transform.GetChild(0).gameObject;
        attack = rotatePoint.transform.GetChild(1).gameObject;
        attackDistance = 1.5f;

        animator = GetComponent<Animator>();
        animator.Play("BigMeleeSlime", 0, Random.value);
        ps = GetComponent<ParticleSystem>();

        StartZ();
        StartCoroutine(Freeze());
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        distance = Vector2.Distance(player.position, transform.position);

        Look();
        Movement();
        StartCoroutine(Dash());
        StartCoroutine(MeleeAttack());

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
            if (health <= 0 && spawned == false)
            {
                spawned = true;
                Instantiate(smallSlime, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
                Instantiate(smallSlime, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                PlayerController.LevelManager(exp);
                Destroy(this.gameObject);
            }
        }
        
    }

}
