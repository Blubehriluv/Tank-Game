using UnityEngine;

public class Pickup : MonoBehaviour
{
    /// <summary>
    /// Script to manage all pickup data
    /// </summary>
    [SerializeField]
    public enum PickupType { RapidFire, Stealth, Mine, FireRateUp, Switcheroonie };
    public PickupType pickupType;
    [SerializeField] Collider collider;
    [SerializeField] float respawnTime;

    [Header("RapidFire"), SerializeField]
    float activationTime;
    [Header("Ghost"), SerializeField]
    float ghostTime;


    // Start is called before the first frame update
    void Start()
    {
        collider = this.gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Sets the pickup type for the pickup gameobject

    public float GetRespawnTime()
    {
        return respawnTime;
    }

    void RapidFire(Artillery artillery)
    {

        artillery.SetFireRate(artillery.GetFireRate() / 2);
        artillery.SetDamage(artillery.GetDamage() - 14);
        artillery.ResetStats(activationTime, true);

    }

    void Stealth(TankData data)
    {
        data.SetGhost(true, ghostTime);
    }

    void Mine()
    {
        // swap mine color
        // get position of 
    }

    void FireRateUp(Artillery artillery)
    {
        artillery.SetFireRate(artillery.GetFireRate() + .2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<InputController>())
        {
            // This is the player
            GameObject player = collision.gameObject;

            if (pickupType == PickupType.RapidFire || pickupType == PickupType.FireRateUp)
            {
                Artillery playerArty = collision.gameObject.GetComponent<Artillery>();
                if (pickupType == PickupType.RapidFire)
                {
                    RapidFire(playerArty);
                }
                else if (pickupType == PickupType.FireRateUp)
                {
                    FireRateUp(playerArty);
                }
            }
            if (pickupType == PickupType.Mine)
            {
                if (collision.gameObject.GetComponent<InputController>())
                {

                }
            }
            if (pickupType == PickupType.Stealth)
            {
                TankData playerData = collision.gameObject.GetComponent<TankData>();
                Stealth(playerData);
            }
        }

        if (collision.gameObject.GetComponent<TankData>())
        {

        }

        Destroy(this.gameObject);
    }
}
