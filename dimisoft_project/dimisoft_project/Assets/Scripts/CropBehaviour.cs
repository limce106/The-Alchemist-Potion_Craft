using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    SeedData seedToGrow;

    [Header("Stage of Life")]
    public GameObject seed;
    public GameObject seedling;
    private GameObject harvestable;

    int growth;
    int maxGrowth;

    public enum CropState
    {
        Seed, Seedling,Harvaestable
    }

    public CropState cropState;

    public void Plant(SeedData seedToGrow)
    {
        this.seedToGrow = seedToGrow;
        seedling = Instantiate(seedToGrow.seedling, transform);

        ItemData cropToYield = seedToGrow.cropToYield;

        harvestable = Instantiate(cropToYield.gameModel, transform);

        SwitchState(CropState.Seed);
    }

    public void Grow()
    {
        growth++;

        if(growth >= maxGrowth / 2 && cropState == CropState.Seed)
        {
            SwitchState(CropState.Seedling);
        }

        if(growth >= maxGrowth && cropState == CropState.Seedling)
        {
            SwitchState(CropState.Harvaestable);
        }
    }

    public void SwitchState(CropState stateToSwitch)
    {
        seed.SetActive(false);
        seedling.SetActive(false);
        harvestable.SetActive(false);

        switch(stateToSwitch)
        {
            case CropState.Seed:
                seed.SetActive(true);
                break;
            case CropState.Seedling:
                seedling.SetActive(true);
                break;
            case CropState.Harvaestable:
                harvestable.SetActive(true);
                Destroy(gameObject);
                break;
        }
        cropState = stateToSwitch;
    }
}
