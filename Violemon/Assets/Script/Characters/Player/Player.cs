using UnityEngine;
using AGS.Config;
using AGS.Items;

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

		protected void ChangeFeededPoint(float value)
		{
			float ori = m_FeededPoint;
			m_FeededPoint += value;
			if (m_FeededPoint > m_MaxFeededPoint) {
				m_FeededPoint = m_MaxFeededPoint;
			}
			FeededPointChanged(ori, m_FeededPoint);
		}

		protected void UpdateFeededPoint()
		{
			if(m_FeededPoint > 0f)
			{
				ChangeFeededPoint(-(m_MaxFeededPoint * GameConstants.Violemon_FeededPointDecreaseRate));

				if(m_FeededPoint < 0f)
				{
					m_FeededPoint = 0f;
				}
			}
		}

		protected void ChangeStamina(float value)
		{
			float ori = m_Stamina;
			m_Stamina += value;
			if (m_Stamina > m_MaxStamina) {
				m_Stamina = m_MaxStamina;
			}
			StaminaChanged(ori, m_Stamina);
		}

		protected override void UpdateStamina()
		{
			if(m_Stamina < m_MaxStamina)
			{
				ChangeStamina((m_FeededPoint / m_MaxFeededPoint) * 3f);
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
			m_ActionPerformedTarget.ActionCallBack (name);
		}

		protected void ChangeMaxFeededPoint(float value)
		{
			float ori = m_MaxFeededPoint;
			m_MaxFeededPoint += value;
			MaxFeededPointChanged(ori, m_MaxFeededPoint);
		}

		protected void FeededPointChanged(float ori, float cur)
		{
			foreach (BaseAttributeListener l in listeners)
			{
				if(l is PlayerAttributeListener)
				{
					((PlayerAttributeListener)l).OnFeededPointChange(ori, cur);
				}
			}
		}

		protected void MaxFeededPointChanged(float ori, float cur)
		{
			foreach (BaseAttributeListener l in listeners)
			{
				if(l is PlayerAttributeListener)
				{
					((PlayerAttributeListener)l).OnMaxFeededPointChange(ori, cur);
				}
			}
		}

		public void Picked(Items.Item item)
		{
			//Debug.Log ("111 " + item);
			ConsumeObj (item as Consumable);
			foreach (BaseAttributeListener l in listeners)
			{
				if(l is PlayerAttributeListener)
				{
					((PlayerAttributeListener)l).OnGainedObj(item);
				}
			}
		}

		public void ConsumeObj(Consumable obj)
		{
			if (obj == null) {
				throw new UnityException("Consumable is null!");
			}
			ChangeHealth         (obj.m_Health);
			ChangeMaxHealth      (obj.m_MaxHealth);
			ChangeFeededPoint    (obj.m_FeededPoint);
			ChangeMaxFeededPoint (obj.m_MaxFeededPoint);
			ChangeStamina        (obj.m_Stamina);
			ChangeMaxStamina     (obj.m_MaxStamina);
		}

		public void Pick(GameObject obj)
		{
			AGSAction action = new ViolemonPick (this, obj);
			this.SetActionTarget (action);
		}

		protected override void OnDie()
		{
			//Debug.Log ("Called in Player");
		}
	}
}
