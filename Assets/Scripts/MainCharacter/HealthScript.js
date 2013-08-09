
var health : float;
var texture1: Texture2D;
var mat : Material;

var x : float = 0;
var y : float = 0;
var w : float;
var h : float;

function Start () {
	health = 100;
}

function OnGUI(){
	if(Event.current.type.Equals(EventType.Repaint)){
	var box : Rect = new Rect(x, y, w, h);
	Graphics.DrawTexture(box, texture1, mat);
	}
}

function Update () {
	var healthy : float = 1-(health/100);
	if(healthy == 0){
		healthy = 0.1;
	}
	mat.SetFloat("_Cutoff", healthy);
}