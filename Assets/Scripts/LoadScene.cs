using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Adds a load scene function to buttons
    public static void LoadRandomRoom()
    {
        if (GameManager.roomNum == 3)
        {
            SceneManager.LoadScene("BossRoom");
            PlayerController.levelBar.gameObject.SetActive(false);
        }

        else
        {
            int rnd = Random.Range(0, GameManager.scenes.Count);
            string sceneName = GameManager.scenes[rnd];
            GameManager.scenes.Remove(sceneName);
            SceneManager.LoadScene(sceneName);
            GameManager.roomNum++;
        }
    }

    public void Load(string sceneName)
    {
        if (sceneName == "TutorialRoom")
        {
            TutorialManager.tutorialStart = true;
            Upgrade.Reset();
            PlayerController.Reset();
            GameManager.Reset();
            GameManager.healthBar.SetMax(PlayerController.maxHealth, PlayerController.health);
            StartCoroutine(Wait());
        }
            
        if (sceneName == "MainMenu")
        {
            GameManager.playerCanvas.SetActive(false);
            Destroy(GameManager.soulKnight.gameObject);
            Destroy(GameManager.wisp.gameObject);
            Destroy(GameManager.playerCanvas.gameObject);
            Destroy(GameManager.manager.gameObject);
            
        }       

        GameManager.gameOver.SetActive(false);
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        StartCoroutine(Wait());
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
        GameManager.playerCanvas.SetActive(false);
        Destroy(GameManager.soulKnight.gameObject);
        Destroy(GameManager.wisp.gameObject);
        Destroy(GameManager.playerCanvas.gameObject);
        Destroy(GameManager.manager.gameObject);
        SceneManager.LoadScene("TutorialRoom");
        TutorialManager.tutorialStart = true;
        Upgrade.Reset();
        PlayerController.Reset();
        GameManager.Reset();
        GameManager.healthBar.SetMax(PlayerController.maxHealth, PlayerController.health);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.soulKnight.SetActive(false);
        GameManager.wisp.SetActive(true);
    }
    
}
