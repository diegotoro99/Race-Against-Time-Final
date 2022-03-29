using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject winUI;
    [SerializeField] EnergyBarScript energyBar;
    
    EventSystem eventSystem;

    bool gameOver;
    bool gamePaused;

    [SerializeField] TextMeshProUGUI text;

    [SerializeField] int min;
    [SerializeField] int sec;
    int m, s;

    string mPrefs = "m";
    string sPrefs = "s";
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        LoadData();
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        eventSystem = EventSystem.current;

        inGameUI.SetActive(true);
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        winUI.SetActive(false);

        gameOver = false;


        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause") && !gameOver)
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        SaveData();
    }

    void PauseGame()
    {
        //Pausar el juego
        Time.timeScale = 0;
        gamePaused = true;
        inGameUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        //Reanudar el juego
        Time.timeScale = 1;
        gamePaused = false;
        inGameUI.SetActive(true);
        pauseUI.SetActive(false);

        eventSystem.SetSelectedGameObject(null);
    }

    public int GetEnergyDrink(GameObject energyDrink)
    {
        int energyObtained = 40;
        int playerEnergy = EnergyBarScript.instance.GetPlayerEnergy();
        int maxEnergy = 100;
        
        int newEnergy = playerEnergy + energyObtained;

        if(newEnergy > maxEnergy)
            newEnergy = maxEnergy;

        energyBar.UpdateEnergybar((float)newEnergy / maxEnergy);

        return newEnergy;
    }
    
    public void GameOver()
    {
        gameOver = true;
        inGameUI.SetActive(false);
        gameOverUI.SetActive(true);

    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
        PlayerPrefs.DeleteAll();
    }

    public void Win()
    {
        Time.timeScale = 0;
        winUI.SetActive(true);
        
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void StartTimer()
    {
        m = PlayerPrefs.GetInt(mPrefs, min);
        s = PlayerPrefs.GetInt(sPrefs, sec);

        UpdateTextTimer(m,s);
        Invoke("UpdateTimer", 1f);
    }

    void UpdateTimer()
    {
        s--;

        if(s < 0)
        {
            if(m == 0)
            {
                Time.timeScale = 0;
                GameOver();
            }
            else
            {
                m--;
                s = 59;
            }
        }

        UpdateTextTimer(m,s);
        Invoke("UpdateTimer", 1f);
    }

    void UpdateTextTimer(int m, int s)
    {
        if(s < 10)
            text.text = m.ToString() + ":0" + s.ToString();
        else
            text.text = m.ToString() + ":" + s.ToString();
    }
    
    void SaveData()
    {
        PlayerPrefs.SetInt(mPrefs, m);
        PlayerPrefs.SetInt(sPrefs, s);
    }

    void LoadData()
    {
        m = PlayerPrefs.GetInt(mPrefs, min);
        s = PlayerPrefs.GetInt(sPrefs, sec);
    }
}
