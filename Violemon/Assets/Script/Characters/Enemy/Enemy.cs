using UnityEngine;
using System.Collections;
using AGS.Character;

namespace AGS.Enemy
{
	public class Enemy : BaseCharacter
	{
		protected void Start()
		{
			base.Start ();
			m_MoveTarget = new Vector3 (1000, 0, 1000);
		}

		public override void Attacked(Object para)
		{
			m_BlockMove = true;
			m_Animator.SetTrigger ("Attacked");
			//StartCoroutine
		}


		
		protected void ActionCallBack(string name)
		{
			m_BlockMove = false;
		}
	}
}