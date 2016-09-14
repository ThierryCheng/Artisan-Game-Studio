using UnityEngine;
using System.Collections;

namespace AGS.Items
{
	public class Consumable : Item{
		//protected    float           m_GrowthPoint;
		public    float           m_MaxFeededPoint = 0f;
		public    float           m_FeededPoint = 0f;
		public    int             m_HatredToHuman = 0;
		public    int             m_FarmiliarityToHumanLanguage = 0;
		public    float           m_Deviation = 0f;
		public    float           m_MaxHealth = 0f;
		public    float           m_Health = 0f;
		public    float           m_MaxStamina = 0f;
		public    float           m_Stamina = 0f;
		//protected    float           m_BasicPower;
		//protected    float           m_BasicAttackDistance;
		//protected    float           m_BasicAttackSpeed;
		//protected    float           m_MoveSpeed;

		public Item Clone()
		{
			Consumable ins = new Consumable ();
			ins.m_ItemID = this.m_ItemID;
			ins.m_ItemCount = this.m_ItemCount;
			ins.m_MaxFeededPoint = this.m_MaxFeededPoint;
			ins.m_FeededPoint = this.m_FeededPoint;
			ins.m_HatredToHuman = this.m_HatredToHuman;
			ins.m_FarmiliarityToHumanLanguage = this.m_FarmiliarityToHumanLanguage;
			ins.m_Deviation = this.m_Deviation;
			ins.m_MaxHealth = this.m_MaxHealth;
			ins.m_Health = this.m_Health;
			ins.m_MaxStamina = this.m_MaxStamina;
			ins.m_Stamina = this.m_Stamina;
			return null;
		}
	}
}
