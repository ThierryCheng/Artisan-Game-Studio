using UnityEngine;
using System.Collections;
using AGS.Config;

namespace AGS.Characters
{
	public class Human : NPC
	{
		protected  new void Start()
		{
			base.Start ();
			//m_MoveTarget = new Vector3 (1000, 0, 1000);
			gameObject.tag = TagManager.HUMAN;
			//m_HitPoints = 200;
			//gameObject.layer = "Human";

		}

		protected override void UpdateSubAttributes()
		{
				//Debug.Log ("Called in Player");
		}

		protected override void ActionCallBack(string name)
		{
			if(TargetInRange(m_CanBeAttacked, m_ActionPerformedTarget))
			{
				

				AttackItem item = GameConstants.GetAttackItem("HumanKnight_" + name);
				if(item != null)
				{
					item.KnockBackDirection = transform.TransformDirection(Vector3.forward);
					BaseCharacter bc = m_ActionPerformedTarget.GetComponent<BaseCharacter>();
					bc.Attacked(item);
					if(m_ActionPerformedTarget == m_ActionTarget && bc.IsDead())
					{
						m_ActionTarget = null;
						
					}
				}
			}
		}
	}
}