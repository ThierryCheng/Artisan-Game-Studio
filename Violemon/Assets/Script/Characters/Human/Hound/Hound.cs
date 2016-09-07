using UnityEngine;
using System.Collections;
using AGS.Config;

namespace AGS.Characters
{
	public class Hound : NPC {

		protected void Start()
		{
			base.Start ();
			//gameObject.tag = "Hound";
			m_MaxHealth                 = GameConstants.Hound_InitialMaxHealth;
			m_Health                    = GameConstants.Hound_InitialMaxHealth;
			m_MaxStamina                = GameConstants.Hound_InitialMaxStamina;
			m_Stamina                   = GameConstants.Hound_InitialMaxStamina;
			m_AbleToAttack              = GameConstants.Hound_AbleToAttack;
			m_AbleToAttackRadius        = GameConstants.Hound_AbleToAttackRadius;
			m_CanBeAttacked             = GameConstants.Hound_CanBeAttacked;
			m_CanBeAttackedRadius       = GameConstants.Hound_CanBeAttackedRadius;
			m_TargetDirectionUpdateRate = GameConstants.Hound_TargetDirectionUpdateRate;
			m_TurnMultiplier            = GameConstants.Hound_TurnMultiplier;
			m_MoveSpeed                 = GameConstants.Hound_MoveSpeed;
		}


		protected override void UpdateSubAttributes()
		{
			//Debug.Log ("Called in Player");
		}

		protected override void ApplyMove(Vector3 move)
		{
			if (m_ActionTarget != null && (Vector3.Distance (m_ActionTarget.transform.position, transform.position) <= 2.5f)) {
				base.ApplyMove(move);
			} else {
				Vector3 moveAmount = Vector3.zero;

				move = transform.TransformDirection (Vector3.forward);
				moveAmount = move * Time.deltaTime * m_MoveSpeed;
				m_Rigidbody.MovePosition (m_Rigidbody.position + moveAmount);
			}
		}

		protected override void ActionCallBack(string name)
		{
			if(TargetInRange(m_CanBeAttacked, m_CanBeAttackedRadius, m_ActionPerformedTarget))
			{
				AttackItem item = GameConstants.GetAttackItem("Hound_" + name);
				if(item != null)
				{Debug.Log("88888");
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