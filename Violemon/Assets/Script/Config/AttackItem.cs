using UnityEngine;
using System.Collections;

namespace AGS.Config
{
    public class AttackItem 
	{
		private int m_Damage; 
		private float m_Stun; 
		private float m_KnockBack; 
		private float m_SlowDown; 
		private Vector3 m_KnockBackDirection = Vector3.zero;

		public AttackItem(int Damage, float Stun, float KnockBack, float SlowDown)
		{
			this.Damage = Damage;
			this.Stun = Stun;
			this.KnockBack = KnockBack;
			this.SlowDown = SlowDown;
			this.KnockBackDirection = KnockBackDirection;
		}


		public Vector3 KnockBackDirection
		{
			set
			{
				m_KnockBackDirection = value;
			}
			get
			{
				return m_KnockBackDirection;
			}
		}
		public int Damage
		{
			set
			{
				m_Damage = value;
			}
			get
			{
				return m_Damage;
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