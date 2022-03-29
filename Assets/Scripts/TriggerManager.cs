using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "GameOver")
        {
            GameManager.instance.RestartGame();
        }
        
        if(other.tag == "EnergyDrink")
        {
            Destroy(other.gameObject);
            GameManager.instance.GetEnergyDrink(other.gameObject);
        }
        
        if(other.tag == "Win")
        {
            GameManager.instance.Win();
        }
    }
}
