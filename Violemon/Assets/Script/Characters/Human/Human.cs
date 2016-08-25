using UnityEngine;
using System.Collections;

namespace AGS.Characters
{
	public class Human : BaseCharacter
	{
		protected void Start()
		{
			base.Start ();
			//m_MoveTarget = new Vector3 (1000, 0, 1000);
			gameObject.tag = "Human";
			m_HitPoints = 100;
			//gameObject.layer = "Human";

		}

		protected void ActionCallBack(string name)
		{
			m_BlockMove = false;
			if(TargetInRange(m_CanBeAttacked))
			{
				
				AttackItem item = new AttackItem();
				item.Damage = 40;
				item.SlowDown = 3.0f;
				item.Stun = 4.0f;
				BaseCharacter bc = m_ActionTarget.GetComponent<BaseCharacter>();
				bc.Attacked(item);
				if(bc.IsDead())
				{
					m_ActionTarget = null;
					m_BlockMove = false;
					return;
				}
			}
			//m_Animator.SetBool ("Run", true);
		}
	}
}