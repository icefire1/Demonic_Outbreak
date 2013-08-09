using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageAndConditionControl : MonoBehaviour {
	public float health = 100.0f;
	public float BasePhysicalDefense = 10.0f;
	public float BaseMagicalDefense = 10.0f;
	
	private float physDefModifier = 0.0f;
	private float magiDefModifier = 0.0f;
	private float dmgPerSec = 0.0f;
	private float dealDmgPerSec = 0.0f;
	private float dmgPerSecInterval = 0.5f;
	private float dmgPerSecTimer = 0.5f;
	private ArrayList dmgPerSecArray = new ArrayList(); 
	
	private List<SpellManager.SpellItem.ConditionItem> Conditions = new List<SpellManager.SpellItem.ConditionItem>(); 
	private List<float> ConditionTimers = new List<float>();
	private List<GameObject> onHitParticles = new List<GameObject>();
	
	void Start () {
		dmgPerSecArray.Add("Magical");
		dmgPerSecArray.Add(0);
	}
	
	void Update () {
		if (Conditions != null)
			ControlConditions ();
		if (onHitParticles != null)
			moveParticles ();
		
		// This script shouldn't kill any objects
		if (health <= 0.0f)
			Destroy(gameObject);
	}
	
	void moveParticles ()
	{
		for (var i = 0; i < onHitParticles.Count; i++)
		{
			if (onHitParticles[i])
				onHitParticles[i].transform.position = transform.position;
			else
				onHitParticles.RemoveAt(i);
		}
	}
	
	void ControlConditions ()
	{
		physDefModifier = 100.0f;
		magiDefModifier = 100.0f;
		dmgPerSec = 0.0f;
		float time = Time.time;
		dmgPerSecTimer += Time.deltaTime;
		for (var i = 0; i < Conditions.Count; i++)
		{
			string conditionName = Conditions[i].Condition.ToString();
			float conditionDuration = Conditions[i].Duration;
			if (conditionDuration >= time - ConditionTimers[i])
			{
				// Find out what kind of condition it is
				// and apply it's affect
				// Notice; their effects are undone automatically
				if (conditionName == "ImprovePhysDef")
					physDefModifier += Conditions[i].ImprovePhysDef;
				if (conditionName == "ImproveMagiDef")
					magiDefModifier += Conditions[i].ImproveMagiDef;
				if (conditionName == "DecreasePhysDef")
					physDefModifier -= Conditions[i].DecreasePhysDef;
				if (conditionName == "DecreaseMagiDef")
					magiDefModifier -= Conditions[i].DecreaseMagiDef;
				if (conditionName == "DamagePerSecond")
					dmgPerSec += Conditions[i].DmgPrSec;
			}
			else
			{
				Conditions.RemoveAt(i);
				ConditionTimers.RemoveAt(i);
			}
		}
		dealDmgPerSec += dmgPerSec * Time.deltaTime;
		if (dmgPerSecTimer >= dmgPerSecInterval || dmgPerSec == 0.0f)
		{
			dmgPerSecTimer = 0.0f;
			dmgPerSecArray[1] = dealDmgPerSec;
			ApplyDamage(dmgPerSecArray);
			dealDmgPerSec = 0.0f;
		}
	}
	
	void ApplyDamage (ArrayList damageArr)
	{
		string type = (string) damageArr[0];
		float damage = (float) damageArr[1];
		float physicalDefense = BasePhysicalDefense * (physDefModifier/100.0f);
		float magicalDefense = BaseMagicalDefense * (magiDefModifier/100.0f);
		if (type == "Physical")
			health -= damage - damage * physicalDefense * 0.01f;	
		if (type == "Magical")
			health -= damage - damage * magicalDefense * 0.01f;		 
	}
	
	void ApplyConditions (List<SpellManager.SpellItem.ConditionItem> conditionArr)
	{
		float time = Time.time;
		for (var i = 0; i < conditionArr.Count; i++)
		{
			// Add all the conditions
			Conditions.Add(conditionArr[i]);
			// And set a timer for the durations
			ConditionTimers.Add(time);
		}
	}
	
	void ApplyOnHitParticle (GameObject particleEffect)
	{
		Quaternion q = new Quaternion();
		q.SetLookRotation(Vector3.forward, Vector3.forward);
		GameObject particle = particleEffect;
		GameObject particleClone = (GameObject)Instantiate(particle, transform.position, q) as GameObject;
		onHitParticles.Add(particleClone);
	}
}
