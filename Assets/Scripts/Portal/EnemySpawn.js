#pragma strict

var enemy : Transform;
var enemyArea : Transform;
var timeDelay = 0.0;

function SpawnEnemy () 
{
	Instantiate (enemy, enemyArea.position, enemyArea.rotation);
}

function Start ()
{
	SpawnEnemy();
}

function OnTriggerStay(triggerCollider : Collider)
{
	if(triggerCollider.transform.name == ("Player"))
	{	
		timeDelay += Time.deltaTime;
		if(timeDelay > 5.0)
		{
			SpawnEnemy();
			timeDelay = 0.0; //keep spawning till base is destroyed
		}
	}
}