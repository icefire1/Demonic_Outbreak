public var maxSpeed : float = 5;
public var acceleration : float = 5;

private var moveSpeed : float = 0.0;
private var onTheMove : boolean;

function Start () 
{

}

function Update () 
{
	// Check if we're on a mobile device
    if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) 
	{
		// Detect multi touch
        if (Input.touchCount > 0)
		{
	        for (var touch : Touch in Input.touches)
			{
		    	MoveTowards(touch.position);
	        }
		}	        
    }
    // Check for mouse clicks instead of touch if on PC/Mac/Linux
    else if (Input.GetButton("Fire1"))
	{
    	MoveTowards(Input.mousePosition);
	}
	// set the speed from acceleration
}

function MoveTowards (screenPos : Vector2) 
{
	// Construct a ray from the current mouse coordinates
	var ray = Camera.main.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y, 0));
	var hit : RaycastHit = new RaycastHit();
	if (Physics.Raycast(ray, hit))
	{	
		var direction : Vector3 = hit.point;
		direction -= transform.position;
		direction.y = 0.0;
	}
	direction.Normalize();
	rigidbody.MovePosition(rigidbody.position+direction * Time.deltaTime * maxSpeed); 
}