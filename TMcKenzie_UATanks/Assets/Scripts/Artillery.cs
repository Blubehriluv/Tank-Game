using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        ProjectileData = bullet.GetComponent<Projectile>();
        

        locationTransform = shootLocation.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilNextEvent -= Time.deltaTime;
        if (timeUntilNextEvent <= 0)
        {
            canShoot = true;
            
        }
        else
        {
            
            // Play a sound
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            ProjectileData.SetData(projectileSpeed, projectileDamage, destroyTime, this.gameObject);
            Instantiate(bullet, locationTransform.transform.position, locationTransform.transform.rotation);
            timeUntilNextEvent = firingDelay;
            canShoot = false;
        }
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public void SetAmmo(int giveThisAmmo)
    {
        ammo = giveThisAmmo;
    }
}
