using UnityEngine;
using System.Collections; 
namespace AGS.Characters
{
	public class MoveBlockerHumanArcher : StateMachineBehaviour {
		
		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
		{
			if (stateInfo.IsName ("Attack") ) {
				animator.gameObject.GetComponent<BaseCharacter>().BlockMove(true);
			}

			/*if (stateInfo.IsName ("Idle") || stateInfo.IsName("Run")) {
				animator.gameObject.GetComponent<BaseCharacter>().BlockMove(false);
			}*/
		}
		
		override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
		{
			if (stateInfo.IsName ("Attack")) {
				animator.gameObject.GetComponent<BaseCharacter>().BlockMove(false);
			}
		}
	}
}