#pragma strict

public var health : float = 250;
public var physicalDefense : float = 50;
public var magicalDefense : float = 50;
var destructionParticles : GameObject;

function Start () {

}

function Update () {

	if(health <= 0.0)
	{
		Instantiate(destructionParticles, transform.position, transform.rotation);
		Destroy(gameObject);
	}

}

// It should receive damage like the beneath function
//function OnCollisionEnter(myCollision : Collision)
//{
//	if(myCollision.transform.name == ("Bullet(Clone)")) //i think this is for when the main character attacks.
//	{
//		baseHealth -= 1;
//	}
//}

// I made it so it can receive damage
function ApplyDamage (damageArr : Array)
{
	
	var type : String = damageArr[0];
	var damage : float = damageArr[1];
	if (type == "Physical")
		health -= damage - damage * physicalDefense * 0.01;	
	if (type == "Magical")
		health -= damage - damage * magicalDefense * 0.01;		 
}