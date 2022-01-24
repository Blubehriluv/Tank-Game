using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    TankData data;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] bool isImmortal;
    [SerializeField] int maxLives;
    [SerializeField] int currentLives;

    void Start()
    {
        data = this.gameObject.GetComponent<TankData>();
        currentHealth = maxHealth;
        currentLives = maxLives;
    }

    void Update()
    {

    }

    // Getter for the health of this game object.
    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    // Will this game object survive after a change in health?
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

    // Handles damage on this game object and kills it if necessary.
    public void TakeDamage(float damageToTake)
    {
        // If the object will survive, just deal damage.
        if (WillSurvive(damageToTake))
        {
            currentHealth -= damageToTake;
        }
        // If the object will not survive, it dies.
        else if (!WillSurvive(damageToTake))
        {
            // If they have a life saving powerup, they will acquire health instead
            Death();
        }
    }

    // Setter for the current health of this game object.
    public void SetCurrentHealth(float newHealth)
    {
        currentHealth = newHealth;
    }

    // Setter for the max health of this game object.
    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }

    // Death.
    void Death()
    {
        if (isImmortal)
        {
            GameManager.instance.SomeoneDied(this.gameObject);
            Debug.Log("Tank is: " + this.gameObject.name);
        }
        else
        {
            if (GameManager.instance.GetCurrentLives(data.GetPlayerNumber()) != 0)
            {
                GameManager.instance.SetCurrentLives(data.GetPlayerNumber());
                GameManager.instance.SomeoneDied(this.gameObject);
            }
            else
            {
                
            }
        }
        Destroy(this.gameObject);
    }
}
