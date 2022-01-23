using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image[] lives;
    int currentLives;
    // Start is called before the first frame update
    void Start()
    {
        currentLives = 4;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LifeChecker()
    {
        for (int i = currentLives+1; i < 7; i++)
        {
            
        }
    }
}
