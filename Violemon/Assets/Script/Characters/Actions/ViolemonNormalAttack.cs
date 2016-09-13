using UnityEngine;
using System.Collections;
using AGS.Config;
using AGS.Items;

namespace AGS.Characters
{
	public class ViolemonNormalAttack : AGSAction {

		private BaseCharacter m_BaseCharacter;
		private GameObject    m_ActionTarget;

		public GameObject TargetObj()
		{
			return m_ActionTarget;
		}

		public ViolemonNormalAttack(BaseCharacter m_BaseCharacter, GameObject m_ActionTarget)
		{
			this.m_BaseCharacter = m_BaseCharacter;
			this.m_ActionTarget  = m_ActionTarget;
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
			if (m_BaseCharacter.TargetInRange (m_BaseCharacter.m_AbleToAttack, m_BaseCharacter.m_AbleToAttackRadius, m_ActionTarget)) {
				return true;
			} else {
				return false;
			}
		}
		
		public void ActionCallBack (string name)
		{
			if (name.Equals ("Attack 001") || name.Equals ("Attack 002") || name.Equals ("Attack 003")) 
			{
				if(m_BaseCharacter.TargetInRange(m_BaseCharacter.m_CanBeAttacked, m_BaseCharacter.m_CanBeAttackedRadius, m_ActionTarget))
				{
					//Debug.Log(name + "  111111");
					AttackItem item = GameConstants.GetAttackItem("Violemon_" + name);
					if(item != null)
					{
						item.KnockBackDirection = m_BaseCharacter.transform.TransformDirection(Vector3.forward);
						BaseCharacter bc = m_ActionTarget.GetComponent<BaseCharacter>();
						bc.Attacked(item);
						if(m_BaseCharacter.IsActionTargetTheSame() && bc.IsDead())
						{
							m_BaseCharacter.ClearActionTarget();
						}
					}
				}
			}
		}
	}
}