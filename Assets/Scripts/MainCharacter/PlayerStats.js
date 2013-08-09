public var health : float;
public var physicalDefense : float;
public var magicalDefense : float;

public var exampleTarget : GameObject;

/*
 The beneath code should be in another script.
 When something needs to deal damage to something
 this is the code required to deal damage.
 The order of the lines are crucial!
*/
function Start () 
{
//	var exampleArray = new Array(); // Make an array
//	exampleArray.Add("Physical"); // Add the damage type (Physical/Magical)
//	exampleArray.Add(20); // Add the damage. the receiver should apply defense reductions
//	// Send the damage to get Target. Where the Target is a gameobject
//	exampleTarget.SendMessage("ApplyDamage", exampleArray, SendMessageOptions.DontRequireReceiver);
}

function Update () 
{
}

/*
 Anything that needs to received damage and
 should be able to reduce it, need these lines of code
*/
function ApplyDamage (damageArr : Array)
{
	
	var type : String = damageArr[0];
	var damage : float = damageArr[1];
	if (type == "Physical")
		health -= damage - damage * physicalDefense * 0.01;	
	if (type == "Magical")
		health -= damage - damage * magicalDefense * 0.01;		 
}
