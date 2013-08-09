//var health = 100;
//var TakeDamage : boolean;
//var playerDistance : int;
//var player : GameObject;
//var attacking : boolean;
//
//function Start()
//{
//	player = GameObject.Find("Player");
//	attacking = false;
//}
//
//function OnTriggerEnter(other : Collider){
//	if(other.tag == "Player"){
//		TakeDamage = true;
//	}
//}
//
//function OnTriggerExit(other : Collider){
//	if(other.tag == "Player"){
//		TakeDamage = false;
//	}
//}
//
//function Update () {
//	if(TakeDamage){
//		if(Input.GetButtonDown("Fire1")){
//			health --;
//		}
//
//
//	playerDistance = Vector3.Distance(player.transform.position, transform.position);
//	if(playerDistance <=2)
//	{
//		if(!attacking)
//		{
//			Invoke("ApplyDamage", 3);
//		
//	
//	if(health < 1){
//	print("Enemy Down!");
//	health = 0;
//	Destroy (gameObject);
//	}
//	
//	if(health == 10){
//		HealthFull = true;
//	}
//}
//
//function ApplyDamage()
//{
//	player.SendMessage("SubtractHealth");
//	attacking = false;
//}
