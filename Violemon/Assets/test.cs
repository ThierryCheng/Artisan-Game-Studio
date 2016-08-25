using UnityEngine;
using System.Collections;
using AGS.Characters;
public class test : StateMachineBehaviour {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		if (stateInfo.IsName ("Attack 001") || stateInfo.IsName ("Attack 002") || stateInfo.IsName ("Attack 003")) {
			animator.gameObject.GetComponent<BaseCharacter>().BlockMove(true);
		}
	}
	
	
	
	
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		if (stateInfo.IsName ("Attack 001") || stateInfo.IsName ("Attack 002") || stateInfo.IsName ("Attack 003")) {
			animator.gameObject.GetComponent<BaseCharacter>().BlockMove(false);
		}
	}
}
