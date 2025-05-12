using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //TypeWriter
    TextMeshProUGUI tText;
    string[] dialogueArray = {"Oh...?", "How Did You Get Here?", "Souls Do Not Belong In This Realm.", "...", "Well Dont Just Float There!", "break", "Good...", "Souls Are Very Differnt From Physical Beings.",
    "Souls Activly Accumulate And Consume Soul Essence.", "Soul Essence Allows Souls Powers Not Used By The Physical.", "Try Using SoulFire For Example.", "break",
    "Good!", "Using Soul Magic Requires The Consumption Of Soul Essenece.", "You Can See How Much Soul Essence You Have On The Blue Bar.", "...", "Souls Lose Most Abilties They Had While Alive.", "However...They Can Still Dash.", "Dashing Is Essential For Survival.",  "Give It A Try...", "break", "Great!",
    "Wait...Whats That Sound?", "Slimes!", "Use SoulFire To Defend YourSelf!", "break", "Well Done.", "Your Health Can Be Seen On The Red Bar.", "I Have Taken The Liberty Of Healing Any Damage Done.", "However...This Wont Happen Further On, So Be More Carful.", "As You Can See, Souls Consume Defeated Enemys Pure Soul Essence.", 
    "When A Soul Consumes Pure Soul Essence, They Grow And Evolve.", "Think Carefully And Hard About Which Upgrades To Choose.", "When You Level Up, You Will See Text Above The Blue Bar.", "This Means You Can Select A Upgrade.", "break",
    "Your New Abilty Seems To Allow You To Use Generated Soul Essence To Create A Physical Form.", "Why Dont You Give it A Try?", "break", "It Seems This New Abilty Allows You To Attack Physically At No Cost.", "However...You Consume The Same Ammount Of Soul Essence As You Take In.",
    "It Seems You Need To Decide When You Should Take On Which Form...", "Note You Are MUCH WEAKER In Soul Form And Take More Damage.", "Why Dont You Give Your Sword A Few Swings Before Moving On?", "break", "...", "Well Little Spark Thats About All I Have To Teach You...",
    "The Doors Leading Through This Place Should Open Once You Defeat Whatever Lies Inside.", "Be Carful...We Sha'll See How Far You Can Go."};
    List<string> dialogue = new List<string>();
    string newText;
    int track;
    float textTypeTime = 0.1f; //0.1
    float interWordWaitTime = 3f; //3
    public static bool tutorialStart = false;
    bool doneTyping = true;
    bool next = false;

    //Tutorial Images
    [SerializeField] GameObject text;
    [SerializeField] GameObject moveKey;
    [SerializeField] GameObject spaceKey;
    [SerializeField] GameObject levelKey;
    [SerializeField] GameObject attackKey;
    [SerializeField] GameObject tabKey;
    [SerializeField] GameObject eKey;

    [SerializeField] GameObject tCanvas;

    [SerializeField] Transform spawn;
    [SerializeField] Transform spawn2;
    [SerializeField] GameObject meleeSlime;

    bool canAdd = true;

    int tutorialScene;

    bool skipping = false;
    bool skipped = false;

    private void Awake()
    {
        PlayerController.dashLocked = true;
        PlayerController.fireLocked = true;
        PlayerController.moveLocked = true;
        PlayerController.swapLocked = true;

        tText = text.GetComponentInChildren<TextMeshProUGUI>();

        PlayerController.soulKnight.gameObject.SetActive(false);
        PlayerController.wisp.gameObject.SetActive(true);
    }

    private void Update()
    {
        StartCoroutine(Tutorial());
        StartCoroutine(Skip());
    }

    public IEnumerator Tutorial()
    {
        switch (tutorialScene)
        {
            case 0:
                {
                    if (tutorialStart == true)
                    {
                        tutorialStart = false;
                        foreach (string s in dialogueArray)
                        {
                            dialogue.Add(s);
                        }
                        StartCoroutine(TypeWriter());                       
                    }
                    if (doneTyping == true)
                    {
                        doneTyping = false;
                        tutorialScene++;
                        moveKey.SetActive(true);
                        text.SetActive(false);
                        PlayerController.moveLocked = false;
                        next = true;

                    }
                    break;
                }
            case 1:
                {
                    if (next && PlayerController.isMoving)
                    {      
                        next = false;
                        moveKey.SetActive(false);
                        text.SetActive(true);
                        StartCoroutine(TypeWriter());
                    }
                    if (doneTyping == true)
                    {
                        doneTyping = false;
                        tutorialScene++;  
                        eKey.SetActive(true);
                        text.SetActive(false);
                        PlayerController.fireLocked = false;
                        next = true;
                    }
                    break;
                }

            case 2:
                {
                    if (next && PlayerController.isCasting)
                    {     
                        next = false;
                        StartCoroutine(TypeWriter());
                        eKey.SetActive(false);
                        text.SetActive(true);
                    }
                    if (doneTyping == true)
                    {
                        doneTyping = false;
                        tutorialScene++;
                        spaceKey.SetActive(true);
                        text.SetActive(false);
                        PlayerController.dashLocked = false; 
                        next = true;
                    }
                    break;
                }

            case 3:
                {
                    if (next && PlayerController.isDashing == true)
                    {
                        next = false;
                        StartCoroutine(TypeWriter());
                        spaceKey.SetActive(false);
                        text.SetActive(true);
                    }
                    if (doneTyping == true)
                    {
                        doneTyping = false;
                        tutorialScene++;
                        text.SetActive(false);
                        Instantiate(meleeSlime, spawn.position, Quaternion.identity);
                        Instantiate(meleeSlime, spawn2.position, Quaternion.identity);
                        next = true;
                    }
                    break;
                }

            case 4:
                {
                    bool added;
                    if (GameManager.defeatedEnemys == 3 && canAdd)
                    {
                        canAdd = false;
                        PlayerController.LevelManager(80);
                        PlayerController.health = PlayerController.maxHealth;
                        PlayerController.healthBar.SetMax(PlayerController.maxHealth, PlayerController.health);
                        added = true;
                    }
                    else
                    {
                        added = false;
                    }
                    if (next && added)
                    {
                        next = false;
                        yield return new WaitForSeconds(0.2f);
                        GameManager.defeatedEnemys = -1;
                        StartCoroutine(TypeWriter());
                        text.SetActive(true);
                    }
                    if (doneTyping == true)
                    {
                        doneTyping = false;
                        tutorialScene++;
                        text.SetActive(false);
                        levelKey.SetActive(true);                       
                        PlayerController.swapLocked = false;
                        next = true;
                    }
                    break;
                }

            case 5:
                {
                    if (Input.GetKey(KeyCode.R) && next)
                    {
                        next = false;
                        levelKey.SetActive(false);
                        tutorialScene++;
                        tabKey.SetActive(true);
                        text.SetActive(false);
                        next = true;
                    }
                    break;
                }

            case 6:
                {
                    if (next && PlayerController.selected == PlayerController.soulKnight && Input.GetKey(KeyCode.Tab))
                    {
                        next = false;
                        StartCoroutine(TypeWriter());
                        text.SetActive(true);
                        tabKey.SetActive(false);
                    }
                    if (doneTyping == true)
                    {
                        doneTyping = false;
                        tutorialScene++;
                        text.SetActive(false);
                        attackKey.SetActive(true);
                        next = true;
                    }
                    break;
                }

            case 7:
                {
                    if (next && Input.GetMouseButton(0) && PlayerController.selected == PlayerController.soulKnight)
                    {
                        next = false;
                        StartCoroutine(TypeWriter());
                        attackKey.SetActive(false);
                        text.SetActive(true);
                    }
                    if (doneTyping == true)
                    {
                        doneTyping = false;
                        text.SetActive(false);
                        GameManager.roomComplete = true;
                    }
                    break;
                }
        }


    }

    public IEnumerator TypeWriter()
    {
        doneTyping = false;
        foreach (string s in dialogue)
        {
            if (s == "break")
            {
                dialogue.RemoveRange(0, track + 1);
                track = 0;
                break;
            }
            else
            {
                track++;  
                foreach (char c in s)
                {
                    newText += c;
                    tText.SetText(newText);
                    yield return new WaitForSeconds(textTypeTime);
                }

                newText = "";
                yield return new WaitForSeconds(interWordWaitTime);
            }
        }
        doneTyping = true;       
    }

    public IEnumerator Skip()
    {   
        if (Input.GetKey(KeyCode.T) && skipping == false)
        {
            skipping = true;
            yield return new WaitForSeconds(3);
            if (Input.GetKey(KeyCode.T) && skipped == false)
            {
                skipped = true;
                Destroy(tCanvas.gameObject);
                GameManager.roomComplete = true;
                PlayerController.upgrades += 1;
                PlayerController.dashLocked = false;
                PlayerController.fireLocked = false;
                PlayerController.moveLocked = false;
                PlayerController.swapLocked = false;
            }
            skipping = false;
        }
    }
}
