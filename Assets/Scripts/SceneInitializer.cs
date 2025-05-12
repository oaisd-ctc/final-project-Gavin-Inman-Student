using TMPro;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    private void Awake()
    {
        GameManager.SpawnerGrabber();   
        PlayerController.Initialize();
        PlayerController.soulKnight.SetActive(false);
        PlayerController.wisp.SetActive(false);
        GameManager.rounds = Random.Range(1, GameManager.spawners.Count + 1);
        GameManager.roundCount = 0;
        GameManager.roomComplete = false;
        GameManager.roundComplete = false;
        GameManager.defeatedEnemys = 1;
        GameManager.sl = 0;
        PlayerController.soulKnight.transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
        PlayerController.wisp.transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
        GameManager.playerCanvas.gameObject.SetActive(true);
        GameManager.PlayerManager();
    }
}
