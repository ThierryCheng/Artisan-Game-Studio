using UnityEngine;
using System.Collections;
namespace AGS.Characters
{
	[RequireComponent(typeof (NavMeshAgent))]
	public abstract class NPC : BaseCharacter {	

		public    GameObject m_Target;
		protected NavMeshAgent    m_Agent;
		private   AGSAction m_NPCNormalAttack;

		protected void Start()
		{
			base.Start ();
			m_Agent = GetComponentInChildren<NavMeshAgent>();
			m_Agent.enabled = true;
			m_Agent.updateRotation = true;
			m_Agent.updatePosition = true;
			m_Agent.acceleration = 40f;
			m_Agent.angularSpeed = 240f;
			m_Agent.stoppingDistance = 0.2f;

			//SetTarget(GameObject.Find ("Violemon"));
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

		protected override void FollowTarget()
		{
			if (this.IsDead ()) {
				return;
			}
			if (m_MoveTarget != Vector3.zero) {
				m_Animator.SetBool("HasTarget", false);
				Vector3 mf = new Vector3 (transform.position.x, 0, transform.position.z);
				Vector3 tf = new Vector3 (m_MoveTarget.x, 0, m_MoveTarget.z);
				if (Vector3.Distance (mf, tf) > 0.2f) {
					if (m_Agent.isOnNavMesh) {
						m_Agent.SetDestination (m_MoveTarget);
					}

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
					//UpdateDirection();
					if (!m_BlockMove && m_Agent.isOnNavMesh) {
						m_Agent.Resume ();
						m_Agent.SetDestination (m_ActionTarget.TargetObj ().transform.position);
					}
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
						m_Agent.Stop();
					}
				}
			}
			else 
			{
				m_Animator.SetBool("HasTarget", false);
				m_Animator.SetBool("Run", false);
			}
		}

		protected override void OnDie()
		{
			base.OnDie ();
			m_Agent.Stop ();
			//m_Agent.enabled = false;
			this.BlockMove (true);
		}

		/*private void UpdateDirection()
		{
			if(m_TargetDirectionUpdateRate <= 0f)
			{
				m_Agent.SetDestination (m_ActionTarget.TargetObj().transform.position);
            }
			else if((Time.time - m_LastDirectionUpdateTime) >= m_TargetDirectionUpdateRate)
			{
				m_Agent.SetDestination (m_ActionTarget.TargetObj().transform.position);

				m_LastDirectionUpdateTime = Time.time;
			}
		}*/
	}
}