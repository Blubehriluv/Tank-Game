using System.Collections;
using UnityEngine;

public class Artillery : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootLocation;
    private Transform locationTransform;

    [Header("Projectile Data")]
    [SerializeField] private Projectile ProjectileData;
    [SerializeField] int projectileSpeed;
    [SerializeField] int projectileDamage;
    [SerializeField] int destroyTime;

    [Header("Artillery Data")]
    [SerializeField] float firingDelay;
    [SerializeField] int reloadSpeed;
    [SerializeField] int ammo;



    float timeUntilNextEvent;
    bool canShoot;
    int tempProjectileDamage;
    float tempFiringDelay;

    void Start()
    {
        // Grabs the Projectile component for data manipulation.
        ProjectileData = bullet.GetComponent<Projectile>();
        // Sets the shoot location transform.
        locationTransform = shootLocation.GetComponent<Transform>();
        ResetStats(0, false);
    }

    void Update()
    {
        // A timer that limits the fire rate.
        timeUntilNextEvent -= Time.deltaTime;
        // If the alotted time has passed, the player can shoot.
        if (timeUntilNextEvent <= 0)
        {
            canShoot = true;
        }
    }

    // Fires a bullet in the direction of the tank it is shot from.
    public void Shoot()
    {
        if (canShoot)
        {

            ProjectileData.SetData(projectileSpeed, projectileDamage, destroyTime, this.gameObject);
            Instantiate(bullet, locationTransform.transform.position, locationTransform.transform.rotation);
            timeUntilNextEvent = firingDelay;
            canShoot = false;
        }
        else
        {
            // Play a sound
        }
    }

    public float GetFireRate()
    {
        return firingDelay;
    }

    // Sets the new fire rate.
    public void SetFireRate(float newFireRate)
    {
        firingDelay = newFireRate;
    }

    public int GetDamage()
    {
        return projectileDamage;
    }

    public void SetDamage(int newDamage)
    {
        projectileDamage = newDamage;
    }

    // TODO : Have a maximum ammo limit.
    public int GetAmmo()
    {
        return ammo;
    }

    // TODO : Ammo packs give more ammo with this function.
    public void SetAmmo(int giveThisAmmo)
    {
        ammo = giveThisAmmo;
    }

    public void ResetStats(float timeBeforeRevert, bool willResetStats)
    {
        if (!willResetStats)
        {
            tempFiringDelay = firingDelay;
            tempProjectileDamage = projectileDamage;
        }
        else if (willResetStats == true)
        {
            StartCoroutine(Wait(timeBeforeRevert));
        }
    }

    IEnumerator Wait(float waitTime)
    {
        Debug.Log("Waiting to reset...");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Resetting stats.");
        firingDelay = tempFiringDelay;
        projectileDamage = tempProjectileDamage;
    }
}
