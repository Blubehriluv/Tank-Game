using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;
    public List<Transform> spawnLocations;
    public List<Transform> openLocations;
    [SerializeField] int enemiesToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnLocations = new List<Transform>();
        
        InitializeLocations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeLocations()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnLocations.Add(spawnPoints[i].GetComponent<Transform>());
            if (spawnPoints[i].GetComponent<Spawn>().TypeOfSpawn() == true)
            {
                openLocations.Add(spawnLocations[i].GetComponent<Transform>());
                break;
            }
        }
    }

    

    public void SpawnTank(GameObject Tank, bool isMultiple)
    {
        CheckLocations();
        if (!isMultiple)
        {
            Instantiate(Tank, openLocations[RandomNumber(0, openLocations.Count)]);

        }
        else
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Instantiate(GameManager.instance.enemies[RandomNumber(0, GameManager.instance.enemies.Length)], openLocations[RandomNumber(0, openLocations.Count)]);
                i++;
            }
        }
    }

    void CheckLocations()
    {
        openLocations = new List<Transform>();
        int temp = 0;

        Debug.Log("we in here");

        while(openLocations != null)
        {
            for (int i = 0; i < spawnLocations.Count; i++)
            {
                openLocations.Add(spawnLocations[i].GetComponent<Transform>());
                temp++;
            }
        }
        bool tempBool = spawnPoints[temp].gameObject.GetComponent<Spawn>().TypeOfSpawn();
        Debug.Log(tempBool);
        
        
    }

    int RandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }
}
