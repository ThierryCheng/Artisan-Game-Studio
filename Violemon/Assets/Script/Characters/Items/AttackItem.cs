using UnityEngine;
using System.Collections;

namespace AGS.Characters
{
    public class AttackItem 
	{

		public int m_HitPoint;
		public int m_Stun
		{
			set
			{
				if(value < 0 || value > 7)
					throw new UnityException();
				m_Stun = value;
		    }
			get
			{
				return m_Stun;
		    }
		}
		public int m_BlowBack
		{
			set
			{
				if(value < 0 || value > 7)
					throw new UnityException();
				m_BlowBack = value;
			}
			get
			{
				return m_BlowBack;
			}
		}
		public int m_SlowDown
		{
			set
			{
				if(value < 0 || value > 7)
					throw new UnityException();
				m_SlowDown = value;
			}
			get
			{
				return m_SlowDown;
			}
		}
    }
}