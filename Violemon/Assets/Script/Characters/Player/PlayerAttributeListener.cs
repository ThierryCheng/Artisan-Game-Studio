using UnityEngine;
using System.Collections;
using AGS.Items;

namespace AGS.Characters
{
	public interface PlayerAttributeListener:BaseAttributeListener{
		void OnFeededPointChange(float ori, float cur);

		void OnMaxFeededPointChange(float ori, float cur);

		void OnGrowthPointChange(float ori, float cur);

		void OnHatredToHumanChange(float ori, float cur);

		void OnFarmiliarityToHumanLanguageChange(float ori, float cur);

		void OnDeviationChange(float ori, float cur);

		void OnGainedObj(Items.Item item);
	}
}