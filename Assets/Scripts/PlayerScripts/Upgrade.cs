using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : PlayerController
{

    [Header("Upgrades Controller")]
    public static bool firstUpgrade = false;

    protected static string upgrade1;
    protected static string upgrade2;
    protected static string upgrade3;

    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;

    GameObject img1;
    GameObject img2;
    GameObject img3;

    new static string selected;
    

    [Header("Upgrades")]
    public static bool bodyOfSteel = false;

    public static bool ricochet = false;

    public static bool upgraded = true;


    [Header("Upgrades")]
    protected static string[] upgrades1Array = {"BodyOfSteel", "StrongerSoul1", "BiggerSoul1", "BiggerSoul2", "Resilient1", "Resilient2", "Ricochet", "Ricochet2",
    "GodSpeed", "GodSpeed2", "GodSpeed3", "Perseverance", "Perseverance2", "ShapeShifter"};

    [SerializeField] GameObject[] upgradeImages;

    static List<string> upgrades1List = new List<string>();


    void Start()
    {
        Reset();
    }
    void OnEnable()
    {
        RandomUpgrade();
    }




    new public static void Reset()
    {
        upgrades1List.Clear();
        foreach (string s in upgrades1Array)
        {
            upgrades1List.Add(s);           
        }

        bodyOfSteel = false;
        firstUpgrade = false;
        upgraded = false;
        ricochet = false;
    }

    public void UpgradeOne()
    {  
        
        selected = upgrade1;

        UpgradeFilter();

        DestroyImages();

        levelScene.SetActive(false);
        bars.SetActive(true);
        Time.timeScale = 1.0f;
        firstUpgrade = true;
        upgraded = true;
        upgrades -= 1;
    }

    public void UpgradeTwo()
    {

        selected = upgrade2;

        UpgradeFilter();

        DestroyImages();

        levelScene.SetActive(false);
        bars.SetActive(true);
        Time.timeScale = 1.0f;
        firstUpgrade = true;
        upgraded = true;
        upgrades -= 1;
    }

    public void UpgradeThree()
    {

        selected = upgrade3;

        UpgradeFilter();

        DestroyImages();

        levelScene.SetActive(false);
        bars.SetActive(true);
        Time.timeScale = 1.0f;
        firstUpgrade = true;
        upgraded = true;
        upgrades -= 1;
    }

    void RandomUpgrade()
    {
        if (firstUpgrade == false)
        {
            upgrade1 = "BodyOfSteel";
            UpgradeImages(button1, upgrade1);
            upgrade2 = "BodyOfSteel";
            UpgradeImages(button2, upgrade2);
            upgrade3 = "BodyOfSteel";
            UpgradeImages(button3, upgrade3);

        }
        else if (firstUpgrade == true)
        {
            int value = Random.Range(0, upgrades1List.Count);
            upgrade1 = upgrades1List[value];
            UpgradeImages(button1, upgrade1);

            value = Random.Range(0, upgrades1List.Count);
            upgrade2 = upgrades1List[value];
            UpgradeImages(button2, upgrade2);

            value = Random.Range(0, upgrades1List.Count);
            upgrade3 = upgrades1List[value];
            UpgradeImages(button3, upgrade3);
        }
    }


    void UpgradeFilter()
    {
        switch (selected)
        {
            case ("BodyOfSteel"):
                bodyOfSteel = true;
                upgrades1List.Remove(selected);
                break;

            case ("BiggerSoul1"):
                maxEssence += 15;
                essenceBar.SetMax(maxEssence, soulEssence);
                upgrades1List.Remove(selected);
                break;

            case ("BiggerSoul2"):
                maxEssence += 15;
                essenceBar.SetMax(maxEssence, soulEssence);
                upgrades1List.Remove(selected);
                break;

            case ("StrongerSoul1"):
                regenAmmount += 10;
                essenceBar.SetMax(maxEssence, soulEssence);
                upgrades1List.Remove(selected);
                break;

            case ("Resilient1"):
                maxHealth += 20;
                health += 20;
                healthBar.SetMax(maxHealth, health);
                upgrades1List.Remove(selected);
                break;

            case ("Resilient2"):
                maxHealth += 20;
                health += 20;
                healthBar.SetMax(maxHealth, health);
                upgrades1List.Remove(selected);
                break;

            case ("Ricochet"):
                ricochet = true;
                Soulfire.ricochetCount++;
                upgrades1List.Remove(selected);
                break;

            case ("Ricochet2"):
                ricochet = true;
                Soulfire.ricochetCount++;
                upgrades1List.Remove(selected);
                break;

            case ("GodSpeed"):
                PlayerController.moveSpeed += 0.2f;
                upgrades1List.Remove(selected);
                break;

            case ("GodSpeed2"):
                PlayerController.moveSpeed += 0.2f;
                upgrades1List.Remove(selected);
                break;

            case ("GodSpeed3"):
                PlayerController.moveSpeed += 0.2f;
                upgrades1List.Remove(selected);
                break;

            case ("Perseverance"):
                PlayerController.invincibility += 0.5f;
                upgrades1List.Remove(selected);
                break;

            case ("Perseverance2"):
                PlayerController.invincibility += 0.5f;
                upgrades1List.Remove(selected);
                break;

            case ("ShapeShifter"):
                PlayerController.swapTime -= 5;
                upgrades1List.Remove(selected);
                break;


        }
    }

    void UpgradeImages(GameObject button, string selected)
    {
        switch (selected)
        {
            case ("BodyOfSteel"):
                Instantiate(upgradeImages[0], button.transform.position, Quaternion.identity, button.transform);
                break;

            case ("BiggerSoul1"):
                Instantiate(upgradeImages[2], button.transform.position, Quaternion.identity, button.transform);
                break;

            case ("BiggerSoul2"):
                Instantiate(upgradeImages[2], button.transform.position, Quaternion.identity, button.transform); ;
                break;

            case ("StrongerSoul1"):
                Instantiate(upgradeImages[3], button.transform.position, Quaternion.identity, button.transform);
                break;

            case ("Resilient1"):
                Instantiate(upgradeImages[1], button.transform.position, Quaternion.identity, button.transform);
                break;

            case ("Resilient2"):
                Instantiate(upgradeImages[1], button.transform.position, Quaternion.identity, button.transform);
                break;

            case ("Ricochet"):
                Instantiate(upgradeImages[4], button.transform.position, Quaternion.identity, button.transform);
                break;

            case ("Ricochet2"):
                Instantiate(upgradeImages[4], button.transform.position, Quaternion.identity, button.transform);
                break;

            case ("Perseverance"):
                Instantiate(upgradeImages[5], button.transform.position, Quaternion.identity, button.transform);
                break;

            case ("Perseverance2"):
                Instantiate(upgradeImages[5], button.transform.position, Quaternion.identity, button.transform);
                break;

            case ("ShapeShifter"):
                Instantiate(upgradeImages[6], button.transform.position, Quaternion.identity, button.transform);
                break;

            case ("GodSpeed"):
                Instantiate(upgradeImages[7], button.transform.position, Quaternion.identity, button.transform);
                break;

            case ("GodSpeed2"):
                Instantiate(upgradeImages[7], button.transform.position, Quaternion.identity, button.transform);
                break;

            case ("GodSpeed3"):
                Instantiate(upgradeImages[7], button.transform.position, Quaternion.identity, button.transform);
                break;
        }
    }

    void DestroyImages()
    {
        img1 = button1.transform.GetChild(0).gameObject;
        img2 = button2.transform.GetChild(0).gameObject;
        img3 = button3.transform.GetChild(0).gameObject;

        GameObject.Destroy(img1);
        GameObject.Destroy(img2);
        GameObject.Destroy(img3);
    }

}
