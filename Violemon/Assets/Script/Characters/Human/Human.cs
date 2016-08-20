using UnityEngine;
using System.Collections;

namespace AGS.Characters
{
	public class Human : BaseCharacter
	{
		protected void Start()
		{
			base.Start ();
			m_MoveTarget = new Vector3 (1000, 0, 1000);
			gameObject.tag = "Human";
			//gameObject.layer = "Human";
		}

		protected void ActionCallBack(string name)
		{
			m_BlockMove = false;
		}
	}
}