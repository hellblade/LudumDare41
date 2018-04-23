using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantOptionsManager : MonoBehaviour
{
    [SerializeField] GameObject plantOptions;
    [SerializeField] GameObject growOptions;
    [SerializeField] GameObject readyOptions;

    [SerializeField] Button plantButton;
    [SerializeField] Text plantStatus;
    [SerializeField] Text plantName;
    [SerializeField] Text plantSell;

    Plant plant;
    PlanterBox box;
    InventoryManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<InventoryManager>();
    }

    public void SetPlant(PlanterBox box, Plant plant)
    {
        this.plant = plant;
        this.box = box;
    }


    public void HideOptions()
    {
        Time.timeScale = 1;
        plantOptions.SetActive(false);
        growOptions.SetActive(false);
        readyOptions.SetActive(false);
    }

    public void Plant()
    {
        if (manager.Seeds <= 0)
            return;

        manager.UseSeeds(1);
        box.Plant();
        HideOptions();
    }

    public void UsePlant()
    {        
        box.UsePlant();
        HideOptions();
    }

    public void ShowPlantOptions()
    {
        Time.timeScale = 0;
        plantOptions.SetActive(true);

        plantButton.interactable = manager.Seeds > 0;
    }

    public void ShowGrowOptions()
    {
        Time.timeScale = 0;
        growOptions.SetActive(true);

        if (box.TurnsLeft() <= 2)
        {
            plantStatus.text = "It is almost ready...";
        }
        else
        {
            plantStatus.text = "It might be a while...";
        }
    }

    public void ShowReadyOptions()
    {
        Time.timeScale = 0;
        readyOptions.SetActive(true);

        plantName.text = plant.Name;
        plantSell.text = "Sell (" + plant.sellPrice + ")";
    }

    public void SellPlant()
    {
        manager.AddCoins(plant.sellPrice);
        box.Clear();
        HideOptions();
    }

}