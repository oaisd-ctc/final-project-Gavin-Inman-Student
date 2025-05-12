using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{

    //GateController fields
    Animator gateAnim;
    EdgeCollider2D gateEdge;

    GameObject trigger;

    private void Start()
    {
        gateAnim = GetComponent<Animator>();

        trigger = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        DoorOpen();
    }

    void DoorOpen()
    {
        if (GameManager.roomComplete)
        {
            gateAnim.SetBool("RoomComplete", true);
            trigger.SetActive(true);
        }
    }
}
