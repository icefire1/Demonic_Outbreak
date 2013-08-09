public var returnToSceneName : String;

function Update () {
	// Return to chosen scene if back button is pressed
	if (Input.GetKeyDown(KeyCode.Escape)) {
		if (GameObject.FindGameObjectWithTag("WorldScripts"))
			GameObject.FindGameObjectWithTag("WorldScripts").SendMessage("setshowLoadingScreen", true, SendMessageOptions.DontRequireReceiver);
		Application.LoadLevel(returnToSceneName);
	}
}