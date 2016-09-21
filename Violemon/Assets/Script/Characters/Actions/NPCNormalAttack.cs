using UnityEngine;
using System.Collections;
using AGS.Config;
using AGS.Items;

namespace AGS.Characters
{
	public class NPCNormalAttack : AGSAction {
		
		private BaseCharacter m_BaseCharacter;
		private GameObject    m_ActionTarget;

		public GameObject TargetObj()
		{
			return m_ActionTarget;
		}

		public NPCNormalAttack(BaseCharacter m_BaseCharacter, GameObject m_ActionTarget)
		{
			this.m_BaseCharacter = m_BaseCharacter;
			this.m_ActionTarget  = m_ActionTarget;
		}
		
		public void SetActionTarget(GameObject tar)
		{
			m_ActionTarget = tar;
		}

		public void StartAction ()
		{
			m_BaseCharacter.Animator.SetBool("NormalAttack", true);
		}
		
		// Update is called once per frame
		public void StopAction ()
		{
			m_BaseCharacter.Animator.SetBool("NormalAttack", false);
		}
		
		public bool CanStartAction ()
		{
			if (m_ActionTarget == null)
				return false;
			if (m_BaseCharacter.TargetInRange (m_BaseCharacter.m_AbleToAttack, m_BaseCharacter.m_AbleToAttackRadius, m_ActionTarget)) {
				return true;
			} else {
				return false;
			}
		}
		
		public void ActionCallBack (string name)
		{
			if (m_ActionTarget == null)
				return;
			if(m_BaseCharacter.TargetInRange(m_BaseCharacter.m_CanBeAttacked, m_BaseCharacter.m_CanBeAttackedRadius, m_ActionTarget))
			{
				AttackItem item = GameConstants.GetAttackItem("HumanKnight_" + name);
				if(item != null)
				{
					item.KnockBackDirection = m_BaseCharacter.transform.TransformDirection(Vector3.forward);
					BaseCharacter bc = m_ActionTarget.GetComponent<BaseCharacter>();
					bc.Attacked(item);
					if(m_BaseCharacter.IsActionTargetTheSame() && bc.IsDead())
					{
						m_BaseCharacter.Animator.SetBool("NormalAttack", false);
						m_BaseCharacter.ClearActionTarget();
					}
				}
			}
		}

		public void BeforeChangeTarget()
		{
			m_BaseCharacter.Animator.SetBool("NormalAttack", false);
			
		}
	}
}