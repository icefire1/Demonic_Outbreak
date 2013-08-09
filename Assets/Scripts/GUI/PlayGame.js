@script RequireComponent(AudioSource)
private var clicked : boolean = false;
private var levelName : String;

function Awake ()
{
	DontDestroyOnLoad (transform.gameObject);
}

function OnClick (levelNameOnPlay : String) 
{
	print ("Initialising game!");
	audio.Play();
	clicked = true;
	levelName = levelNameOnPlay;
	// Show the load menu when pressed
	GameObject.FindGameObjectWithTag("WorldScripts").SendMessage("setshowLoadingScreen", true, SendMessageOptions.DontRequireReceiver);
//	yield WaitForSeconds (0.1); // Make sure that the loading screen has time to get onto the GUI
//	GameObject.FindGameObjectWithTag("WorldScripts").SendMessage("setshowLoadingScreen", false, SendMessageOptions.DontRequireReceiver);
	Application.LoadLevel(levelNameOnPlay);
}

function Update () 
{
	if (Application.loadedLevelName == levelName && 
		audio.time >= audio.clip.length)
		Destroy (gameObject);
}