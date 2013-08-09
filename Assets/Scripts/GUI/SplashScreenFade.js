public var splashScreen : SplashScreen;

private var textureColor : Color; 
private var alpha = 1.0; 

function OnGUI()
{
	if (splashScreen.fadeInAndOut)
	{
		if (Time.time >= splashScreen.time - splashScreen.fadeSpeed)
			alpha += Time.deltaTime * (1.0/splashScreen.fadeSpeed);
		else if (Time.time <= splashScreen.fadeSpeed)
			alpha -= Time.deltaTime * (1.0/splashScreen.fadeSpeed);
		if (alpha > 1) splashScreen.time = 0.0;
	    textureColor = guiTexture.color;
	    textureColor.a = alpha;
	    guiTexture.color = textureColor;
	}
}