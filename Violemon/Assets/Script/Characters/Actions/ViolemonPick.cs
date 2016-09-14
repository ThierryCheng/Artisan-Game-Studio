using UnityEngine;
using System.Collections;
using AGS.Items;

namespace AGS.Characters
{
	public class ViolemonPick : AGSAction {
		
		private Player        m_Player;
		private GameObject    m_ActionTarget;
		
		public GameObject TargetObj()
		{
			return m_ActionTarget;
		}
		
		public ViolemonPick(Player m_Player, GameObject m_ActionTarget)
		{
			this.m_Player        = m_Player;
			this.m_ActionTarget  = m_ActionTarget;
		}
		
		
		public void StartAction ()
		{
			m_Player.Animator.SetTrigger("Pick");
			m_Player.ClearActionTarget();
			ItemController controller = m_ActionTarget.GetComponent<ItemController> ();
			m_Player.Picked (controller.ItemInfo);
			GameObject.Destroy( m_ActionTarget );
		}
		
		// Update is called once per frame
		public void StopAction ()
		{

		}
		
		public bool CanStartAction ()
		{
			if (m_Player.TargetInRange (m_Player.m_AbleToAttack, m_Player.m_AbleToAttackRadius, m_ActionTarget)) {
				return true;
			} else {
				return false;
			}
		}
		
		public void ActionCallBack (string name)
		{

		}

		public void BeforeChangeTarget()
		{
		}
	}
}