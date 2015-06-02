public interface IMainMenuController
{
	void Initialize();
	void StartNewGame();
	void SwitchCamera();
	void SwitchFieldType();
	void Exit();
	void InputPlayerName(string name);
	void JoinWithFacebook();
}
