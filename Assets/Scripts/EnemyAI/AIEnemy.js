var target : Transform;
public var health : float = 100;
public var physicalDefense : float = 10;
public var magicalDefense : float = 10;
private var agent : NavMeshAgent;
var moveSpeed = 1;

function Start()
{
        // Get the component beforehand
        agent = GetComponent(NavMeshAgent);
}

function Update()
{
        // Then set the destination from the defined variable
        agent.destination = target.position;
        if (health <= 0.0)
        	Destroy(gameObject);
}

//function ApplyDamage (damageArr : ArrayList)
//{
//	var type : String = damageArr[0];
//	print (type);
//	var damage : float = damageArr[1];
//	if (type == "Physical")
//		health -= damage - damage * physicalDefense * 0.01;	
//	if (type == "Magical")
//		health -= damage - damage * magicalDefense * 0.01;		 
//}