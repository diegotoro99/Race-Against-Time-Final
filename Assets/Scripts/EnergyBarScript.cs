using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarScript : MonoBehaviour
{
    public static EnergyBarScript instance;
    Image image;
    float currentEnergy, prevEnergy; 
    float interpolationFactor; 
    float changeSpeed = 1f; //Tiempo total
    float timeSpent; //Tiempo transcurrido

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        image = GetComponent<Image>();
        image.fillAmount = 1f;

        prevEnergy = 1;
        currentEnergy = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime;
        interpolationFactor = timeSpent/changeSpeed;
        if(Time.timeScale != 0)
        {
            if(currentEnergy > 0)
            {
                currentEnergy -= 0.0005f;
                image.fillAmount = Mathf.Lerp(prevEnergy, currentEnergy, interpolationFactor);
            }
            else
            {
                Time.timeScale = 0;
                GameManager.instance.GameOver();
            }
        }
    }

    public void UpdateEnergybar (float energy)
    {
        prevEnergy = currentEnergy;
        currentEnergy += energy;
        timeSpent = 0;
    }

    public int GetPlayerEnergy()
    {
        return (int)currentEnergy;
    }
}
