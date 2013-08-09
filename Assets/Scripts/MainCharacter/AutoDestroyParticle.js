function Update () 
{
	 if (!particleSystem.IsAlive())
	 	Destroy (this.gameObject);
}