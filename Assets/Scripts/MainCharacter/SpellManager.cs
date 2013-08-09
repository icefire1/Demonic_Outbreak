using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]	
public class SpellManager : MonoBehaviour {
	public enum Targets
    {
	    Allies        = 1,
	    Enemies       = 2,
	    MainCharacter = 4,
    }
	public enum DamageTypes {
		Physical, Magical
	}
	
	public enum ConditionTypes {
		Stun, 
		DamagePerSecond,
		Slow,
		ImprovePhysDef, 
		ImproveMagiDef,
		DecreasePhysDef,
		DecreaseMagiDef,
	}

	[Serializable]	
	public class SpellItem {
		public bool show = true; // Editor variable
		
		public int targets = 4;
		
		// Spell variables
		public AudioClip spellSound = null;
		public DamageTypes damageType = DamageTypes.Physical;
		public GameObject initEffect = null;
		public GameObject onHitEffect = null;
		public Texture2D texture = null;
		public string Name = "(enter spell name)";
		public float Damage = 10.0f;
		public float Range = 3.0f;
		public bool Global = false; // NEEDS TO BE IMPLEMENTED
		public float Cooldown = 4.0f;
		public bool IsChosen = false;
		public float CooldownTimer = 0.0f;
		public float Heal = 0.0f;
		
		public bool onCooldown = false;
		
		// Condition variables
		[Serializable]
		public class ConditionItem {
			public bool showCond = true; // Editor variable
			
			public ConditionTypes Condition = ConditionTypes.Stun;
			public float Duration = 2.0f;
			public float DmgPrSec = 0.0f;
			public float SlowPercent = 0.0f;
			public float ImprovePhysDef = 0.0f;
			public float ImproveMagiDef = 0.0f;
			public float DecreasePhysDef = 0.0f;
			public float DecreaseMagiDef = 0.0f;
		}
		public List<ConditionItem> Conditions;
	}

	public List<SpellItem> Spells;
	
	public float margin = 10.0f;
	public float squareSize = 80.0f;
	
	private int allowedSpellSlots = 4; 
	private List<GameObject> spellSlotObjects = new List<GameObject>();   
	private GUILayer guiLayer;
	
	// For input checking
	private GUIElement beganPressedTarget = null;
	private GUIElement endedPressedTarget;
	
	private Vector3 gizmoPos;
	private float gizmoRadius;
	
//		public Targets targets2 = Targets.Enemies;
	void Start ()
	{
//		print(targets2.GetHashCode());
		// Make a GUILayer for hitTests
		guiLayer = Camera.main.GetComponent<GUILayer>();
		// Spawn all the GUITextures that represents the spell icons!
		GameObject guiSpellSlots = new GameObject();
		guiSpellSlots.name = "guiSpellSlots";
		int spellSlot = 0;
		GameObject guiTexture = new GameObject();
		GUITexture placeholder;
		Rect newInset;
		// Decide how many spell slots are needed
		for (int i = 0; i < Spells.Count; i++)
			if (Spells[i].IsChosen && spellSlot < allowedSpellSlots)
			{
				// Instantiate GUITextures and set their positions
				spellSlotObjects.Add((GameObject)Instantiate(guiTexture, new Vector3(1, 0, 0), Quaternion.identity) as GameObject);
				spellSlotObjects[spellSlot].transform.localScale = new Vector3(0,0,1f);
				spellSlotObjects[spellSlot].transform.parent = guiSpellSlots.transform;
				spellSlotObjects[spellSlot].name = i.ToString();
				spellSlotObjects[spellSlot].AddComponent("GUITexture");
				placeholder = spellSlotObjects[spellSlot].GetComponent<GUITexture>();
				placeholder.texture = Spells[i].texture;
				newInset = new Rect(-(margin+squareSize)*(spellSlot+1), margin, squareSize, squareSize);
				placeholder.pixelInset = newInset;
			
				spellSlot++;
			}
	}
	
	void CastSpell (GameObject go)
	{
		int spellSlot = int.Parse(go.name);
		// Check if spell has cooldown
		if (!Spells[spellSlot].onCooldown)
		{
			Spells[spellSlot].onCooldown = true;
			print ("Cast spell: " + go.name);
			
			// Create the initial particle effect
			Quaternion q = new Quaternion();
			q.SetLookRotation(Vector3.forward, Vector3.forward);
			GameObject particle = Spells[spellSlot].initEffect;
			GameObject particleClone = (GameObject)Instantiate(particle, transform.position, q) as GameObject;
			
			// Play the spell audio, if any
			if (Spells[spellSlot].spellSound != null)
				particleClone.AddComponent("AudioSource");
				AudioSource audioSource = particleClone.GetComponent<AudioSource>();
				audioSource.clip = Spells[spellSlot].spellSound;
				audioSource.panLevel = 0.5f; //THIS IS HARDCODED!! But seems to feel right!
				particleClone.audio.Play();
			// Set the cooldown
			Spells[spellSlot].CooldownTimer = Time.time + Spells[spellSlot].Cooldown;
			
			// Apply the effect to selected targets
			if (Spells[spellSlot].targets != 0)
			{
				// THIS IS A HORRIBLE WAY OF DOING IT!!! :( So damned hardcoded,
				// but don't know any other way
				bool demons = false;
				bool angels = false;
				bool mainChar = false;
				var _targets = Spells[spellSlot].targets;
				if (_targets == -1)
				{
					demons = true;
					angels = true;
					mainChar = true;
				}
				else if (_targets < 0) _targets *= -1;
				if (_targets == 4 || _targets == 2 || _targets == 3)
					mainChar = true;
				if (_targets == 6 || _targets == 2 || _targets == 5)
					demons = true;
				if (_targets == 7 || _targets == 5 || _targets == 3)
					angels = true;
				
				// Make the damage and condition arrays
				var damageArray = new ArrayList(); 
				damageArray.Add(Spells[spellSlot].damageType.ToString ());
				damageArray.Add(Spells[spellSlot].Damage); 
				List<SpellItem.ConditionItem> conditionArray = new List<SpellItem.ConditionItem>();
				if (Spells[spellSlot].Conditions != null)
				{
					foreach (SpellItem.ConditionItem condition in Spells[spellSlot].Conditions)
						conditionArray.Add(condition);
				}
				
				// Check if the spell range is global
				if (!Spells[spellSlot].Global)
				{
					var hitColliders = Physics.OverlapSphere(particleClone.transform.position, Spells[spellSlot].Range);
					// Send damage and conditons to AIs
					if (demons || angels)
					{
						for (var i = 0; i < hitColliders.Length; i++) {
							if (hitColliders[i].tag == "Demon" && demons)
								hitColliders[i].SendMessage("ApplyDamage", damageArray, SendMessageOptions.DontRequireReceiver);
								if (Spells[spellSlot].Conditions != null)
									hitColliders[i].SendMessage("ApplyConditions", conditionArray, SendMessageOptions.DontRequireReceiver);
								if (Spells[spellSlot].onHitEffect != null)
									hitColliders[i].SendMessage("ApplyOnHitParticle", Spells[spellSlot].onHitEffect, SendMessageOptions.DontRequireReceiver);
							if (hitColliders[i].tag == "Angel" && angels)
								hitColliders[i].SendMessage("ApplyDamage", damageArray, SendMessageOptions.DontRequireReceiver);
								if (Spells[spellSlot].Conditions != null)
									hitColliders[i].SendMessage("ApplyConditions", conditionArray, SendMessageOptions.DontRequireReceiver);
								if (Spells[spellSlot].onHitEffect != null)
									hitColliders[i].SendMessage("ApplyOnHitParticle", Spells[spellSlot].onHitEffect, SendMessageOptions.DontRequireReceiver);
						}
					}
				}	
				else
				{
					// Send damage and conditons to AIs
					if (angels)
					{
						GameObject[] _angels = GameObject.FindGameObjectsWithTag("Angel");
						for (var i = 0; i < _angels.Length; i++) {
							_angels[i].SendMessage("ApplyDamage", damageArray, SendMessageOptions.DontRequireReceiver);
							if (Spells[spellSlot].Conditions != null)
								_angels[i].SendMessage("ApplyConditions", conditionArray, SendMessageOptions.DontRequireReceiver);
							if (Spells[spellSlot].onHitEffect != null)
								_angels[i].SendMessage("ApplyOnHitParticle", Spells[spellSlot].onHitEffect, SendMessageOptions.DontRequireReceiver);
						}
					}
					// Send damage and conditons to AIs
					if (demons)
					{
						GameObject[] _demons = GameObject.FindGameObjectsWithTag("Demon");
						for (var n = 0; n < _demons.Length; n++) {
							_demons[n].SendMessage("ApplyDamage", damageArray, SendMessageOptions.DontRequireReceiver);
							if (Spells[spellSlot].Conditions != null)
								_demons[n].SendMessage("ApplyConditions", conditionArray, SendMessageOptions.DontRequireReceiver);
							if (Spells[spellSlot].onHitEffect != null)
								_demons[n].SendMessage("ApplyOnHitParticle", Spells[spellSlot].onHitEffect, SendMessageOptions.DontRequireReceiver);
						}
					}
				}
				// Send damage and conditons to player		
				if (mainChar)
				{
					GameObject player = GameObject.FindGameObjectWithTag("Player");
					player.SendMessage("ApplyDamage", damageArray, SendMessageOptions.DontRequireReceiver);
					if (Spells[spellSlot].onHitEffect != null)
						player.SendMessage("ApplyOnHitParticle", Spells[spellSlot].onHitEffect, SendMessageOptions.DontRequireReceiver);
				}
			}
			
			// GIZMO LINES
			gizmoPos = particleClone.transform.position;
			gizmoRadius = Spells[spellSlot].Range;
		}
		else
			print ("Spell: " + Spells[spellSlot].Name + " is on cooldown!");
	}
	
	void ShowCooldown(){
		int n = 0;
		for (var i = 0; i < Spells.Count; i++)
		{
			if (Spells[i].onCooldown && Spells[i].CooldownTimer > Time.time) 
			{
				Color color = new Color(0.2f, 0.3f, 0.2f, 1.0f);
				spellSlotObjects[n].GetComponent<GUITexture>().color = color;
			}
			else
			{
				Spells[i].onCooldown = false;
				spellSlotObjects[n].GetComponent<GUITexture>().color = Color.grey;
			}
			if (Spells[i].IsChosen) n++;
		}	
	}
	
	
	
    void OnDrawGizmos() {
		// GIZMO LINES
		if (gizmoRadius != null && gizmoPos != null)
		{
	        Gizmos.color = Color.white;
	        Gizmos.DrawWireSphere(gizmoPos, gizmoRadius);
		}
    }
	
	void Update ()
	{
		// Check for input
		// Check if we're on a mobile device
	    if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) 
		{
			
			// Detect multi touch
	        if (Input.touchCount > 0)
			{
		        foreach (Touch touch in Input.touches)
				{
					// Check if GUILayers are pressed
					if (touch.phase == TouchPhase.Began)
					{
						beganPressedTarget = guiLayer.HitTest(touch.position);
					}
							
					if (touch.phase == TouchPhase.Ended)
					{
						endedPressedTarget = guiLayer.HitTest(touch.position);
						if (beganPressedTarget != null)
							if (endedPressedTarget == beganPressedTarget)
								CastSpell (beganPressedTarget.gameObject);
					}
		        }
			}	        
	    }
	    // Check for mouse clicks instead of touch if on PC/Mac/Linux
	    else if (Input.GetMouseButtonDown(0))
		{
			GUIElement pressedTarget;
			pressedTarget = guiLayer.HitTest(Input.mousePosition);
			if(pressedTarget)
				CastSpell (pressedTarget.gameObject);
		}
		
		// Check if any spells are on cooldown
		ShowCooldown ();
	}
}