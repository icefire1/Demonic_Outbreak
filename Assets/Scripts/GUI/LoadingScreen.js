public var style : GUIStyle;
public var content : GUIContent;
private var screenRect : Rect = Rect(0, 0, Screen.width, Screen.height);

// This should be set from another script from the old scene!!
private var levelName : String; 
private var runOnce : boolean = false;

function Start () 
{
	DontDestroyOnLoad (transform.gameObject);
}

function SetLevelName (_levelName : String)
{
	levelName = _levelName;
}

function Update ()
{
//	if (runOnce)
//	{
//		for (var go : GameObject in Resources.FindObjectsOfTypeAll(typeof(GameObject))){
//			if (go.tag == "MainCamera") var mainCamera : GameObject = go;
//			if (go.tag != "LoadingScreen" && go.tag != "MainCamera")
//				go.active = true;
//		}
//		mainCamera.active = true;
//		Destroy (gameObject);
//		
//	}
//	if (Application.loadedLevelName == levelName && runOnce == false)
//	{
//		for (var go : GameObject in Resources.FindObjectsOfTypeAll(typeof(GameObject))){
//			print (go.name);
//			if (go.tag != "LoadingScreen")
//				go.active = false;
//		}
//	}
	if (runOnce)
		Destroy (gameObject);
	if (levelName && runOnce == false)
	{
		Application.LoadLevel(levelName);
		runOnce = true;
	}
}

function OnGUI()
{
	GUILayout.BeginArea(screenRect, content, style);
	GUILayout.EndArea();
}