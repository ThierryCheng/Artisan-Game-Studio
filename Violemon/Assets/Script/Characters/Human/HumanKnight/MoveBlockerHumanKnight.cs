using UnityEngine;
using System.Collections;
namespace AGS.Characters
{
	public class MoveBlockerHumanKnight : StateMachineBehaviour {
		
		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
		{
			if (CheckName(animator, stateInfo, layerIndex)) {
				animator.gameObject.GetComponent<BaseCharacter>().BlockMove(true);
			}
		}

		override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
		{
			if (CheckName(animator, stateInfo, layerIndex)) {
				animator.gameObject.GetComponent<BaseCharacter>().BlockMove(false);
			}
		}
		
		private bool CheckName(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (stateInfo.IsName ("Attack")) {
				return true;
			}
			return false;
		}
	}
}