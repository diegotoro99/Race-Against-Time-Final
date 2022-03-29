using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuUI.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
        PlayerPrefs.DeleteAll();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
