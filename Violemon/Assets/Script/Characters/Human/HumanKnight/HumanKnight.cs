using UnityEngine;
using System.Collections;
using AGS.Config;

namespace AGS.Characters
{
	public class HumanKnight : Human {
		
		protected void Start()
		{
			base.Start ();
			//gameObject.tag = "Hound";
			m_MaxHealth                 = GameConstants.HumanKnight_InitialMaxHealth;
			m_Health                    = GameConstants.HumanKnight_InitialMaxHealth;
			m_MaxStamina                = GameConstants.HumanKnight_InitialMaxStamina;
			m_Stamina                   = GameConstants.HumanKnight_InitialMaxStamina;
			m_AbleToAttack              = GameConstants.HumanKnight_AbleToAttack;
			m_AbleToAttackRadius        = GameConstants.HumanKnight_AbleToAttackRadius;
			m_CanBeAttacked             = GameConstants.HumanKnight_CanBeAttacked;
			m_CanBeAttackedRadius       = GameConstants.HumanKnight_CanBeAttackedRadius;
			m_TargetDirectionUpdateRate = GameConstants.HumanKnight_TargetDirectionUpdateRate;
			m_TurnMultiplier            = GameConstants.HumanKnight_TurnMultiplier;
			m_MoveSpeed                 = GameConstants.HumanKnight_MoveSpeed;
		}
		
		protected override void ActionCallBack(string name)
		{
			/*if(TargetInRange(m_CanBeAttacked, m_CanBeAttackedRadius, m_ActionPerformedTarget))
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
			}*/
			m_ActionTarget.ActionCallBack (name);
		}
	}
}