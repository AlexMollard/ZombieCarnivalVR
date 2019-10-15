using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickScript : MonoBehaviour
{
    public Text clicker;
    public Button restartButton;

    int clicks = 0;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(PressedButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            clicks++;
            clicker.text = "Clicks: " + clicks;
        }
    }

    void PressedButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
