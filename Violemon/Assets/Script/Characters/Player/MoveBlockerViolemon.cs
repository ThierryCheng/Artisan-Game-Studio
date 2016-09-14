using UnityEngine;
using System.Collections;

namespace AGS.Characters
{
	public class MoveBlockerViolemon : StateMachineBehaviour {
		
		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
		{
			if (stateInfo.IsName ("Attack 001")) {
				//Debug.Log("block");
				animator.gameObject.GetComponent<BaseCharacter>().BlockMove(true);
			}
		}

		override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
		{
			if (stateInfo.IsName ("Attack 001") || stateInfo.IsName ("Attack 002")) {
				if(!(animator.GetBool("HasTarget") && animator.GetBool("NormalAttack")))
				{
					animator.gameObject.GetComponent<BaseCharacter>().BlockMove(false);
				}
			}
			else if(stateInfo.IsName ("Attack 003"))
			{
				animator.gameObject.GetComponent<BaseCharacter>().BlockMove(false);
			}
		}
	}
}