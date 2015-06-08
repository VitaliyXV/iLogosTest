using strange.extensions.signal.impl;
using UnityEngine;

public class MainMenuStartSignal : Signal { }
public class NewGameSignal : Signal<Vector2> { }
public class CameraSignal : Signal { }
public class FieldTypeSignal : Signal { }
public class ExitSignal : Signal { }
public class JoinFacebookButtonSignal : Signal { }
public class FacebookLoggedSignal : Signal { }
public class InputPlayerNameSignal : Signal<string> { }

public class GamePlayStartSignal : Signal { }
public class LifesChangedSignal : Signal<int> { }
public class LengthChangedSignal : Signal<int> { }
public class PointsChangedSignal : Signal<int> { }
public class GameOverSignal : Signal { }