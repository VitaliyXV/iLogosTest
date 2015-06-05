using UnityEngine;

public interface IMainMenuController
{
	void Initialize();
	void StartNewGame(Vector2 fieldSize);
	void SwitchCamera();
	void SwitchFieldType();
	void Exit();
	void InputPlayerName(string name);
	void JoinWithFacebook();
}
