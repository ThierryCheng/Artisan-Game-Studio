﻿using UnityEngine;
using System.Collections;

namespace AGS.Characters
{
	public interface PlayerAttributeListener:BaseAttributeListener{
		void OnFeededPointChange(float ori, float cur);

		void OnMaxFeededPointChange(float ori, float cur);

		void OnGrowthPointChange(float ori, float cur);

		void OnHatredToHumanChange(float ori, float cur);

		void OnFarmiliarityToHumanLanguageChange(float ori, float cur);

		void OnDeviationChange(float ori, float cur);
	}
}