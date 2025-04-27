using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonData : MonoBehaviour
{
    // Start is called before the first frame update

    [HideInInspector] public int maxRooms = 10;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    public void OnSliderChange()
    {
        sliderText.text = slider.value.ToString();
        maxRooms = (int)slider.value;
    }

    public void StartButtonClicked()
    {
        SceneManager.LoadScene("GameStart");
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
    void Start()
    {
      DontDestroyOnLoad(this);
      sliderText.text = slider.value.ToString();
      maxRooms = (int)slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
