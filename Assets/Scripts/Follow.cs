using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] bool isShadow;
    [SerializeField] GameObject shadow;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isShadow)
        {
            transform.position = EnemyController.player.position - new Vector3(0, 0.5f, 0);
        }

        else if (isShadow == false)
        {
            transform.position = shadow.transform.position;
        }
    }
}
