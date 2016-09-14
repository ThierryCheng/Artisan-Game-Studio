using UnityEngine;
using System.Collections;

namespace AGS.Characters
{
	public interface AGSAction{

		GameObject TargetObj();
		// Use this for initialization
		void StartAction ();
		
		// Update is called once per frame
		void StopAction ();

		bool CanStartAction ();

		void ActionCallBack (string name);

		void BeforeChangeTarget();
	}
}