using UnityEngine;
using System.Collections;
namespace AGS.Characters
{
	public abstract class NPC : BaseCharacter {	

		public GameObject m_Target;

		protected void Start()
		{
			base.Start ();
			if (m_Target != null) {
				AGSAction action = new NPCNormalAttack(this, m_Target);
				//AGSAction action = new Follow(this, m_Target);
				this.SetActionTarget(action);
			}
		}

		protected override abstract void UpdateSubAttributes ();

		protected override abstract void ActionCallBack(string name);
	}
}