using UnityEngine;
using UnityEngine.UI;

namespace AGS.Characters
{
	public class Player : BaseCharacter
	{

		protected void Start()
		{
			base.Start ();
			//m_MoveTarget = new Vector3 (1000, 0, 1000);
			gameObject.tag = "Player";
			m_HitPoints = 100;
			//gameObject.layer = "Human";
			
		}

		protected void ActionCallBack(string name)
		{
			if (name.Equals ("Attack_001") || name.Equals ("Attack_002") || name.Equals ("Attack_003")) 
			{

				if(m_ActionTarget != null)
				{
					if(TargetInRange(m_CanBeAttacked))
					{

						AttackItem item = new AttackItem();
						item.Damage = 40;
						item.SlowDown = 3.0f;
						item.Stun = 4.0f;
						BaseCharacter bc = m_ActionTarget.GetComponent<BaseCharacter>();
						bc.Attacked(item);
						if(bc.IsDead())
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
