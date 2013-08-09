using UnityEngine;
using System.Collections;

public class NavMesh_UI : MonoBehaviour 
{	
	void OnGUI()
	{
		GUILayout.Label("Basic Animator Controller, with NavMesh");
		GUILayout.Label("Touch the Screen to Set a Destination");
		GUILayout.Label("Grahpic level: " + QualitySettings.GetQualityLevel().ToString());
		foreach (Touch touch in Input.touches)
			GUILayout.Label("Touch with ID: " + touch.fingerId);
		
	}		
}
