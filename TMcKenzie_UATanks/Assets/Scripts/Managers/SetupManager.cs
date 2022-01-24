using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupManager : MonoBehaviour
{
    public static SetupManager instance;

    [SerializeField] private Dropdown mapTypeDrop;         //dropdown for user-selected maptype
    [SerializeField] private InputField mapSeedInput;      //input field for map seed
    [SerializeField] private GameObject screenSelectionObject;  //parent of split screen options
    [SerializeField] private Toggle[] playerToggles;
    [SerializeField] private Toggle horizontalScreenToggle;
    [SerializeField] private Toggle verticalScreenToggle;


    //Types of maps available to the player, as reflected in drop down menu
    public enum MapType {
        Random,
        Daily,
        Seeded }
    //The actual selected map type, defaulted to random
    private MapType Map = MapType.Random;

    private int playerMapSeed = 0;
    private int numberOfPlayers = 1;

    public enum ScreenType {
        Full,
        Horizontal,
        Vertical    
    }
    private ScreenType screen = ScreenType.Full;

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
                mapSeedInput.gameObject.SetActive(false);
                break;

            //Drop down option at index 1 = "Map of the Day" 
            case 1:
                Map = MapType.Daily;
                mapSeedInput.gameObject.SetActive(false);
                break;

            //Drop down option at index 2 = "Seeded"
            case 2:
                Map = MapType.Seeded;
                mapSeedInput.gameObject.SetActive(true);
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

    //Called on value changed from player toggles
    public void SetNumberOfPlayers()
    {
        //Search all player toggles
        for (int i = 0; i < playerToggles.Length; i++)
        {
            //If we find the one that is on (note: only one can be on at a time)
            if (playerToggles[i].isOn)
            {
                //player number is that index + 1
                numberOfPlayers = i + 1;

                //end loop
                break;
            }
        }

        //If more than 1 player, activate split screen options
        if (numberOfPlayers > 1)
        {
            screenSelectionObject.SetActive(true);
        }

        //else 1 player, disable splitscreen options
        else
        {
            screenSelectionObject.SetActive(false);
        }
    }

    public MapType GetMapType()
    {
        return Map;
    }

    public int GetPlayerMapSeed()
    {
        return playerMapSeed;
    }

    public int GetNumberOfPlayers()
    {
        return numberOfPlayers;
    }

    //Called by start button after selections are complete
    public void SetScreenType()
    {
        //Full screen for single player mode
        if (numberOfPlayers == 1)
        {
            screen = ScreenType.Full;
        }

        //Horizontal screen if selected
        else if (horizontalScreenToggle.isOn)
        {
            screen = ScreenType.Horizontal;
        }
        
        //Vertical screen if selected
        else if (verticalScreenToggle.isOn)
        {
            screen = ScreenType.Vertical;
        }
    }

    public ScreenType GetScreenType()
    {
        return screen;
    }
}