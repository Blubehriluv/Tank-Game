using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject pickup;
    public enum SpawnType {Tank, Pickup};
    public SpawnType spawnType;

    [SerializeField]bool isOccupied = false;
    bool isForTank;

    // Start is called before the first frame update
    void Awake()
    {
        if (spawnType == SpawnType.Tank)
        {
            isForTank = true;
        }
        else if (spawnType == SpawnType.Pickup)
        {
            Instantiate(pickup, this.gameObject.transform.position, this.gameObject.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TypeOfSpawn()
    {
        return isForTank;
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
                    Debug.Log("something left and it was a :" + other.name);
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
