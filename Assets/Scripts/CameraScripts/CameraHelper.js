/*
The AerialView should follow this script. This script should
be out on an empty object. It makes sure the camera follows 
the character while rotating slowly.
*/
public var target : Transform;
public var rotationDamping : float = 0.5;

private var lastPos : Vector3;

function Update () 
{
	lastPos = transform.position;
	// The camera will take care of the position Slerp
	transform.position = target.TransformPoint(0, 0, -0.5);
	
	// We don't want it to rotate when we stand still!!
	if (lastPos != transform.position)
	{
		var wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
	}
}