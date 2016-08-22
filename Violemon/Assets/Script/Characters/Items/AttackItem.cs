using UnityEngine;
using System.Collections;
using AGS.Config;

namespace AGS.Characters
{
    public class AttackItem 
	{
		private int m_HitPoint; 
		private float m_Stun; 
		private float m_KnockBack; 
		private float m_SlowDown; 
		public int HitPoint
		{
			set
			{
				m_HitPoint = value;
			}
			get
			{
				return m_HitPoint;
			}
		}
		public float Stun
		{
			set
			{
				if(value < 0 || value > GameConstants.MaxStunPower)
					throw new UnityException();
				m_Stun = value;
		    }
			get
			{
				return m_Stun;
		    }
		}
		public float KnockBack
		{
			set
			{
				if(value < 0 || value > GameConstants.MaxKnockBackPower)
					throw new UnityException();
				m_KnockBack = value;
			}
			get
			{
				return m_KnockBack;
			}
		}
		public float SlowDown
		{
			set
			{
				if(value < 0 || value > GameConstants.MaxSlowDownPower)
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