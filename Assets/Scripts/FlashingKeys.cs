using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class FlashingKeys : MonoBehaviour
{
    UnityEngine.UI.Image img;
    bool change = true;
    [SerializeField] bool mouseClick;
    [SerializeField] GameObject click;


    private void Update()
    {
        StartCoroutine(Flash());
    }
    IEnumerator Flash()
    {
        img = GetComponent<UnityEngine.UI.Image>();
        

        if (change)
        {
            change = false;
            img.color = new Color32(150, 150, 150, 255);
            if(mouseClick) { click.SetActive(true); }
            yield return new WaitForSeconds(1.6f);
            img.color = new Color32(255, 255, 255, 255);
            if (mouseClick) { click.SetActive(false); }
            yield return new WaitForSeconds(1.6f);
            change = true;
        }
    }
}
