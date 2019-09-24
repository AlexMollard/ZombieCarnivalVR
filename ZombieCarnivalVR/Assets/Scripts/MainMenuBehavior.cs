using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehavior : MonoBehaviour
{
	[SerializeField] Button[] MenuButton;


    // Start is called before the first frame update
    void Start()
    {
		Button PlayButton = MenuButton[0].GetComponent<Button>();
		PlayButton.onClick.AddListener(PlayButtonPressed);

		Button ExitButton = MenuButton[1].GetComponent<Button>();
		ExitButton.onClick.AddListener(ExitButtonPressed);
	}

	void PlayButtonPressed()
	{
		SceneManager.LoadScene(1);
	}

	void ExitButtonPressed()
	{
		Application.Quit();

	}

}
