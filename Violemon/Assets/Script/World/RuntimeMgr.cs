using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AGS.Characters;

namespace AGS.World
{
	public class RuntimeMgr : MonoBehaviour {
		protected List<SpawnPointBean> m_SpawnPoints = new List<SpawnPointBean> ();
		protected List<SpawnPointBean> m_ActiveSpawnPoints = new List<SpawnPointBean> ();
		private float m_LastCall = 0f;
		private float m_Interval = 1f;
		private float m_OutterDis = 100f;
		private float m_InnerDis = 60f;
		private float m_UpdateDis = 20f;
		private Vector3 m_LastPosition;
		private GameObject m_Violemon;
		// Use this for initialization
		void Start () {
			GameObject[] points = GameObject.FindGameObjectsWithTag ("SpawnPoint");
			SpawnPointBean bean = null;
			m_Violemon = GameObject.Find ("Violemon");
			foreach (GameObject point in points) {
				bean = new SpawnPointBean ();
				bean.Position = point.transform.position;
				m_SpawnPoints.Add (bean);
				GameObject.DestroyImmediate (point);
			}
			m_LastPosition = m_Violemon.transform.position;
			DoUpdateActiveSpawnPoints ();
		}

		// Update is called once per frame
		void Update () {
			m_LastCall += Time.deltaTime;
			if (m_LastCall >= m_Interval) {
				ManageRuntime ();
				m_LastCall -= m_Interval;
			}
		}

		private void ManageRuntime()
		{
			UpdateActiveSpawnPoints ();
			SpawnTest ();
		}

		private void SpawnTest()
		{
			if (m_ActiveSpawnPoints.Count <= 0) {
				return;
			}
			int index = Mathf.FloorToInt(Random.value * (m_ActiveSpawnPoints.Count - 1));
			SpawnPointBean bean = m_ActiveSpawnPoints [index];
			GameObject obj = CharacterMetaDataMgr.Instance ().GeneratCharacter (CharacterIDs.Character_HumanKnight, bean.Position);
			if (obj == null) {
				return;
			}
			NPC npc = obj.GetComponent<NPC>();
			//npc.SetMoveTarget (m_Violemon.transform.position);
			npc.SetTarget (m_Violemon);
		}

		private void UpdateActiveSpawnPoints()
		{
			float dis = Vector3.Distance (m_Violemon.transform.position, m_LastPosition);
			if (dis >= m_UpdateDis) {
				DoUpdateActiveSpawnPoints ();
			}
		}

		private void DoUpdateActiveSpawnPoints()
		{
			m_ActiveSpawnPoints.Clear ();
			foreach (SpawnPointBean point in m_SpawnPoints) {
				float dis = Vector3.Distance (m_Violemon.transform.position, point.Position);
				if (dis <= m_OutterDis && dis >= m_InnerDis) {
					m_ActiveSpawnPoints.Add (point);
				}
			}
		}
	}
}