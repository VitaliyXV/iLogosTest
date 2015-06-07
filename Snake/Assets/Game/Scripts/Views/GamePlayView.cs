using UnityEngine;
using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class GamePlayView : View 
{
	public Text LifesLabel;
	public Text LengthLabel;
	public Text PointsLabel;
	public Button ExitButton;

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
		Application.LoadLevel("Main");
	}
}
