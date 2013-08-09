@script RequireComponent(AudioSource)

function OnClick (levelNameOnPlay : String) 
{
	print ("Quitting game!");
	audio.Play();
	Application.Quit();
}