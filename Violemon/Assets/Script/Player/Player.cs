using UnityEngine;
using UnityEngine.UI;

namespace AGS.Player
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class Player : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_GroundCheckDistance = 0.3f;
		
		Rigidbody m_Rigidbody;
		//Animator m_Animator;
		const float k_Half = 0.5f;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;
		bool m_Crouching;
		//Text m_text;
		
		void Start()
		{
			//m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;
			//m_text = GameObject.Find ("Text").GetComponent<Text>();
			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}
		
		
		public void Move(Vector3 move)
		{
			
			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			//Vector3 ori_move = move;
			Vector3 ori_move = move;
			if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
			//m_text.text = "move: " + ori_move + " local move: " + move;
			CheckGroundStatus ();
			move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;
			//Debug.Log ("m_ForwardAmount: " + m_ForwardAmount);
			ApplyExtraTurnRotation();
			ApplyMove (ori_move);
			// control and velocity handling is different when grounded and airborne:
			//HandleGroundedMovement(move);
			
			
			//ScaleCapsuleForCrouching(crouch);
			//PreventStandingInLowHeadroom();
			
			// send input and other state parameters to the animator
			//UpdateAnimator(move);
		}
		
		/*
        public void RotateTo()
        {
            float current = transform.eulerAngles.y;
            this.transform.LookAt (m_currentNode.transform);
            Vector3 target = this.transform.eulerAngles;
            float next = Mathf.MoveTowardsAngle(current, target.y, 120 * Time.deltaTime);
            this.transform.eulerAngles = new Vector3(0, next, 0);
        }

        public void MoveTo()
        {
            Vector3 pos1 = transform.position;
            Vector3 pos2 = m_currentNode.transform.position;
            float dist = Vector2.Distance(new Vector2(pos1.x, pos1.z), new Vector2(pos2.x, pos2.z));
            if(dist < 1.0f)
            {
                if(m_currentNode.m_next == null){
                    Destroy(this.gameObject);
                }
                else
                {
                    m_currentNode = m_currentNode.m_next;
                }
            }
            transform.Translate (new Vector3(0, 0, speed * Time.deltaTime));
        }
		*/
		
		/*void UpdateAnimator(Vector3 move)
		{
			//m_Animator.SetFloat("Run", tru, 0.1f, Time.deltaTime);
			if (m_ForwardAmount > 0.0f) 
			{
				//Debug.Log("Move");
				m_Animator.SetBool("Run", true);
			}
			else
			{
				//Debug.Log("Stop");
				m_Animator.SetBool("Run", false);
			}
			
		}*/
		
		/*void HandleGroundedMovement(Vector3 move)
		{
			Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
			
			// we preserve the existing y part of the current velocity.
			v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v;
		}*/

		void ApplyMove(Vector3 move)
		{
			m_Rigidbody.MovePosition (transform.position + new Vector3 (move.x, 0, move.z) * Time.deltaTime * m_MoveSpeedMultiplier);
			
		}

		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}
		
		/*public void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			if (Time.deltaTime > 0)
			{
				Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
				
				// we preserve the existing y part of the current velocity.
				v.y = m_Rigidbody.velocity.y;
				m_Rigidbody.velocity = v;
			}
		}*/
		
		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
			#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
			#endif
			// 0.1f is a small offset to start the ray from inside the character
			// it is also good to note that the transform position in the sample assets is at the base of the character
			if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
			{
				m_GroundNormal = hitInfo.normal;
				//m_Animator.applyRootMotion = true;
			}
			else
			{
				m_GroundNormal = Vector3.up;
				//m_Animator.applyRootMotion = false;
			}
		}
	}
}
