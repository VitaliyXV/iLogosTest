using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class MainMenuMediator : Mediator
{
	[Inject]
	public MainMenuView mainMenuView { get; set; }

	[Inject]
	public StartSignal startSignal { get; set; }

	[Inject]
	public NewGameSignal newGameSignal { get; set; }

	[Inject]
	public CameraSignal cameraSignal { get; set; }

	[Inject]
	public FieldTypeSignal fieldTypeSignal { get; set; }

	[Inject]
	public ExitSignal exitSignal { get; set; }

	[Inject]
	public InputPlayerNameSignal inputPlayerNameSignal { get; set; }

	[Inject]
	public JoinFacebookButtonSignal facebookSignal { get; set; }

	[Inject]
	public FacebookLoggedSignal facebookLoggedSignal { get; set; }

	public override void OnRegister()
	{
		startSignal.AddListener(mainMenuView.Initialize);

		mainMenuView.buttonNewGameClicked.AddListener(newGameSignal.Dispatch);
		mainMenuView.buttonCameraClicked.AddListener(cameraSignal.Dispatch);
		mainMenuView.buttonFieldTypeClicked.AddListener(fieldTypeSignal.Dispatch);
		mainMenuView.buttonExitClicked.AddListener(exitSignal.Dispatch);
		mainMenuView.inputPlayerNameChanged.AddListener(inputPlayerNameSignal.Dispatch);
		mainMenuView.buttonFacebookClicked.AddListener(facebookSignal.Dispatch);

		facebookLoggedSignal.AddListener(mainMenuView.UpdatePlayerName);
	}

	public override void OnRemove()
	{
		startSignal.RemoveListener(mainMenuView.Initialize);

		mainMenuView.buttonNewGameClicked.RemoveListener(newGameSignal.Dispatch);
		mainMenuView.buttonCameraClicked.RemoveListener(cameraSignal.Dispatch);
		mainMenuView.buttonFieldTypeClicked.RemoveListener(fieldTypeSignal.Dispatch);
		mainMenuView.buttonExitClicked.RemoveListener(exitSignal.Dispatch);
		mainMenuView.inputPlayerNameChanged.RemoveListener(inputPlayerNameSignal.Dispatch);
		mainMenuView.buttonFacebookClicked.RemoveListener(facebookSignal.Dispatch);

		facebookLoggedSignal.RemoveListener(mainMenuView.UpdatePlayerName);
	}
}