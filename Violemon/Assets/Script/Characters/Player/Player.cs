using UnityEngine;
using AGS.Config;

namespace AGS.Characters
{
	public class Player : BaseCharacter
	{
		public    float           m_GrowthPoint;
		public    float           m_MaxFeededPoint;
		public    float           m_FeededPoint;
		public    int             m_HatredToHuman;
		public    int             m_FarmiliarityToHumanLanguage;
		public    float           m_Deviation;
		protected new void Start()
		{
			base.Start ();
			//m_MoveTarget = new Vector3 (1000, 0, 1000);
			gameObject.tag = TagManager.PLAYER;
			//m_HitPoints = 400;
			//gameObject.layer = "Human";
			m_MaxFeededPoint            = GameConstants.Violemon_InitialMaxFeededPoint;
			m_FeededPoint               = GameConstants.Violemon_InitialMaxFeededPoint;
			m_MaxHealth                 = GameConstants.Violemon_InitialMaxHealth;
			m_Health                    = GameConstants.Violemon_InitialMaxHealth;
			m_MaxStamina                = GameConstants.Violemon_InitialMaxStamina;
			m_Stamina                   = GameConstants.Violemon_InitialMaxStamina;
			m_AbleToAttack              = GameConstants.Violemon_AbleToAttack;
			m_AbleToAttackRadius        = GameConstants.Violemon_AbleToAttackRadius;
			m_CanBeAttacked             = GameConstants.Violemon_CanBeAttacked;
			m_CanBeAttackedRadius       = GameConstants.Violemon_CanBeAttackedRadius;
			m_TargetDirectionUpdateRate = GameConstants.Violemon_TargetDirectionUpdateRate;
			m_TurnMultiplier            = GameConstants.Violemon_TurnMultiplier;
			m_MoveSpeed                 = GameConstants.Violemon_MoveSpeed;
		}

		protected override void UpdateSubAttributes()
		{
			UpdateHealth ();
			UpdateFeededPoint ();
		}

		protected void UpdateFeededPoint()
		{
			if(m_FeededPoint > 0f)
			{
				m_FeededPoint -= m_MaxFeededPoint * GameConstants.Violemon_FeededPointDecreaseRate;
				if(m_FeededPoint < 0f)
				{
					m_FeededPoint = 0f;
				}
			}
		}

		protected override void UpdateStamina()
		{
			if(m_Stamina < m_MaxStamina)
			{
				m_Stamina += (m_FeededPoint / m_MaxFeededPoint) * 3f;
				if(m_Stamina > m_MaxStamina)
				{
					m_Stamina = m_MaxStamina;
				}
			}
		}

		protected void UpdateHealth()
		{
			//float percentage = m_FeededPoint / m_MaxFeededPoint;
			//float percentage = 1f;
			//float va = Mathf.Log (percentage + 1f);
			//float vb = 1.6668f * Mathf.Log (percentage + 1f);
			//Debug.Log ("recover: " + ((-0.1734f * va * va) + vb - 3f));
		}

		protected override void ActionCallBack(string name)
		{
			//Debug.Log(name + "  111111");
			if (name.Equals ("Attack 001") || name.Equals ("Attack 002") || name.Equals ("Attack 003")) 
			{
				if(TargetInRange(m_CanBeAttacked, m_CanBeAttackedRadius, m_ActionPerformedTarget))
				{
					//Debug.Log(name + "  111111");
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
