using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanterBox : MonoBehaviour
{
    public Plants plantTypes;
    public int index;

    public PlantStatus Status { get; private set; }

    Plant plant;

    void Awake()
    {
        var gameManager = FindObjectOfType<RunnerManager>();

        var gamePlanted = PlayerPrefs.GetInt(PlantPref(), -1);        
        var plantType = PlayerPrefs.GetInt(PlantPref() + "type", -1);

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
    }

    string PlantPref()
    {
        return "Plant-" + index;
    }
}