using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointer : MonoBehaviour
{
    protected bool canFade = true;
    protected bool canUnFade = true;

    protected static List<GameObject> fades = new List<GameObject>();

    [SerializeField] GameObject character;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RayCast();
    }

    void RayCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0, 0, 1));
       
        if (hit.collider.CompareTag("Player"))
        {
            StartCoroutine(UnFade());
            SetLayerOver();
            
        }

        if (hit.collider.CompareTag("Enemy"))
        {
            SetLayerOver();
        }

        if (hit.collider.CompareTag("Pillar"))
        {
            StartCoroutine(Fade(hit.collider.gameObject));
            SetLayerBetween();

        }

        if (hit.collider.CompareTag("PillarUnder"))
        {
            StartCoroutine(Fade(hit.collider.gameObject));
            SetLayerUnder();

        }

    }

    IEnumerator Fade(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        canUnFade = true;
        if (canFade == true && character.CompareTag("Player"))
        {
            canFade = false;
            fades.Add(obj);
            sr.color = new Color(1, 1, 1, 0.75f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 0.25f);
            yield return new WaitForSeconds(0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator UnFade()
    {
        if (canUnFade == true)
        {

            canUnFade = false;
            if (fades != null)
            {
                foreach (GameObject go in fades)
                {
                    SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                    sr.color = new Color(1, 1, 1, 0.5f);
                    yield return new WaitForSeconds(0.2f);
                    sr.color = new Color(1, 1, 1, 0.75f);
                    yield return new WaitForSeconds(0.2f);
                    sr.color = new Color(1, 1, 1, 1);
                    yield return new WaitForSeconds(0.2f);
                    yield return new WaitForSeconds(0.2f);
                }
                fades.Clear();
                canFade = true;
            }
            else if (fades == null)
            {
                canFade = true;
                yield break;
            }
        }
    }

    void SetLayerUnder()
    {
        SpriteRenderer sr = character.GetComponent<SpriteRenderer>();
        Canvas canvas = character.GetComponentInChildren<Canvas>();

        if (canvas != null) 
        {
            canvas.sortingOrder = 5;
        }
        sr.sortingOrder = 5;
    }

    void SetLayerBetween()
    {
        SpriteRenderer sr = character.GetComponent<SpriteRenderer>();
        Canvas canvas = character.GetComponentInChildren<Canvas>();

        if (canvas != null)
        {
            canvas.sortingOrder = 7;
        }
        sr.sortingOrder = 7;
    }

    void SetLayerOver()
    {
        SpriteRenderer sr = character.GetComponent<SpriteRenderer>();
        Canvas canvas = character.GetComponentInChildren<Canvas>();

        if (canvas != null)
        {
            canvas.sortingOrder = 9;
        }
        sr.sortingOrder = 9;
    }
}
