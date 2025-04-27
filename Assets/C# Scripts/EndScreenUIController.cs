using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenUIController : MonoBehaviour
{
    private GameObject dungeonData;
    // Start is called before the first frame update
    void Start()
    {
        dungeonData = GameObject.FindGameObjectWithTag("DataStore");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1;
        if (dungeonData != null)
        {
            Destroy(dungeonData);
        }
        SceneManager.LoadScene("StartUI");
    }
}
