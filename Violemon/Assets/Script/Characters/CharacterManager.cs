using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AGS.Characters
{
	public class CharacterManager{
		private static Dictionary<string,GameObject>  m_CharacterTemplates = new Dictionary<string,GameObject> ();
		private static CharacterManager m_Instance;
		private GameObject m_Violemon;
		private Vector3 m_TempPoint = new Vector3(-40, 2, -15);

		public static CharacterManager Instance()
		{
			if (m_Instance == null) {
				m_Instance = new CharacterManager();
			}
			return m_Instance;
		}
		
		private CharacterManager(){
		}
		
		public void Init()
		{
			m_Violemon = GameObject.Find ("Violemon");

			GameObject character = (GameObject)Resources.Load ("Prefabs/Characters/Human/HumanKnight/HumanKnight");
			Debug.Log ("333333 " + character);
			AddItem (CharacterIDs.Character_HumanKnight, character);
			//Debug.Log ("44444" + consumableItemTemp.m_GameObject);
		}

		private void AddItem(string key, GameObject character)
		{
			if (m_CharacterTemplates.ContainsKey (key)) {
				throw new UnityException("Duplicated id: " + key);
			}
			
			m_CharacterTemplates.Add (key, character);
		}

		public void GeneratCharacter(string id)
		{
			GameObject character = m_CharacterTemplates[id];
			if (character == null) {
				throw new UnityException("Obj does not exist: " + id);
			}
			GameObject obj = (MonoBehaviour.Instantiate (character, m_TempPoint, Quaternion.identity) as GameObject);
			NPC npc = obj.GetComponent<NPC>();
			npc.SetTarget (m_Violemon);

		}
	}
}