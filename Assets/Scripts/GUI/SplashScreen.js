public var menuSceneName : String;
public var splashImageTime : float;
public var fadeInAndOut : boolean;
public var fadeSpeed : float;

private var myGUITexture : GUITexture;
@HideInInspector
public var time : float;
 
function Awake()
{
	time = Time.time + splashImageTime;
    myGUITexture = this.gameObject.GetComponent("GUITexture") as GUITexture;
}
 
// Use this for initialization
function Start()
{
    // Position the billboard in the center, 
    // but respect the picture aspect ratio
    var textureHeight : int = guiTexture.texture.height;
    var textureWidth : int = guiTexture.texture.width;
    var screenHeight : int = Screen.height;
    var screenWidth : int = Screen.width;
 
    var screenAspectRatio : int = (screenWidth / screenHeight);
    var textureAspectRatio : int = (textureWidth / textureHeight);
 
    var scaledHeight : int ;
    var scaledWidth : int ;
    if (textureAspectRatio <= screenAspectRatio)
    {
        // The scaled size is based on the height
        scaledHeight = screenHeight;
        scaledWidth = (screenHeight * textureAspectRatio);
    }
    else
    {
        // The scaled size is based on the width
        scaledWidth = screenWidth;
        scaledHeight = (scaledWidth / textureAspectRatio);
    }
    var xPosition : float = screenWidth / 2 - (scaledWidth / 2);
    myGUITexture.pixelInset = 
        new Rect(xPosition, scaledHeight - scaledHeight, 
        scaledWidth, scaledHeight);
}

function Update()
{
		// Check if we're on a mobile device
	    if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) 
		{
			// Detect multi touch
	        if (Input.touchCount > 0)
			{
		        time = fadeSpeed;
			}	        
	    }
	    // Check for mouse clicks instead of touch if on PC/Mac/Linux
	    else if (Input.GetMouseButtonDown(0))
		{
	    	time = fadeSpeed;
		}
	if (Time.time >= time)
	{
		Application.LoadLevel(menuSceneName);
	}
}
