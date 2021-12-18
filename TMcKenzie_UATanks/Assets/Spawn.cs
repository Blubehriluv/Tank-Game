using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject pickup;
    GameObject enemy;
    public enum SpawnType {Tank, Pickup};
    public SpawnType spawnType;

    [SerializeField]bool isOccupied = false;
    bool isForTank;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnType == SpawnType.Tank)
        {
            isForTank = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSomething()
    {

    }

    public bool TypeOfSpawn()
    {
        return isForTank;
    }

    public void EmptyAlert()
    {
        if (spawnType == SpawnType.Pickup)
        {

        }
    }

    public void ToggleOccupation()
    {
        isOccupied = !isOccupied;
    }

    public bool OccupationCheck()
    {
        return isOccupied;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Hearing")
        {
            isOccupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Hearing")
        {
            if (spawnType == SpawnType.Pickup)
            {
                if (other.gameObject.GetComponent<Pickup>())
                {
                    StartCoroutine(WaitForPickup(other.gameObject.GetComponent<Pickup>().GetRespawnTime()));
                }
            }
            isOccupied = false;
        }
    }

    IEnumerator WaitForPickup(float waitTime)
    {
        Debug.Log("Pickup acquired.");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Pickup respawned.");
        Instantiate(pickup, this.gameObject.transform);
    }
}
