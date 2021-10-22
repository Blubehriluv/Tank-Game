using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    bool WillSurvive(float changeInHealth)
    {
        float helperVariable = currentHealth - changeInHealth;
        if (helperVariable <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        if (WillSurvive(damageToTake))
        {
            currentHealth -= damageToTake;
        }
        else if (!WillSurvive(damageToTake))
        {
            // If they have a life saving powerup, they will acquire health instead
            Death();
        }
    }

    public void SetCurrentHealth(float newHealth)
    {
        currentHealth = newHealth;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }

    void Death()
    {
        Destroy(this.gameObject);
    }
}
