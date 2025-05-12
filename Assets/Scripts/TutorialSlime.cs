using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TutorialSlime : MeleeEnemy
{
    [SerializeField] Bars hBar;
    Animator animator;
    ParticleSystem ps;

    bool added = false;

    bool inv = true;

    private void Start()
    {
        //movement
        moveSpeed = 1.5f;
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
        maxHealth = 30;
        health = maxHealth;
        healthBar = hBar;
        healthBar.SetMax(maxHealth, health);

        exp = 10;

        //attack
        rotatePoint = transform.GetChild(0).gameObject;
        warning = rotatePoint.transform.GetChild(0).gameObject;
        attack = rotatePoint.transform.GetChild(1).gameObject;
        attackDistance = 1.5f;

        animator = GetComponent<Animator>();
        animator.Play("SmallMeleeSlime", 0, UnityEngine.Random.value);
        ps = GetComponent<ParticleSystem>();

        StartZ();
        StartCoroutine(Freeze());
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        distance = Vector2.Distance(player.position, transform.position);

        StartCoroutine(Inv());
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
        if (other.gameObject.CompareTag("pAttack") && inv == false)
        {
            StartCoroutine(HealthManager(PlayerController.damage));
            if (health <= 0 && added == false)
            {
                added = true;
                GameManager.defeatedEnemys += 1f;
                PlayerController.LevelManager(exp);
                Destroy(this.gameObject);
            }
        }

    }

    IEnumerator Inv()
    {
        yield return new WaitForSeconds(0.8f);
        inv = false;
    }

}

