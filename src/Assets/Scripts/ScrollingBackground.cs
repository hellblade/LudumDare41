using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class ScrollingBackground : MonoBehaviour
{

    [SerializeField] float speed;
    float initialPosition;
    float width;

    RunnerManager gameManager;
    GeneratorManager genManager;

    int currentCopies = 0;

    void Awake()
    {
        gameManager = FindObjectOfType<RunnerManager>();
        genManager = FindObjectOfType<GeneratorManager>();
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        
        genManager.ResolutionChanged.AddListener(DoSizing);
    }

    private void Start()
    {
        DoSizing();
    }

    void DoSizing()
    {
        int requiredCopies = Mathf.CeilToInt(genManager.ScreenAmountX / width);

        if (currentCopies != requiredCopies)
        {
            currentCopies = requiredCopies;

            // Just remove current children and make new ones for now...
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 1; i <= requiredCopies; i++)
            {
                var copy = Instantiate(gameObject);

                Destroy(copy.GetComponent<ScrollingBackground>());

                copy.transform.SetParent(transform);
                copy.transform.localPosition = new Vector3(width * i, 0, 0);
            }
        }

        transform.position = new Vector3(-width, 0, 0);

        initialPosition = transform.position.x;
    }

    void Update()
    {
        transform.Translate(gameManager.CurrentMoveSpeed * -speed * Time.deltaTime);

        // Loop it
        if (initialPosition - transform.position.x > width)
        {
            transform.Translate(new Vector3(width, 0, 0));
        }
    }
}