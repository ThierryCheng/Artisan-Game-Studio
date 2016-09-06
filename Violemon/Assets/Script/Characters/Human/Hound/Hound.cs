﻿using UnityEngine;
using System.Collections;

namespace AGS.Characters
{
	public class Hound : NPC {

		protected void Start()
		{
			base.Start ();
			//gameObject.tag = "Hound";
		}


		protected override void UpdateSubAttributes()
		{
			//Debug.Log ("Called in Player");
		}

		protected override void ActionCallBack(string name)
		{
		}


	}
}