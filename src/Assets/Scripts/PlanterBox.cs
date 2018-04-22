using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanterBox : MonoBehaviour
{   
    public int index;

    public PlantStatus Status { get; private set; }

    int gamePlanted;
    string plantType;

    void Awake()
    {
        gamePlanted = PlayerPrefs.GetInt(PlantPref(), -1);        
        plantType = PlayerPrefs.GetString(PlantPref() + "type", "");

        Status = PlantStatus.Empty;

        if (gamePlanted > -1)
        {
            Status = PlantStatus.Planted;


        }
    }

    string PlantPref()
    {
        return "Plant-" + index;
    }
}