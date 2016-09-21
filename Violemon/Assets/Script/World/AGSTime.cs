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
		protected List<FixedTimeEventObj> m_FixedTimeEvents = new List<FixedTimeEventObj> ();
		protected List<IntervalEventObj> m_IntervalEvents = new List<IntervalEventObj> ();
		// Use this for initialization
		void Start () {
			m_CurrentTime = 12 * 60 * 60;
		}
		
		// Update is called once per frame
		void Update () {
			m_CurrentTime += Time.deltaTime * (m_RealWorldFullDayDuration/GameConstants.DayDuration);
			if (m_CurrentTime >= m_RealWorldFullDayDuration) {
				foreach (FixedTimeEventObj eo in m_FixedTimeEvents)
				{
					eo.m_Excuted = false;
				}
				m_CurrentTime -= m_RealWorldFullDayDuration;
			}

			foreach (FixedTimeEventObj eo in m_FixedTimeEvents)
			{
				if(eo.m_Time <= m_CurrentTime && eo.m_Excuted == false)
				{
					eo.m_Event.Exec();
					eo.m_Excuted = true;
				}
			}


			foreach (IntervalEventObj ie in m_IntervalEvents)
			{
				if(ie.m_Amount <= 0f)
				{
					ie.m_Amount += ie.m_Interval;
					ie.m_Event.Exec();
				}
				ie.m_Amount -= Time.deltaTime;
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
			FixedTimeEventObj e = new FixedTimeEventObj(m_Time, m_Event);
			m_FixedTimeEvents.Add (e);
		}

		public void AddIntervalEvent(float m_Time, AGSEvent m_Event)
		{
			IntervalEventObj e = new IntervalEventObj(m_Time, m_Event);
			m_IntervalEvents.Add (e);
		}

		public class FixedTimeEventObj {

			public FixedTimeEventObj(float m_Time, AGSEvent m_Event)
			{
				this.m_Time = m_Time;
				this.m_Event = m_Event;
			}
			public AGSEvent m_Event;
			public float    m_Time;
			public bool     m_Excuted = false;
		}

		public class IntervalEventObj {
			
			public IntervalEventObj(float m_Interval, AGSEvent m_Event)
			{
				this.m_Interval = m_Interval;
				this.m_Event = m_Event;
				this.m_Amount = m_Interval;
			}
			public AGSEvent m_Event;
			public float    m_Interval;
			public float    m_Amount;
		}
	}


}