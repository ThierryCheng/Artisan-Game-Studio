using UnityEngine;
using System.Collections;
using AGS.Config;
using AGS.Items;

namespace AGS.Characters
{
	public class HumanArcher : Human {

		protected Transform m_Arrow;
		protected GameObject m_OriArrow;

		protected void Start()
		{
			base.Start ();
			//gameObject.tag = "Hound";
			m_MaxHealth                 = GameConstants.HumanArcher_InitialMaxHealth;
			m_Health                    = GameConstants.HumanArcher_InitialMaxHealth;
			m_MaxStamina                = GameConstants.HumanArcher_InitialMaxStamina;
			m_Stamina                   = GameConstants.HumanArcher_InitialMaxStamina;
			m_AbleToAttack              = GameConstants.HumanArcher_AbleToAttack;
			m_AbleToAttackRadius        = GameConstants.HumanArcher_AbleToAttackRadius;
			m_CanBeAttacked             = GameConstants.HumanArcher_CanBeAttacked;
			m_CanBeAttackedRadius       = GameConstants.HumanArcher_CanBeAttackedRadius;
			m_TargetDirectionUpdateRate = GameConstants.HumanArcher_TargetDirectionUpdateRate;
			m_TurnMultiplier            = GameConstants.HumanArcher_TurnMultiplier;
			m_MoveSpeed                 = GameConstants.HumanArcher_MoveSpeed;

			m_OriArrow = (GameObject)Resources.Load ("Test/AllStarCharacterLibrary/AmazingAmazons/Arrow");

			foreach (Transform t in transform.GetComponentsInChildren<Transform> ()) {
			    if(t.name.Equals("Arrow"))
				{
					m_Arrow = t;
					break;
				}
			}
		}
		
		protected override void ActionCallBack(string name)
		{
			/*if(TargetInRange(m_CanBeAttacked, m_CanBeAttackedRadius, m_ActionPerformedTarget))
			{
				AttackItem item = GameConstants.GetAttackItem("HumanKnight_" + name);
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
			}*/
			//m_ActionPerformedTarget.ActionCallBack (name);
			GameObject obj = (MonoBehaviour.Instantiate (m_OriArrow, m_Arrow.position, m_Arrow.rotation) as GameObject);
			Vector3 targetObjPosition = m_ActionPerformedTarget.TargetObj ().transform.position;
			Vector3 target = new Vector3 (targetObjPosition.x, m_Arrow.position.y, targetObjPosition.z);
			obj.transform.LookAt (target);
			obj.AddComponent<FlyItem> ();
		}
		
		protected override void OnDie()
		{
			ItemManager.Instance ().GenerateItem (ItemIDs.Item_Apple, transform.position);
			ItemManager.Instance ().GenerateItem (ItemIDs.Item_Apple, transform.position);
		}
	}
}