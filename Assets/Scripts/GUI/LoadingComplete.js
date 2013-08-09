function Start () {
	if (GameObject.FindGameObjectWithTag("WorldScripts"))
		GameObject.FindGameObjectWithTag("WorldScripts").SendMessage("setshowLoadingScreen", false, SendMessageOptions.DontRequireReceiver);
}