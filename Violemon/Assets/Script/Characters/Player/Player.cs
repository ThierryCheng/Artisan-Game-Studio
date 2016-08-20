using UnityEngine;
using UnityEngine.UI;

namespace AGS.Characters
{
	public class Player : BaseCharacter
	{

		protected void ActionCallBack(string name)
		{
			if (name.Equals ("Attack_001") || name.Equals ("Attack_002") || name.Equals ("Attack_003")) 
			{
				if(m_MoveTarget != Vector3.zero || m_ActionTarget != m_ActionPerformedTarget)
				{
					m_Animator.SetTrigger("Interrupt");
					m_BlockMove = false;
					return;
				}
				if(m_ActionTarget != null)
				{
					AttackItem item = new AttackItem();
					item.m_HitPoint = 35;
					m_ActionTarget.SendMessage("Attacked", item);
				}
			}
			if (name.Equals ("Attack_003")) 
			{
				m_BlockMove = false;
				if(m_MoveTarget != Vector3.zero || m_ActionTarget != null)
				{
					m_Animator.SetTrigger("Interrupt");
					m_Animator.SetBool("Run", true);
					return;
				}
			}
			//Debug.Log ("CallBack: " + name);
		}

	}
}
