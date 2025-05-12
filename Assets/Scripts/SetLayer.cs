using System.Collections;
using UnityEngine;
public class SetLayer : MonoBehaviour
{
    [SerializeField] int layerLevel;
    [SerializeField] bool fadeOther;
    [SerializeField] GameObject other;

    protected bool canFade = true;
    protected bool canUnFade = true;

    protected bool otherCanFade = true;
    protected bool otherCanUnFade = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {

            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            Canvas canvas = collision.GetComponentInChildren<Canvas>();

            StartCoroutine(Fade(collision));

            if (fadeOther == true)
            {
                StartCoroutine(FadeOther(collision));
            }


            if (layerLevel == 1)
            {
                if (canvas != null)
                {
                    canvas.sortingOrder = 5;
                }

                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = 5;
                }
            }

            if (layerLevel == 2)
            {
                if (canvas != null)
                {
                    canvas.sortingOrder = 7;
                }

                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = 7;
                }
            }

            if (layerLevel == 3)
            {
                if (canvas != null)
                {
                    canvas.sortingOrder = 9;
                }

                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = 9;
                }
            }
        }

        if (collision.CompareTag("Boss"))
        {

            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            Canvas canvas = collision.GetComponentInChildren<Canvas>();

            if (layerLevel == 1)
            {
                if (canvas != null)
                {
                    canvas.sortingOrder = 5;
                }

                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = 5;
                }
            }

            if (layerLevel == 2)
            {
                if (canvas != null)
                {
                    canvas.sortingOrder = 7;
                }

                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = 7;
                }
            }

            if (layerLevel == 3)
            {
                if (canvas != null)
                {
                    canvas.sortingOrder = 9;
                }

                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = 9;
                }
            }
        }

        if (collision.CompareTag("BossBody"))
        {
            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            Canvas canvas = collision.GetComponentInChildren<Canvas>();

            if (layerLevel == 1)
            {
                if (canvas != null)
                {
                    canvas.sortingOrder = 5;
                }

                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = 4;
                }
            }

            if (layerLevel == 2)
            {
                if (canvas != null)
                {
                    canvas.sortingOrder = 7;
                }

                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = 6;
                }
            }

            if (layerLevel == 3)
            {
                if (canvas != null)
                {
                    canvas.sortingOrder = 9;
                }

                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = 8;
                }
            }
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            Canvas canvas = collision.GetComponentInChildren<Canvas>();

            StartCoroutine(UnFade(collision));

            if (fadeOther == true)
            {
                StartCoroutine(UnFadeOther(collision));
            }

            if (canvas != null)
            {
                canvas.sortingOrder = 9;
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = 9;
            }
        }

        if (collision.CompareTag("Boss"))
        {
            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            Canvas canvas = collision.GetComponentInChildren<Canvas>();

            StartCoroutine(UnFade(collision));

            if (canvas != null)
            {
                canvas.sortingOrder = 11;
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = 11;
            }
        }

        if (collision.CompareTag("BossBody"))
        {
            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            Canvas canvas = collision.GetComponentInChildren<Canvas>();

            StartCoroutine(UnFade(collision));

            if (canvas != null)
            {
                canvas.sortingOrder = 10;
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = 10;
            }
        }
    }

    IEnumerator Fade(Collider2D collision)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        canUnFade = true;
        if (canFade == true && collision.CompareTag("Player"))
        {
            canFade = false;
            canUnFade = true;
            sr.color = new Color(1, 1, 1, 0.75f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 0.25f);
            yield return new WaitForSeconds(0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator UnFade(Collider2D collision)
    {
        if (canUnFade == true && collision.CompareTag("Player"))
        {
            canUnFade = false;
            canFade = true;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 0.75f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.2f);
            yield return new WaitForSeconds(0.2f);
            canUnFade = true;
            
        }
    }

    IEnumerator FadeOther(Collider2D collision)
    {
        SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
        otherCanUnFade = true;
        if (otherCanFade == true && collision.CompareTag("Player"))
        {
            otherCanFade = false;
            otherCanUnFade = true;
            sr.color = new Color(1, 1, 1, 0.75f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 0.25f);
            yield return new WaitForSeconds(0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator UnFadeOther(Collider2D collision)
    {
        if (otherCanUnFade == true && collision.CompareTag("Player"))
        {
            otherCanUnFade = false;
            otherCanFade = true;
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
            sr.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 0.75f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.2f);
            yield return new WaitForSeconds(0.2f);
            otherCanUnFade = true;

        }
    }
}
