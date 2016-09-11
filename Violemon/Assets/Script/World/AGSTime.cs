using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AGS.Config;
using AGS.Util;

namespace AGS.World
{
	public class AGSTime : MonoBehaviour {
		private float      m_RealWorldFullDayDuration = 24 * 60 * 60;
		public  float      m_CurrentTime;
		private float      m_Round = 360f;
		protected List<EventObj> m_Events = new List<EventObj> ();
		// Use this for initialization
		void Start () {
			m_CurrentTime = 12 * 60 * 60;
		}
		
		// Update is called once per frame
		void Update () {
			m_CurrentTime += Time.deltaTime * (m_RealWorldFullDayDuration/GameConstants.DayDuration);
			if (m_CurrentTime >= m_RealWorldFullDayDuration) {
				foreach (EventObj eo in m_Events)
				{
					eo.m_Excuted = false;
				}
				m_CurrentTime -= m_RealWorldFullDayDuration;
			}

			foreach (EventObj eo in m_Events)
			{
				if(eo.m_Time <= m_CurrentTime && eo.m_Excuted == false)
				{
					eo.m_Event.Exec();
					eo.m_Excuted = true;
				}
			}

		}
		
		public float CurrentTime
		{
			get{
				return m_CurrentTime;
			}
		}

		public float CurrentSunDirection
		{
			get{
				return m_Round * (m_CurrentTime / m_RealWorldFullDayDuration);
			}
		}

		public void AddFixedTimeEvent(float m_Time, AGSEvent m_Event)
		{
			EventObj e = new EventObj(m_Time, m_Event);
			m_Events.Add (e);
		}

		public class EventObj {

			public EventObj(float m_Time, AGSEvent m_Event)
			{
				this.m_Time = m_Time;
				this.m_Event = m_Event;
			}
			public AGSEvent m_Event;
			public float    m_Time;
			public bool     m_Excuted = false;
		}
	}


}