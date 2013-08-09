using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {
	public GameObject			particle;
	public float timeScale = 0.5f;
	protected NavMeshAgent		agent;
	protected Animator			animator;

	protected Locomotion locomotion;
	protected Object particleClone;
	protected float denyTouch = -1.0f;
	protected GUILayer guiLayer;


	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;

		animator = GetComponent<Animator>();
		animator.speed = timeScale;
		locomotion = new Locomotion(animator);

		particleClone = null;
		guiLayer = Camera.main.GetComponent<GUILayer>();
	}

	protected void SetDestination(Vector2 screenPos)
	{
		// Construct a ray from the current mouse coordinates
		var ray = Camera.main.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y, 0));
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit))
		{
			if (particleClone != null)
			{
				GameObject.Destroy(particleClone);
				particleClone = null;
			}

			// Create a particle if hit
			Quaternion q = new Quaternion();
			q.SetLookRotation(hit.normal, Vector3.forward);
			particleClone = Instantiate(particle, hit.point, q);
			
			agent.destination = hit.point;
		}
	}

	protected void SetupAgentLocomotion()
	{
		if (AgentDone())
		{
			locomotion.Do(0, 0);
			if (particleClone != null)
			{
				GameObject.Destroy(particleClone);
				particleClone = null;
			}
		}
		else
		{
			float speed = agent.desiredVelocity.magnitude;

			Vector3 velocity = Quaternion.Inverse(transform.rotation) * agent.desiredVelocity;

			float angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / 3.14159f;
			
			locomotion.Do(speed, angle);
		}
	}

    void OnAnimatorMove()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;
		transform.rotation = animator.rootRotation;
    }

	protected bool AgentDone()
	{
		return !agent.pathPending && AgentStopping();
	}

	protected bool AgentStopping()
	{
		return agent.remainingDistance <= agent.stoppingDistance;
	}

	// Update is called once per frame
	void Update () 
	{
		// Check if we're on a mobile device
	    if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) 
		{
			
			// Detect multi touch
	        if (Input.touchCount > 0)
			{
		        foreach (Touch touch in Input.touches)
				{
					// Check if GUILayers are pressed. If not, then set destination
					if (touch.phase == TouchPhase.Began)
						if(guiLayer.HitTest(touch.position))
							denyTouch = touch.fingerId;
					if (touch.fingerId != denyTouch)
			    		SetDestination(touch.position);
					if (touch.phase == TouchPhase.Ended)
						if (touch.fingerId == denyTouch)
							denyTouch = -1;
		        }
			}	        
	    }
	    // Check for mouse clicks instead of touch if on PC/Mac/Linux
	    else if (Input.GetMouseButtonDown(0))
		{
			if(!guiLayer.HitTest(Input.mousePosition))
	    		SetDestination(Input.mousePosition);
		}

		SetupAgentLocomotion();
	}
}
