// Read the CameraHelper script explanation 
var CameraHelper : Transform;
var distance = 3.0;
var height = 3.0;
var damping = 5.0;
var smoothLookAt = true;
var lookAtDamping = 10.0;

function Update () 
{
	var wantedPosition = CameraHelper.TransformPoint(0, height, -distance);
	transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);

	if (smoothLookAt) 
	{
		var wantedRotation = Quaternion.LookRotation(CameraHelper.position - transform.position, CameraHelper.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * lookAtDamping);
	}
	
	else transform.LookAt (CameraHelper, CameraHelper.up);
}