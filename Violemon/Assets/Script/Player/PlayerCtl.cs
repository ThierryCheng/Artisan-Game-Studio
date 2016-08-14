using System;
using UnityEngine;

namespace AGS.Player
{
	[RequireComponent(typeof (Player))]
	public class PlayerCtl : MonoBehaviour
	{
		private Player m_Player; // A reference to the ThirdPersonCharacter on the object
		private Transform m_Cam;                  // A reference to the main camera in the scenes transform
		private Vector3 m_CamForward;             // The current forward direction of the camera
		private Vector3 m_Move;
		private bool mouseDown;
		private int groundLayerIndex;
		private Animator m_Animator;
		private void Start()
		{
			// get the transform of the main camera
			if (Camera.main != null)
			{
				m_Cam = Camera.main.transform;
			}
			else
			{
				Debug.LogWarning(
					"Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
				// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
			}
			
			// get the third person character ( this should never be null due to require component )
			m_Animator = GetComponent<Animator>();
			m_Player = GetComponent<Player>();
			groundLayerIndex = LayerMask.GetMask ("GroundCollider");
		}
		
		
		private void Update()
		{
			MoveByMouse ();
			Attack ();
		}

		private Vector3 MoveByKeys()
		{
			// read inputs
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			
			
			// calculate move direction to pass to character
			if (m_Cam != null)
			{
				// calculate camera relative direction to move:
				m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
				m_Move = v*m_CamForward + h*m_Cam.right;
			}
			else
			{
				// we use world-relative directions in the case of no main camera
				m_Move = v*Vector3.forward + h*Vector3.right;
			}
			#if !MOBILE_INPUT
			// walk speed multiplier
			if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
			#endif
			return m_Move;
		}

		private void MoveByMouse()
		{
			if (Input.GetMouseButtonDown (1)) {
				mouseDown = true;
				m_Animator.SetBool("Run", true);
			}
			else if(Input.GetMouseButtonUp (1))
			{
				mouseDown = false;
				m_Animator.SetBool("Run", false);
			}
			if(mouseDown){
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hitInfo;
				if (Physics.Raycast (ray, out hitInfo, 1000, groundLayerIndex)) {
					//Debug.Log ("111");
					Vector3 target = hitInfo.point;
					m_Move = target - transform.position;
					m_Move = Vector3.Scale(m_Move, new Vector3(1, 0, 1)).normalized;



				}
			}
		}

		private void Attack()
		{
			int attack = 0;
			if (Input.GetMouseButtonDown (0)) {
				attack = 1;
				m_Animator.SetInteger("Attack", attack);
			}

		}

		// Fixed update is called in sync with physics
		private void FixedUpdate()
		{
			if(mouseDown){
				m_Player.Move (m_Move);
			}

			// pass all parameters to the character control script
			/*if (m_Move != null) {
				m_Player.Move (m_Move);
			}*/
			
		}
	}
}
