public var levelNameOnPlay :  String;
public var loadingScreenStyle : GUIStyle;
public var loadingScreenContent : GUIContent;
private var screenRect : Rect = Rect(0, 0, Screen.width, Screen.height);
@HideInInspector
public var showLoadingScreen : boolean = true;

// This is automaticly set
private var menuSceneName : String;

function Awake () 
{
	if (GameObject.FindGameObjectsWithTag("WorldScripts").Length > 1)
		Destroy (gameObject);
	DontDestroyOnLoad (transform.gameObject);
	menuSceneName = Application.loadedLevelName;
}

function Update () 
{
	if (Application.loadedLevelName == menuSceneName)
	{
		// Check if we're on a mobile device
	    if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
			// Close application if back button is pressed
			if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit(); 
			
	        if (Input.touchCount <= 0)
	            return;
	
	        // Detect multi touch
	        for (var touch : Touch in Input.touches) 
	        {
	        	if (touch.phase == TouchPhase.Began) 
		            OnTouchBegan(touch.position);
	        }
	    }
	    // Check for mouse clicks instead of touch if on PC/Mac/Linux
	    else if (Input.GetMouseButtonDown(0))
	    	OnTouchBegan(Input.mousePosition);
	}
}

function OnTouchBegan (screenPos : Vector2) 
{
	var ray = Camera.main.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y, 0));
    var hit: RaycastHit;
    if (Physics.Raycast(ray, hit)) 
    {
		hit.collider.gameObject.SendMessage("OnClick", levelNameOnPlay, SendMessageOptions.DontRequireReceiver);
    }
}

function setshowLoadingScreen (bool : boolean)
{
	showLoadingScreen = bool;
}

function OnGUI () 
{
	if (showLoadingScreen)
	{
		GUILayout.BeginArea(screenRect, loadingScreenContent, loadingScreenStyle);
		GUILayout.EndArea();
	}
}