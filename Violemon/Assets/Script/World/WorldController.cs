using UnityEngine;
using System.Collections;
using AGS.Config;
using AGS.Util;
using AGS.Items;
using AGS.Characters;

namespace AGS.World
{
	[RequireComponent(typeof(AGSTime))]
	[RequireComponent(typeof(RuntimeMgr))]
	public class WorldController : MonoBehaviour {
		
		private GameObject m_DirectionalLight;
		private Quaternion m_OriLightRotation = Quaternion.Euler (-60f, -180f, -0f);
		private AGSTime    m_AGSTime;
		private RuntimeMgr m_RuntimeMgr;
		private float      m_LastUpdateSunDirectionTime;
		public  Material   m_PurpleNebula;
		private Material   m_OriSkybox;

		void Start () {
			m_DirectionalLight = GameObject.Find ("Directional Light");
			if(m_DirectionalLight == null)
			{
				return;
			}
			//m_Violemon = GameObject.Find ("Violemon");
			if(m_DirectionalLight == null)
			{
				return;
			}
			//m_DirectionalLight.transform.LookAt (m_Violemon.transform.position);
			//m_DirectionalLight.transform.rotation = m_OriLightRotation;
			m_AGSTime = gameObject.GetComponent<AGSTime> ();
			m_RuntimeMgr = gameObject.GetComponent<RuntimeMgr> ();
			//m_OriLightDir = m_DirectionalLight.transform.TransformDirection (Vector3.forward);
			//m_CurrentTime = 6 * 60 * 60;
			//m_PurpleNebula = Resources.Load("Test/Skyboxes/PurpleNebula/PurpleNebula") as Material;



			m_OriSkybox = RenderSettings.skybox;
			//AGSEvent changeSkyboxEvent = new AGSEvent (){};
			if (m_PurpleNebula != null) {
				AGSEvent changeSkyboxEvent = new ChangeSkyboxEvent (m_PurpleNebula);
				m_AGSTime.AddFixedTimeEvent (18 * 60 * 60, changeSkyboxEvent);
				changeSkyboxEvent = new ChangeSkyboxEvent (m_OriSkybox);
				m_AGSTime.AddFixedTimeEvent (6 * 60 * 60, changeSkyboxEvent);
			}

			ItemManager.Instance ().Init ();
			CharacterMetaDataMgr.Instance ().Init ();
		}
		
		// Update is called once per frame
		void Update () {
			if (GameConstants.UpdateSunDirectionDuration >= 0f) {
				if ((Time.time - m_LastUpdateSunDirectionTime) >= GameConstants.UpdateSunDirectionDuration) {
					m_LastUpdateSunDirectionTime = Time.time;
					UpdateSunDirection ();
				}
			} else {
				UpdateSunDirection();
			}
			//CurrentTime ();
		}

		private void UpdateSunDirection()
		{
			m_DirectionalLight.transform.rotation = m_OriLightRotation;
			m_DirectionalLight.transform.Rotate (0, m_AGSTime.CurrentSunDirection, 0);
		}
		private class ChangeSkyboxEvent : AGSEvent{

			public Material m_PurpleNebula;

			public ChangeSkyboxEvent(Material m_PurpleNebula)
			{
				this.m_PurpleNebula = m_PurpleNebula;
			}

			public void Exec()
			{
				RenderSettings.skybox = m_PurpleNebula;

			}
		}
	}
}
