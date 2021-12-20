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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeLocations()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnLocations = new List<Transform>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnLocations.Add(spawnPoints[i].GetComponent<Transform>().transform);
        }
        CheckLocations();
    }

    public void SpawnTank(GameObject Tank, bool isMultiple)
    {
        CheckLocations();
        if (!isMultiple)
        {
            int tempIndex = RandomNumber(0, openLocations.Count);
            Instantiate(Tank, openLocations[tempIndex].position, openLocations[tempIndex].rotation);
            openLocations.RemoveAt(tempIndex);
        }
        //TODO: Remove isMultiple; remove for loop
        else
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                int tempIndex = RandomNumber(0, openLocations.Count);
                Instantiate(GameManager.instance.enemies[RandomNumber(0, GameManager.instance.enemies.Length)], openLocations[tempIndex].position, openLocations[tempIndex].rotation );
                openLocations.RemoveAt(tempIndex);
            }
        }
    }

    void CheckLocations()
    {
        openLocations = new List<Transform>();
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            if (spawnLocations[i].gameObject.GetComponent<Spawn>().TypeOfSpawn())
            {

                if (!spawnLocations[i].GetComponent<Spawn>().OccupationCheck())
                {
                    
                    openLocations.Add(spawnPoints[i].GetComponent<Transform>());
                    //continue;
                }
            }
        }
    }

    int RandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }
}
