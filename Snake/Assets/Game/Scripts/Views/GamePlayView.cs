using UnityEngine;
using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class GamePlayView : View 
{
	public Text LifesLabel;
	public Text LengthLabel;
	public Text PointsLabel;
	public Button ExitButton;
	public GameObject GameOverLabel;

	public new void Awake()
	{
		base.Awake();

		// swith off 'GameOver' label
		GameOverLabel.SetActive(false);
	}

	public void UpdateLifes(int lifes)
	{
		LifesLabel.text = lifes.ToString();
	}

	public void UpdateLength(int length)
	{
		LengthLabel.text = length.ToString();
	}

	public void UpdatePoints(int points)
	{
		PointsLabel.text = points.ToString();
	}

	public void ExitButtonClick()
	{
		BackToMainMenu();
	}

	public void GameOver()
	{
		GameOverLabel.SetActive(true);
		Invoke("BackToMainMenu", 3);
	}

	public void BackToMainMenu()
	{ 
		Application.LoadLevel("Main");
	}

}
