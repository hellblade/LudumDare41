using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] float animationTime = 0.2f;
    [SerializeField] bool destroyOnComplete = false;
    [SerializeField] bool repeat = false;
    [SerializeField] bool playOnStart = false;
    new SpriteRenderer renderer;
    int currentSprite = 0;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[currentSprite];

        if (playOnStart)
        {
            PlayAnimation();
        }
    }

    public Vector2 Bounds
    {
        get { return sprites[currentSprite].bounds.size; }
    }

    public void PlayAnimation()
    {
        StartCoroutine(DoAnimation());
    }

    IEnumerator DoAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(animationTime);
            currentSprite = (currentSprite + 1) % sprites.Length;

            if (currentSprite == 0)
            {
                if (destroyOnComplete)
                {
                    Destroy(gameObject);
                    break;
                }
                else if (!repeat)
                {
                    break;
                }
            }

            renderer.sprite = sprites[currentSprite];
        }
    }
}