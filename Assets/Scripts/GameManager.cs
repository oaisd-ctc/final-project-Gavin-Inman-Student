using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PlayerController
{
    public static GameManager instance;
    
    //Player Controller
    [SerializeField] GameObject knight;
    [SerializeField] GameObject pWisp;

    [SerializeField] GameObject level;
    [SerializeField] public GameObject levelUp;
    [SerializeField] public GameObject bar;
    [SerializeField] public GameObject go;

    public static List<GameObject> spawners = new List<GameObject>();

    public Bars hBar;
    new public static Bars healthBar;

    [SerializeField] GameObject canvas;
    public static GameObject playerCanvas;
    public static GameObject manager;

    //round number
    public static int roundCount =0;
    public static int rounds;
    
    //Which Spawner to Activate
    public static int sl = 0;
   
    //bool controller for telling if to end room or go to next round
    public static bool roomComplete = false;
    public static bool roundComplete = false;
    public static bool nextRoom = false;
    public static int roomNum = 0;

    //tells if all enemys have been killed
    public static int requiredEnemys;
    public static float defeatedEnemys = -1;

    //Scene Controll
    public static string[] scenesArray = { "StorageRoom", "SqaureRoom", "ZigRoom"};
    public static List<string> scenes = new List<string>();


    void Start()
    {
        //sets Intial health and essence
        soulEssence = maxEssence;
        health = maxHealth;
        canRegen = true;

        roomComplete = true;

        manager = this.gameObject;

        bars = bar;
        lvUp = levelUp;
        levelScene = level;

        healthBar = hBar;

        PlayerController.gameOver = go;

        SoulKnight.canAttack = true;
        
        foreach (string s in scenesArray)
        {
            scenes.Add(s);
        }
    }

    private void Awake()
    {
        //The two player chars
        soulKnight = knight;
        wisp = pWisp;

        selected = wisp;

        playerCanvas = canvas;

        DontDestroyOnLoad(soulKnight);
        DontDestroyOnLoad(wisp);
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(canvas);

        soulKnight.SetActive(false);
        wisp.SetActive(false);
        canvas.gameObject.SetActive(false);  
    }

    void Update()
    {
        //controlls which player
        StartCoroutine(Swap());

        //tells if the room is complete and character should move on
        StartCoroutine(RoundManager());

        if (PlayerController.upgrades > 0)
        {
            levelUp.SetActive(true);
        }
        else if (PlayerController.upgrades <= 0)
        {
            levelUp.SetActive(false);
        }

        Debug.Log(PlayerController.upgrades);
    }

    
    IEnumerator RoundManager()
    {
        if (roundComplete == false && roomComplete == false)
        {
            roundComplete = true;
            defeatedEnemys = 1;
            yield return new WaitForSeconds(1);
            if (roundCount < rounds && spawners[sl] != null)
            {
                spawners[sl].SetActive(true);
                sl++;
            }
            requiredEnemys = Spawner.enemyCount;
            defeatedEnemys = 0;
            yield return new WaitForSeconds(0.2f);
        }    
        
        else if (defeatedEnemys == requiredEnemys)
        {
            defeatedEnemys = 1;
            roundComplete = false;
            roundCount++;
        }

        if (roundCount == rounds)
        {
            roomComplete = true;
        }

    }

    public static void PlayerManager()
    {

        if (selected == wisp)
        {
            wisp.SetActive(true);
            soulKnight.SetActive(false);
            swapped = false;
        }
        else if (selected == soulKnight)
        {
            soulKnight.SetActive(true);
            SoulKnight.canAttack = true;
            wisp.SetActive(false);
            swapped = true;
        }
    }

    public static void SpawnerGrabber()
    {
        spawners.Clear();
        GameObject[] spawnersArray;
        spawnersArray = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawn in spawnersArray)
        {
            spawners.Add(spawn);
        }
        foreach (GameObject spawn in spawners)
        {
            spawn.SetActive(false);
        }
    }


    new public static void Reset()
    {
        roundCount = 0;
        rounds = 0;

        sl = 0;

        roomComplete = false;
        roundComplete = false;
        nextRoom = false;

        defeatedEnemys = -1;

        roomNum = 0;


        soulEssence = maxEssence;
        health = maxHealth;
        canRegen = true;

        roomComplete = true;

        SoulKnight.canAttack = true;

        scenes.Clear();
        
        foreach (string s in scenesArray)
        {
            scenes.Add(s);
        }
    }
}
