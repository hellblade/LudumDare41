using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanterBox : MonoBehaviour
{
    public Plants plantTypes;

    public PlantStatus Status { get; private set; }

    Plant plant;

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
    int index;

    void Awake()
    {
        gameManager = FindObjectOfType<RunnerManager>();
        genManager = FindObjectOfType<GeneratorManager>();
    }

    public void SetIndex(int index)
    {
        var gamePlanted = PlayerPrefs.GetInt(PlantPref(), -1);
        var plantType = PlayerPrefs.GetInt(PlantPref() + "-type", -1);

        Status = PlantStatus.Empty;

        if (gamePlanted > -1)
        {
            plant = plantTypes.GetPlant(plantType);
            Status = PlantStatus.Planted;

            if (plant.TurnsToGrow > gameManager.GamesPlayed - gamePlanted)
            {
                Status = PlantStatus.Ready;
            }
        }

        this.index = index;
    }

    string PlantPref()
    {
        return "Plant-" + index;
    }

    public void UsePlant()
    {
        plant.OnUsed.Invoke();

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

        if (transform.position.x < -genManager.ScreenAmountX - 2)
        {
            Pool.Free(this);
            gameObject.SetActive(false);
        }
    }

}