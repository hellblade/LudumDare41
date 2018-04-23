using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanterBox : MonoBehaviour
{
    public Plants plantTypes;

    public PlantStatus Status { get; private set; }

    public Sprite emptySprite;
    public Sprite growingSprite;
    public Sprite grownSprite;

    Plant plant;
    SpriteRenderer renderer;

    static ObjectPool<PlanterBox> Pool = new ObjectPool<PlanterBox>();

    public static PlanterBox GetPlanterObject(PlanterBox source)
    {
        PlanterBox result = null;

        if (!Pool.TryGet(ref result))
        {
            result = Instantiate(source);
        }

        return result;
    }


    RunnerManager gameManager;
    GeneratorManager genManager;
    PlantOptionsManager plantManager;
    int index;
    int turnsLeft;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<RunnerManager>();
        genManager = FindObjectOfType<GeneratorManager>();
        plantManager = FindObjectOfType<PlantOptionsManager>();
    }

    public void SetIndex(int index)
    {
        this.index = index;

        var gamePlanted = PlayerPrefs.GetInt(PlantPref(), -1);
        var plantType = PlayerPrefs.GetInt(PlantPref() + "-type", -1);

        Status = PlantStatus.Empty;

        renderer.sprite = emptySprite;

        if (gamePlanted > -1)
        {
            plant = plantTypes.GetPlant(plantType);
            Status = PlantStatus.Planted;
            renderer.sprite = growingSprite;

            turnsLeft = plant.TurnsToGrow - (gameManager.GamesPlayed - gamePlanted);

            if (turnsLeft <= 0)
            {
                Status = PlantStatus.Ready;
                renderer.sprite = grownSprite;
            }
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject && collision.gameObject.GetComponent<PlayerController>())
        {
            ShowPlantMenu();
        }
    }

    void ShowPlantMenu()
    {
        plantManager.SetPlant(this, plant);
        switch (Status)
        {
            case PlantStatus.Empty:
                plantManager.ShowPlantOptions();
                break;
            case PlantStatus.Planted:
                plantManager.ShowGrowOptions();
                break;
            case PlantStatus.Ready:
                plantManager.ShowReadyOptions();
                break;            
        }
    }

    string PlantPref()
    {
        return "Plant-" + index;
    }

    public int TurnsLeft()
    {
        return turnsLeft;
    }

    public void UsePlant()
    {
        plant.OnUsed.Invoke();
        Clear();
    }


    public void Clear()
    {
        renderer.sprite = emptySprite;

        PlayerPrefs.SetInt(PlantPref(), -1);
        PlayerPrefs.Save();
    }

    public void Plant()
    {
        Status = PlantStatus.Planted;

        var plantIndex = plantTypes.GetRandomPlantIndex();
        plant = plantTypes.GetPlant(plantIndex);

        PlayerPrefs.SetInt(PlantPref(), gameManager.GamesPlayed + 1);
        PlayerPrefs.SetInt(PlantPref() + "-type", plantIndex);

        PlayerPrefs.Save();
    }

    private void FixedUpdate()
    {
        transform.position -= gameManager.CurrentMoveSpeed * Time.deltaTime;

        if (!gameManager.IsGameRunning || transform.position.x < -genManager.ScreenAmountX - 2)
        {
            Pool.Free(this);
            gameObject.SetActive(false);
        }
    }

}