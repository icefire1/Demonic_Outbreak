#pragma strict
private var player : GameObject;
//function Start () {
// player = GameObject.FindGameObjectWithTag("Player");
//}

function OnTriggerEnter(other : Collider){
	if(other.gameObject == player){
		player.GetComponent(HealthScript).health -= 10;
		}
}