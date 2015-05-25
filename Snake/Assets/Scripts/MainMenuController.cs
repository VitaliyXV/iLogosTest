using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
	void Start () 
	{
	
	}

	void Update()
	{
#if UNITY_ANDROID	// exit from game by press android's back button 
		
		if(Input.GetKeyDown(KeyCode.Escape) && Game.Instance.State == GameState.MainMenu)
		{
			ExitGameButtonClick();
		}
#endif
	}

	public void NewGameButtonClick()
	{
		Debug.Log("New Game");
	}

	public void ExitGameButtonClick()
	{
		Debug.Log("Exit");

		Application.Quit();
	}
}
