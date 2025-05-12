using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class SlimeKing : EnemyController
{

    new float health = 500;
    bool dashed = false;
    bool bursting = false;
    bool slamming = false;
    bool falling = false;
    bool damaged = false;

    int attackLv = 1;
    bool next = true;

    protected bool isShooting;
    [SerializeField] Bars bossBar;
    [SerializeField] GameObject win;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float sps;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject burstProjectile;
    [SerializeField] private GameObject burstRotate;
    [SerializeField] private GameObject[] burstSpawns;
    [SerializeField] private GameObject shadow;
    [SerializeField] new private GameObject warning;
    [SerializeField] private GameObject slamAttack;
    [SerializeField] new private GameObject attack;
    [SerializeField] GameObject spawn;


    private void Awake()
    {
        bossBar.SetMax(500, health);
    }
    void Start()
    {
        rotatePoint = transform.GetChild(0).gameObject;
        
        win.SetActive(false);
        bossBar.gameObject.SetActive(true);

        //dash
        dashSpeed = 10;
        dashCoolDown = 2;
        dashTime = 0.2f;
        pauseTime = 0.5f;
        canDash = true;
    }
    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (transform.position.y < warning.transform.position.y && falling == true)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, 0);
            transform.position = warning.transform.position + new Vector3 (0, 0.7f, 0);
            slamAttack.SetActive(true);
        }
        Look();
        AttackPattern();
    }

    void AttackPattern()
    {
        switch (attackLv)
        {
            case 1:
                if (next)
                {
                    next = false;
                    StartCoroutine(SlimeDash());
                    attackLv++;
                }
                break;

            case 2:
                if (next)
                {
                    next = false;
                    StartCoroutine(RangedAttack(projectile));
                    attackLv++;
                }
                break;

            case 3:
                if (next)
                {
                    next = false;
                    StartCoroutine(SlimeDash());
                    attackLv++;
                }
                break;

            case 4:
                if (next)
                {
                    next = false;
                    StartCoroutine(BurstAttack());
                    attackLv++;
                }
                break;

            case 5:
                if (next)
                {
                    next = false;
                    Spawn();
                    attackLv++;
                }
                break;

            case 6:
                if (next)
                {
                    next = false;
                    StartCoroutine(Slam());
                    attackLv = 1;
                }
                break;
        }
    }

    public IEnumerator SlimeDash()
    {
        if (dashed == false)
        {
            dashed = true;
            for (int i = 0; i < 3; i++)
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
                        Vector2 pos = new Vector2((player.position.x - transform.position.x), (player.position.y - transform.position.y)).normalized;
                        rb.velocity = (pos * dashSpeed);
                        StartCoroutine(MeleeAttack());
                    }
                    yield return new WaitForSeconds(dashTime);
                    isDashing = false;
                    rb.velocity = Vector2.zero;
                    yield return new WaitForSeconds(pauseTime);
                    canLook = true;
                    isStopping = false;
                    yield return new WaitForSeconds(dashCoolDown);
                    canDash = true;
                }
            }
            yield return new WaitForSeconds(2);
            dashed = false;
            next = true;
        }
    }

    public IEnumerator RangedAttack(GameObject projectile)
    {
        if (attacking == false && slamming == false && bursting == false)
        {
            for (int i = 0; i < 5; i++)
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
            yield return new WaitForSeconds(3);
            next = true;
        }
    }

    public IEnumerator BurstAttack()
    {
        if (bursting == false && slamming == false)
        {
            bursting = true;
            for (int i = 0; i < 2; i++)
            {
                foreach (GameObject spawn in burstSpawns)
                {
                    Instantiate(burstProjectile, spawn.transform.position, spawn.transform.rotation);
                }               
                burstRotate.transform.rotation = Quaternion.Euler(0, 0, 22.5f);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(2);
            bursting = false;
            next = true;
        }
    }

    public IEnumerator Slam()
    {
        if (slamming == false && bursting == false)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            CapsuleCollider2D cc = rb.GetComponent<CapsuleCollider2D>();

            slamming = true;
            cc.isTrigger = true;
            rb.velocity = Vector3.up * 30;
            yield return new WaitForSeconds(0.2f);
            shadow.SetActive(true);
            yield return new WaitForSeconds(5);
            warning.SetActive(true);
            shadow.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            transform.position = new Vector3 (warning.transform.position.x, 0, 0);
            falling = true;
            rb.velocity = Vector3.down * 30;            
            yield return new WaitForSeconds (1.9f);
            slamAttack.SetActive(false);
            warning.SetActive(false);
            cc.isTrigger = false;
            yield return new WaitForSeconds(3);
            falling = false;
            slamming = false;
            next = true;
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < 2; i++)
        {
            Instantiate(spawn, burstSpawns[i].transform.position, Quaternion.identity);
        }
        next = true;
    }

    public IEnumerator MeleeAttack()
    {
        if (distance <= attackDistance && attacking == false || isDashing == true)
        {
            attacking = true;
            canLook = false;

            attack.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            attack.SetActive(false);
            canLook = true;
            attacking = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("pAttack"))
        {
            if (damaged == false)
            {
                damaged = true;
                health -= PlayerController.damage;

                bossBar.SetAmmount(health);

                if (health < 0)
                {
                    Destroy(this.gameObject);
                    win.SetActive(true);
                    bossBar.gameObject.SetActive(false);
                }
                StartCoroutine(Wait());
                
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.8f);
        damaged = false;
    }
}

