using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace AGS.Editors
{
	public class SpawnObjs{
		public string[][] m_ObjsMap;
		public GameObject[] m_GameObjs;
		private static SpawnObjs m_Instance;
		public static SpawnObjs Instance()
		{
			if (m_Instance == null)
				m_Instance = new SpawnObjs ();
			return m_Instance;
		}


		public SpawnObjs()
		{
			m_ObjsMap = new string[][] {
				new string[]{"Apple", "Prefabs/Items/Apple"},
				new string[]{"Violemon", "Prefabs/Characters/Violemon/Violemon"},
				new string[]{"Human Knight", "Prefabs/Characters/Human/HumanKnight/HumanKnight"},
				new string[]{"Wasted Land", "Prefabs/Scene Model/Wasteland_Plate"},
				new string[]{"Greenland", "Prefabs/Scene Model/Greenland"},
				new string[]{"Forest_Tile_1", "Prefabs/Tile/Forest_Tile_1"},
				new string[]{"Frozen_Tile_1", "Prefabs/Tile/Frozen_Tile_1"},
				new string[]{"Frozen_Tile_2", "Prefabs/Tile/Frozen_Tile_2"},
				new string[]{"Frozen_Tile_3", "Prefabs/Tile/Frozen_Tile_3"},
				new string[]{"Grass", "Prefabs/Scene Model/Grassland/Grass"},
				new string[]{"Camel thorn", "Prefabs/Scene Model/Wasteland/Camel thorn"},
				new string[]{"WitheredTree", "Prefabs/Scene Model/Wasteland/WitheredTree"},
				new string[]{"SpawnPoint", "Prefabs/World/SpawnPoint"}
			};


			//LoadObjs ();
		}

		public void LoadObjs()
		{
			m_GameObjs = new GameObject[m_ObjsMap.Length];
			for (int i = 0; i < m_ObjsMap.Length; i++) {
				m_GameObjs[i] = (GameObject)Resources.Load (m_ObjsMap[i][1]);
				if (m_GameObjs [i] == null) {
					new UnityException ("Failed to load " + m_ObjsMap[i][0]);
					//Debug.Log ("Failed to load " + m_ObjsMap[i][0]);
				}
			}
		}
	}

}