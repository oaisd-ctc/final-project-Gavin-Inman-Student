using System.Collections;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public static bool isMoving;
    public static float moveSpeed = 1.5f;
    protected Rigidbody2D rb;
    public static GameObject wisp;
    public static GameObject soulKnight;
    public static bool flip = false;

    [Header("Locks")]
    public static bool moveLocked = false;
    public static bool dashLocked = false;
    public static bool fireLocked = false;
    public static bool swapLocked = false;

    [Header("Swap")]
    protected static bool swapped = true;
    protected static bool isSwapping;
    protected static bool canSwap = true;
    protected static float swapTime = 5;
    public static GameObject selected;


    [Header("Dash")]
    public static bool isDashing;
    protected static bool canDash;
    protected static float dashSpeed = 8;
    protected static float dashTime = 0.2f;
    protected static float dashCoolDown = 1;

    [Header("Look")]
    new protected Camera camera;
    protected GameObject rotationPoint;

    [Header("SoulFire")]
    protected Transform spawnPoint;
    public static bool isCasting = false;
    protected static bool canShoot = true;
    protected static float soulFireDamage = 25;

    [Header("SoulEssence")]
    public Bars essenceBar;
    protected static bool canRegen;
    protected static float maxEssence = 50;
    protected static float soulEssence;
    protected static float regenAmmount = 5;
    protected static float regenSpeed = 1;

    [Header("Level")]
    public static Bars levelBar;
    protected static float needed = 100;
    protected static float current;
    public static int upgrades;
    public static GameObject lvUp;
    
    public static GameObject levelScene;
    public static GameObject bars;
    public static bool levelWindow = false;


    [Header("Health")]
    public static Bars healthBar;
    protected static bool isDamaged;
    public static float maxHealth = 100;
    public static float health;
    protected static float invincibility = 0.5f;

    public static float damage;
    public static GameObject gameOver;


    //Player Movement Function that takes which player body to control and the speed. Allows the player to move.
    public void Movement()
    {    
        float movY = Input.GetAxisRaw("Vertical");
        float movX = Input.GetAxisRaw("Horizontal");
        Vector2 moveDirection = new Vector2(movX, movY).normalized;


        if (isDashing == false && isSwapping == false && moveLocked == false)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }

        if (movX == 0 && movY == 0)
        {
            isMoving = false;
        }

        else if (movX > 0 || movX < 0 || movY > 0 || movY < 0)
        {
            isMoving = true;
        }

        if (movX < 0)
        {
            flip = true;
        }

        else if (movX > 0)
        { 
            flip = false; 
        }
    }

    //Player Dash Function that takes which player body to control, how fast, how long, and the cooldown for the dash. Allows the player to dash.
    public IEnumerator Dash()
    {
        float movY = Input.GetAxisRaw("Vertical");
        float movX = Input.GetAxisRaw("Horizontal");
        Vector2 moveDirection = new Vector2(movX, movY).normalized;

        if (canDash == true && isMoving == true && dashLocked == false)
        {
            canDash = false;
            isDashing = true;
            rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
            yield return new WaitForSeconds(dashTime);
            isDashing = false;
            yield return new WaitForSeconds(dashCoolDown);
            canDash = true;
        }
    }

    //Player Look Function that takes a camera and gameobject. Rotates the gameobject to face the cursor of the player.
    public void Look()
    {        
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        rotationPoint.transform.right = direction;

    }

    //Instantiates SoulFire Ranged Attack.
    public IEnumerator SoulFire(GameObject cast)
    {
        if (isCasting  == false && fireLocked == false)
        {
            SoulEssenceManager(20);
            if (canShoot == true)
            {
                isCasting = true;
                damage = soulFireDamage;
                Instantiate(cast, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(1);
                isCasting = false;
            }
        }
        
    }

    //Swap Bodys
    public IEnumerator Swap()
    {
        if (Input.GetKey(KeyCode.Tab) && isDashing == false && isDamaged == false && isCasting == false && Upgrade.bodyOfSteel == true && swapLocked == false)
        {
            if (swapped == true && canSwap == true)
            {
                wisp.transform.position = soulKnight.transform.position;
                canSwap = false;
                isSwapping = true;
                swapped = false;
                selected = wisp;
                soulKnight.SetActive(false);
                wisp.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                isSwapping = false;
                canDash = true;
                yield return new WaitForSeconds(swapTime);
                
                canSwap = true;
            }
            else if (swapped == false && canSwap == true)
            {
                soulKnight.transform.position = wisp.transform.position;
                canSwap = false;
                isSwapping = true;
                swapped = true;
                selected = soulKnight;
                wisp.SetActive(false);
                canRegen = true;
                soulKnight.SetActive(true);
                SoulKnight.canAttack = true;
                yield return new WaitForSeconds(0.5f);
                isSwapping = false;
                canDash = true;
                yield return new WaitForSeconds(swapTime);
                
                canSwap = true;
            }
        }
        
    }

    public IEnumerator HealthManager(float healingFactor, float damage)
    {
        if(isDamaged == false)
        {
            isDamaged = true;
            healthBar.SetMax(maxHealth, health);
            health -= damage;
            healthBar.SetAmmount(health);
            if (health - damage <= 0)
            {
                Death();
            }
            if(healingFactor > 0)
            {
                health += healingFactor;
            }
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            yield return new WaitForSeconds(invincibility);
            isDamaged = false;
        }
        
    }

    public void SoulEssenceManager(float cost)
    {
        essenceBar.SetMax(maxEssence, soulEssence);
        if ((soulEssence - cost) < 0)
        {
            canShoot = false;
        }
        else if (soulEssence - cost >= 0 && isCasting == false)
        {
            canShoot = true; 
            soulEssence -= cost;
            essenceBar.SetAmmount(soulEssence);
              
        }
    }

    public static void LevelManager(float ammount)
    {
        levelBar.SetMax(needed, ammount);
        current += ammount;
        levelBar.SetAmmount(current);
        ammount = 0;
        if (current >= needed)
        {
            current -= needed;
            needed += 15;
            levelBar.SetAmmount(current);
            upgrades += 1;
        }
    }


    public static void LevelScene()
    {
        if (upgrades > 0 && Input.GetKey(KeyCode.R))
        {
            if (Upgrade.upgraded == true)
            {
                Upgrade.upgraded = false;               
                lvUp.SetActive(false);
            }
            levelWindow = true;
            Time.timeScale = 0;
            levelScene.SetActive(true);
            bars.SetActive(false);
            
            
        }

    }

    public IEnumerator EssenceRegen()
    {
        if(canRegen == true)
        {
            if (soulEssence + regenAmmount >= maxEssence)
            {
                soulEssence = maxEssence;
                essenceBar.SetAmmount(soulEssence);
                yield break;
            }
            else if (soulEssence + regenAmmount <= maxEssence)
            {
                canRegen = false; 
                soulEssence = soulEssence + regenAmmount;
                essenceBar.SetAmmount(soulEssence);
                yield return new WaitForSeconds(regenSpeed);  
                canRegen = true;       
            }
        }
        
    }

    public void Death()
    {
        Time.timeScale = 0;
        dashLocked = true;
        fireLocked = true;
        moveLocked = true;
        swapLocked = true;
        gameOver.SetActive(true);
    }

    public static void Reset()
    {

        moveSpeed = 1.5f;
        flip = false;

        moveLocked = false;
        dashLocked = false;
        fireLocked = false;
        swapLocked = false;


        swapped = true;

        canSwap = true;
        swapTime = 5;

        dashSpeed = 8;
        dashTime = 0.2f;
        dashCoolDown = 1;

        isCasting = false;
        canShoot = true;
        soulFireDamage = 35;

        upgrades = 0;

        maxEssence = 50;
        regenAmmount = 5;
        regenSpeed = 1;
        needed = 100;
        levelWindow = false;
        maxHealth = 100;
        health = maxHealth;
        invincibility = 0.5f;
}

    public static void Initialize()
    {
        canDash = true;
        SoulKnight.canAttack = true;
        isDamaged = false;
    }

}
