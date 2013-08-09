using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;


[CustomEditor(typeof(SpellManager))]

public class SpellManagerEditor : Editor {
	
	public override void OnInspectorGUI() {
		SpellManager SpellManager = (SpellManager)target;
		
		EditorGUIUtility.LookLikeControls();
		if (Application.isPlaying) {
			//return;
		}
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Icon Margin");
		SpellManager.margin = EditorGUILayout.FloatField(SpellManager.margin);
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Icon Square Size");
		SpellManager.squareSize = EditorGUILayout.FloatField(SpellManager.squareSize);
		EditorGUILayout.EndHorizontal();

		if (SpellManager.Spells!=null) for (int i = 0; i < SpellManager.Spells.Count; i++) {
			SpellManager.SpellItem Spells = SpellManager.Spells[i];
			
			EditorGUILayout.BeginVertical(new GUIStyle("box"));
			EditorGUILayout.BeginHorizontal();
			Spells.show = EditorGUILayout.Foldout(Spells.show, Spells.Name.Substring(0, Mathf.Min(40,Spells.Name.Length)));
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("delete")) {
				SpellManager.Spells.RemoveAt(i);
			}
			EditorGUILayout.EndHorizontal();
			if (Spells.show) {
				EditorGUILayout.BeginHorizontal(); EditorGUILayout.Space(); EditorGUILayout.BeginVertical();
				
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Spell Chosen?");
				Spells.IsChosen = EditorGUILayout.Toggle(Spells.IsChosen);
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Targets");
				Spells.targets = (int)((SpellManager.Targets)EditorGUILayout.EnumMaskField("select targets: ", (SpellManager.Targets)Spells.targets));
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Spell Sound Effect");
				Spells.spellSound = (AudioClip)EditorGUILayout.ObjectField(Spells.spellSound, typeof(AudioClip));
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Damage Type");
				Spells.damageType = (SpellManager.DamageTypes)EditorGUILayout.EnumPopup(Spells.damageType);
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Initial Particle Effect");
				Spells.initEffect = (GameObject)EditorGUILayout.ObjectField(Spells.initEffect, typeof(GameObject));
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("On Hit Particle Effect");
				Spells.onHitEffect = (GameObject)EditorGUILayout.ObjectField(Spells.onHitEffect, typeof(GameObject));
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Spell Icon");
				Spells.texture = (Texture2D)EditorGUILayout.ObjectField(Spells.texture, typeof(Texture2D));
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Spell Name");
				Spells.Name = EditorGUILayout.TextArea(Spells.Name);
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Spell Damage");
				Spells.Damage =  EditorGUILayout.FloatField(Spells.Damage);
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Spell Healing");
				Spells.Heal =  EditorGUILayout.FloatField(Spells.Heal);
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Spell Range (Global?)");
				Spells.Global = EditorGUILayout.Toggle(Spells.Global,  GUILayout.MaxWidth(20));
				if (Spells.Global)
					EditorGUILayout.FloatField(Mathf.Infinity);
				else
					Spells.Range = EditorGUILayout.FloatField(Spells.Range);
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("Spell Cooldown");
				Spells.Cooldown =  EditorGUILayout.FloatField(Spells.Cooldown);
				EditorGUILayout.EndHorizontal();
				
				
				EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("Add new condition")) {
					if (SpellManager.Spells[i].Conditions==null) {
						SpellManager.Spells[i].Conditions = new List<SpellManager.SpellItem.ConditionItem>();
					}
					SpellManager.Spells[i].Conditions.Add(new SpellManager.SpellItem.ConditionItem());
					EditorUtility.SetDirty(SpellManager);
				}	
				EditorGUILayout.EndHorizontal();
				
					// Loop through and show the conditions
					if (Spells.Conditions!=null) for (int n = 0; n < Spells.Conditions.Count; n++) {
						SpellManager.SpellItem.ConditionItem Condition = SpellManager.Spells[i].Conditions[n];
						EditorGUILayout.Separator(); // <-- Maybe not?
						
						EditorGUILayout.BeginHorizontal();
						Condition.showCond = EditorGUILayout.Foldout(Condition.showCond, Condition.Condition.ToString());
						GUILayout.FlexibleSpace();
						if (GUILayout.Button("delete")) {
							Spells.Conditions.RemoveAt(n);
						}
						EditorGUILayout.EndHorizontal();
					
						if (Condition.showCond) {
							EditorGUILayout.BeginHorizontal(); EditorGUILayout.Space(); EditorGUILayout.BeginVertical();
							EditorGUILayout.Separator(); // <-- Maybe not?
							
							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.PrefixLabel("Condition Type");
							Condition.Condition = (SpellManager.ConditionTypes)EditorGUILayout.EnumPopup(Condition.Condition);
							EditorGUILayout.EndHorizontal();
							
							EditorGUILayout.BeginHorizontal();
							EditorGUILayout.PrefixLabel("Condition Duration");
							Condition.Duration = EditorGUILayout.FloatField(Condition.Duration);
							EditorGUILayout.EndHorizontal();
							
							// Show different variables for conditions
							ConditionVariables(Condition);
							
							EditorGUILayout.EndVertical(); EditorGUILayout.EndHorizontal();	
						}
					}
					EditorGUILayout.Separator();
				
				EditorGUILayout.EndVertical(); EditorGUILayout.EndHorizontal();		
			}
			
				EditorGUILayout.EndVertical();
				EditorGUILayout.Separator();
			} // endof loop over Spells elements

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Add new spell")) {
			if (SpellManager.Spells==null) {
				SpellManager.Spells = new List<SpellManager.SpellItem>();
			}
			SpellManager.Spells.Add(new SpellManager.SpellItem());
			EditorUtility.SetDirty(SpellManager);
		}
		EditorGUILayout.EndHorizontal();
		
	}


	void Resort() {
		
	}
	
	void ConditionVariables(SpellManager.SpellItem.ConditionItem Condition) {
		if (Condition.Condition.ToString() == "DamagePerSecond")
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Damage Per Second");
			Condition.DmgPrSec = EditorGUILayout.FloatField(Condition.DmgPrSec);
			EditorGUILayout.EndHorizontal();
		}
		if (Condition.Condition.ToString() == "Slow")
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Slow Percentage");
			Condition.DmgPrSec = EditorGUILayout.Slider(Condition.DmgPrSec, 0.01f, 1.0f);
			EditorGUILayout.EndHorizontal();
		}
		if (Condition.Condition.ToString() == "ImprovePhysDef")
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Improved Physical Def");
			Condition.ImprovePhysDef = EditorGUILayout.Slider(Condition.ImprovePhysDef, 0.01f, 100.0f);
			EditorGUILayout.EndHorizontal();
		}
		if (Condition.Condition.ToString() == "ImproveMagiDef")
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Improved Magical Def");
			Condition.ImproveMagiDef = EditorGUILayout.Slider(Condition.ImproveMagiDef, 0.01f, 100.0f);
			EditorGUILayout.EndHorizontal();
		}
		if (Condition.Condition.ToString() == "DecreasePhysDef")
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Decrease Physical Def");
			Condition.DecreasePhysDef = EditorGUILayout.Slider(Condition.DecreasePhysDef, 0.01f, 100.0f);
			EditorGUILayout.EndHorizontal();
		}
		if (Condition.Condition.ToString() == "DecreaseMagiDef")
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Decrease Magical Def");
			Condition.DecreaseMagiDef = EditorGUILayout.Slider(Condition.DecreaseMagiDef, 0.01f, 100.0f);
			EditorGUILayout.EndHorizontal();
		}
	}
}