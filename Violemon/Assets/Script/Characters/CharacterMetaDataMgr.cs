using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AGS.Characters
{
	public class CharacterMetaDataMgr{
		private static Dictionary<string,GameObject>  m_CharacterTemplates = new Dictionary<string,GameObject> ();
		private static CharacterMetaDataMgr m_Instance;
		private GameObject m_Violemon;
		//private Vector3 m_TempPoint = new Vector3(-40, 2, -15);

		public static CharacterMetaDataMgr Instance()
		{
			if (m_Instance == null) {
				m_Instance = new CharacterMetaDataMgr();
			}
			return m_Instance;
		}
		
		private CharacterMetaDataMgr(){
		}
		
		public void Init()
		{
			m_Violemon = GameObject.Find ("Violemon");

			GameObject character = (GameObject)Resources.Load ("Prefabs/Characters/Human/HumanKnight/HumanKnight");
			AddItem (CharacterIDs.Character_HumanKnight, character);
		}

		private void AddItem(string key, GameObject character)
		{
			if (m_CharacterTemplates.ContainsKey (key)) {
				throw new UnityException("Duplicated id: " + key);
			}
			
			m_CharacterTemplates.Add (key, character);
		}

		public GameObject GeneratCharacter(string id, Vector3 position)
		{
			GameObject character = m_CharacterTemplates[id];
			if (character == null) {
				throw new UnityException("Obj does not exist: " + id);
			}
			GameObject obj = null;
			try{
			    obj = (MonoBehaviour.Instantiate (character, position, Quaternion.identity) as GameObject);
			}
			catch(Exception e)
			{
				Debug.Log(e.StackTrace);
			}
			return obj;
			//NPC npc = obj.GetComponent<NPC>();
			//npc.SetTarget (m_Violemon);

		}
	}
}