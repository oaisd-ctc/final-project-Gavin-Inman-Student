using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SoulKnight : PlayerController
{
    [SerializeField] Bars lBar;
    [SerializeField] Bars hBar;
    [SerializeField] GameObject soulFire;

    //MeleeAttack
    GameObject attackRange;
    public static bool canAttack;
    float attackCoolDown = 0.8f;
    protected float meleeAttackDamage = 20;
    Animator animator;
    SpriteRenderer sR;


    //Testing purposes...allows us to view the soul ammount from the inspector
    public float hp;
    public float mp;

    void Start()
    {
        //Movement
        rb = GetComponent<Rigidbody2D>();   
        animator = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();

        //Look and cast
        rotationPoint = transform.GetChild(1).gameObject;
        spawnPoint = rotationPoint.transform.GetChild(0);
        camera = Camera.main;

        //dash
        canDash = true;

        //HealthManager
        healthBar = hBar;
        isDamaged = false;


        //EssenceManager
        levelBar = lBar;
        healthBar = hBar;
        isCasting = false;

        //Attack
        attackRange = rotationPoint.transform.GetChild(1).gameObject;

    }

    private void Awake()
    {
        canAttack = true;
    }

    void Update()
    {
        hp = health;
        mp = soulEssence;
        

        //Movement
        Movement();

        //Dash
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Dash());
        }

        //Look/Aim
        Look();

        //SoulFire
        if (Input.GetKey(KeyCode.E))
        {
            StartCoroutine(SoulFire(soulFire));
        }

        //MeleeAttack
        StartCoroutine(MeleeAttack());

        AnimatorController();

        LevelScene();

    }

    //Melee Attack
    IEnumerator MeleeAttack()
    {
        if(Input.GetMouseButton(0) && canAttack == true)
        {
            canAttack = false;
            damage = meleeAttackDamage;
            attackRange.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            attackRange.SetActive(false);
            yield return new WaitForSeconds(attackCoolDown);
            canAttack = true;
        }
    }

    //Melee Damage to player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("eAttack"))
        {
            StartCoroutine(HealthManager(0, 10));
        }

        if (other.gameObject.CompareTag("GreenSlimeBall"))
        {
            StartCoroutine(HealthManager(0, 5));
        }

        if (other.gameObject.CompareTag("Gate"))
        {
            LoadScene.LoadRandomRoom();
        }

        if (other.gameObject.CompareTag("BossMeleeAttack"))
        {
            StartCoroutine(HealthManager(0, 10));
        }

        if (other.gameObject.CompareTag("BossSlam"))
        {
            StartCoroutine(HealthManager(0, 20));
        }

        if (other.gameObject.CompareTag("BossSlimeBall"))
        {
            StartCoroutine(HealthManager(0, 10));
        }
    }

    void AnimatorController()
    {
        float movY = Input.GetAxisRaw("Vertical");
        float movX = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("MovX", movX);
        animator.SetFloat("MovY", movY);

        if (isMoving == true)
        {
            animator.SetBool("IsMoving", true);
        }

        else if (isMoving == false)
        {
            animator.SetBool("IsMoving", false);
        }      
        
        if (flip == true)
        {
            sR.flipX = true;
        }
        else if (flip == false)
        {
            sR.flipX = false;
        }
    }
}


