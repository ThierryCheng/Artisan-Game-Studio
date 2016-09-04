using UnityEngine;
using UnityEngine.UI;
using AGS.Config;

namespace AGS.Characters
{
	public class Player : BaseCharacter
	{

		protected void Start()
		{
			base.Start ();
			//m_MoveTarget = new Vector3 (1000, 0, 1000);
			gameObject.tag = "Player";
			//m_HitPoints = 400;
			//gameObject.layer = "Human";
			
		}

		protected void ActionCallBack(string name)
		{
			Debug.Log(name + "  111111");
			if (name.Equals ("Attack 001") || name.Equals ("Attack 002") || name.Equals ("Attack 003")) 
			{
				if(TargetInRange(m_CanBeAttacked, m_ActionPerformedTarget))
				{
					Debug.Log(name + "  111111");
					AttackItem item = GameConstants.GetAttackItem("Violemon_" + name);
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
				}

					


			}


			//Debug.Log ("CallBack: " + name);
		}
	}
}
