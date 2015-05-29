using strange.extensions.mediation.impl;

public class MainMenuMediator : Mediator
{
	[Inject]
	public MainMenuView mainMenuView { get; set; }

	[Inject]
	public NewGameSignal newGameSignal { get; set; }

	[Inject]
	public CameraSignal cameraSignal { get; set; }

	[Inject]
	public FieldTypeSignal fieldTypeSignal { get; set; }

	[Inject]
	public ExitSignal exitSignal { get; set; }

	public override void OnRegister()
	{
		mainMenuView.buttonNewGameClicked.AddListener(newGameSignal.Dispatch);
		mainMenuView.buttonCameraClicked.AddListener(cameraSignal.Dispatch);
		mainMenuView.buttonFieldTypeClicked.AddListener(fieldTypeSignal.Dispatch);
		mainMenuView.buttonExitClicked.AddListener(exitSignal.Dispatch);
	}
}