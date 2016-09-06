﻿using UnityEngine;
using System.Collections;
namespace AGS.Characters
{
	public abstract class NPC : BaseCharacter {	
		protected override abstract void UpdateSubAttributes ();

		protected override abstract void ActionCallBack(string name);
	}
}