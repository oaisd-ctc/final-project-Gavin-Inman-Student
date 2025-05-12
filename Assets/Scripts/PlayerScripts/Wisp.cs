using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wisp : PlayerController
{
    [SerializeField] Bars hBar;
    [SerializeField] Bars lBar;
    [SerializeField] GameObject soulFire;


    //Testing purposes...allows us to view the soul ammount from the inspector
    [SerializeField] float soulAmmount;
    [SerializeField] float healthAmmount;

    //Testing purposes...allows us to view the soul ammount from the inspector
    public float hp;
    public float mp;

    void Start()
    {
        //Movement
        rb = GetComponent<Rigidbody2D>();


        //Look and cast
        rotationPoint = transform.GetChild(1).gameObject;
        spawnPoint = rotationPoint.transform.GetChild(0);
        camera = Camera.main;

        //dash
        canDash = true;
        canRegen = true;

        //HealthManager
        healthBar = hBar;
        isDamaged = false;

        //EssenceManager
        levelBar = lBar;
        healthBar = hBar;
        isCasting = false;
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

        //Essence Regen
        if (isSwapping == false)
        {
            StartCoroutine(EssenceRegen());
        }

        LevelScene();
    }

    //Melee Damage to player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("eAttack"))
        {
            StartCoroutine(HealthManager(0, 1000));
        }

        if (other.gameObject.CompareTag("GreenSlimeBall"))
        {
            StartCoroutine(HealthManager(0, 5000));
        }

        if (other.gameObject.CompareTag("Gate"))
        {
            LoadScene.LoadRandomRoom();
        }

        if (other.gameObject.CompareTag("BossMeleeAttack"))
        {
            StartCoroutine(HealthManager(0, 1000));
        }

        if (other.gameObject.CompareTag("BossSlam"))
        {
            StartCoroutine(HealthManager(0, 2000));
        }

        if (other.gameObject.CompareTag("BossSlimeBall"))
        {
            StartCoroutine(HealthManager(0, 1000));
        }
    }
}

