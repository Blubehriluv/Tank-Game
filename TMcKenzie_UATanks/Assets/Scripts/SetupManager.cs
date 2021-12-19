using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupManager : MonoBehaviour
{
    public static SetupManager instance;

    [SerializeField] private Dropdown mapTypeDrop;         //dropdown for user-selected maptype
    [SerializeField] private InputField mapSeedInput;      //input field for map seed

    //Types of maps available to the player, as reflected in drop down menu
    public enum MapType {
        Random,
        Daily,
        Seeded }
    //The actual selected map type, defaulted to random
    public MapType Map = MapType.Random;

    private int playerMapSeed = 0;


    private void Awake()
    {
        //Singletone for Setup Manager
        if (instance == null)
        {
            instance = this;

            //Must persist to pass data into game scene
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Debug.Log("Extra Setup Manager was found and destroyed.");
        }
    }

    //Called on value changed from drop down UI
    public void SetMapType()
    {
        //Debug.Log($"Value is: {mapTypeDrop.value}. Text is: {mapTypeDrop.options[mapTypeDrop.value].text}");

        switch (mapTypeDrop.value)
        {
            //Drop down option at index 0 = "Random"
            case 0:
                Map = MapType.Random;
                break;

            //Drop down option at index 1 = "Map of the Day" 
            case 1:
                Map = MapType.Daily;
                break;

            //Drop down option at index 2 = "Seeded"
            case 2:
                Map = MapType.Seeded;
                break;

            //Default case
            default:
                Debug.Log("Case not implemented for map drop down.");
                break;
        }
    }

    //Called on value changed from input field
    public void SetPlayerMapSeed()
    {
        //If the input was a true integer, set map seed
        int temp;
        if (int.TryParse(mapSeedInput.text, out temp))
        {
            playerMapSeed = temp;
        }

        //Else it was not a valid integer, default to seed 0
        else
        {
            Debug.Log("Map seed input field value was not an integer. Seed reset to 0.");
            playerMapSeed = 0;
        }

        //Debug.Log("Seed is: " + playerMapSeed);
    }


    public MapType GetMapType()
    {
        return Map;
    }

    public int GetPlayerMapSeed()
    {
        return playerMapSeed;
    }

}
