using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artillery : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootLocation;

    private Transform locationTransform;
    [SerializeField] private Projectile bulletData;
    [SerializeField] int firingSpeed;
    [SerializeField] int projectileSpeed;
    [SerializeField] int reloadSpeed;
    [SerializeField] int projectileDamage;
    [SerializeField] int ammo;

    // Start is called before the first frame update
    void Start()
    {
        bulletData = bullet.GetComponent<Projectile>();
        bulletData.SetDamageAndSpeed(projectileSpeed, projectileDamage);

        locationTransform = shootLocation.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        Instantiate(bullet, locationTransform.transform.position, locationTransform.transform.rotation);
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
