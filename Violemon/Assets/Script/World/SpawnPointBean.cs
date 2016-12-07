using UnityEngine;
using System.Collections;

namespace AGS.World
{
	public class SpawnPointBean {
		private Vector3 m_Position;

		public Vector3 Position
		{
			get{ 
				return m_Position;
			}
			set{ 
				m_Position = value;
			}
		}
	}
}