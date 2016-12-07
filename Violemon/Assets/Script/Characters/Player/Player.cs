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

		protected override void FollowTarget()
		{
			if (m_MoveTarget != Vector3.zero) {
				m_Animator.SetBool("HasTarget", false);
				Vector3 mf = new Vector3 (transform.position.x, 0, transform.position.z);
				Vector3 tf = new Vector3 (m_MoveTarget.x, 0, m_MoveTarget.z);
				if (Vector3.Distance (mf, tf) > 0.2f) {
					m_MoveDirection = m_MoveTarget - transform.position;
					m_MoveDirection = Vector3.Scale (m_MoveDirection, m_VectorMask).normalized;

					m_Animator.SetBool ("Run", true);

				} else {
					m_MoveTarget = Vector3.zero;
					m_Animator.SetBool ("Run", false);
				}
			}
			else if(m_ActionTarget != null)
			{
				BaseCharacter baseCharacter = m_ActionTarget.TargetObj().GetComponent<BaseCharacter>();
				if(baseCharacter != null && baseCharacter.IsDead())
				{
					m_ActionTarget = null;
					m_Animator.SetBool("HasTarget", false);
				}
				else
				{
					m_Animator.SetBool("HasTarget", true);
					UpdateDirection();
					if(!m_ActionTarget.CanStartAction())
					{

						m_Animator.SetBool ("Run", true);
						m_ActionTarget.StopAction();
					}
					else
					{
						m_ActionTarget.StartAction();
						m_Animator.SetBool ("Run", false);
						m_ActionPerformedTarget = m_ActionTarget;

					}
				}
			}
			else 
			{
				m_Animator.SetBool("HasTarget", false);
				m_Animator.SetBool("Run", false);
			}

			if(m_Animator.GetBool("Run") == true && !IsDead() && !m_BlockMove)
			{
				Move (m_MoveDirection);
			}
		}

		private void UpdateDirection()
		{
			if(m_TargetDirectionUpdateRate <= 0f)
			{
				m_MoveDirection = m_ActionTarget.TargetObj().transform.position - transform.position;
				m_MoveDirection = Vector3.Scale (m_MoveDirection, m_VectorMask).normalized;
			}
			else if((Time.time - m_LastDirectionUpdateTime) >= m_TargetDirectionUpdateRate)
			{
				m_MoveDirection = m_ActionTarget.TargetObj().transform.position - transform.position;
				m_MoveDirection = Vector3.Scale (m_MoveDirection, m_VectorMask).normalized;
				m_LastDirectionUpdateTime = Time.time;
			}
		}
	}
}
