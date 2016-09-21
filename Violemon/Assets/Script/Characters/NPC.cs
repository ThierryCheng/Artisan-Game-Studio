using UnityEngine;
using System.Collections;
namespace AGS.Characters
{
	public abstract class NPC : BaseCharacter {	

		public GameObject m_Target;

		private AGSAction m_NPCNormalAttack;

		protected void Start()
		{
			base.Start ();
		}

		public NPC()
		{
			m_NPCNormalAttack = new NPCNormalAttack(this, null);
		}

		public void SetTarget(GameObject m_Target)
		{
			this.m_Target = m_Target;
			if (m_Target != null) {
				m_NPCNormalAttack.SetActionTarget(m_Target);
				this.SetActionTarget(m_NPCNormalAttack);
			}
		}

		protected override abstract void UpdateSubAttributes ();

		protected override abstract void ActionCallBack(string name);
	}
}