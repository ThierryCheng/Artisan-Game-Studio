using UnityEngine;
using System.Collections;

namespace AGS.Characters
{
    public interface BaseAttributeListener{
		void OnHealthChange(float ori, float cur);

		void OnMaxHealthChange(float ori, float cur);

		void OnStaminaChange(float ori, float cur);

		void OnMaxStaminaChange(float ori, float cur);

		void OnBasicPowerChange(float ori, float cur);
	}
}