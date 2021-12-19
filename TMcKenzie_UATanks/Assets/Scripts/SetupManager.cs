using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupManager : MonoBehaviour
{
    public static SetupManager instance;

    [SerializeField] private Dropdown mapTypeDrop;         //dropdown for user-selected maptype
    [SerializeField] private InputField mapSeedInput;      //input field for map seed

    public enum MapType { Random, Seeded, Daily}
    public MapType Map = MapType.Random;


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

    public void SetMapType()
    {
        Debug.Log("SetMapType called");
    }
}
