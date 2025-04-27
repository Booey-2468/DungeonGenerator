using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnUnpauseButtonClicked()
    {
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
    public void OnResetButtonClicked()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameStart");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && Time.timeScale == 0)
        {
            Time.timeScale = 1.0f;
            Destroy(gameObject);
        }
    }
}
