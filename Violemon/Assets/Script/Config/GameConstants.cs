using UnityEngine;
using System.Collections.Generic;
namespace AGS.Config
{
    public class GameConstants{
		public static float MaxStunTime = 1.0f;
		public static float MaxStunPower = 10.0f;
		public static float MaxKnockBackSpeed = 40.0f;
		public static float KnockBackSpeedDecreaseRate = 4.0f;
		public static float MaxKnockBackPower = 10.0f;
		public static float MaxSlowDownPercentage = 0.5f;
		public static float MaxSlowDownPower = 10.0f;
		private static AttackItem Violemon_Attack_001 = new AttackItem (20, 0f, 0f, 0f);
		private static AttackItem Violemon_Attack_002 = new AttackItem (25, 0f, 0f, 0f);
		private static AttackItem Violemon_Attack_003 = new AttackItem (30, 0f, 7f, 0f);
		private static AttackItem HumanKnight_Attack  = new AttackItem (25, 0f, 0f, 0f);
		private static Dictionary<string,AttackItem>  AttackMap = new Dictionary<string,AttackItem> ();
		static GameConstants()
		{
			AttackMap.Add ("Violemon_Attack 001", Violemon_Attack_001);
			AttackMap.Add ("Violemon_Attack 002", Violemon_Attack_002);
			AttackMap.Add ("Violemon_Attack 003", Violemon_Attack_003);
			AttackMap.Add ("HumanKnight_Attack", HumanKnight_Attack);
		}

		public static AttackItem GetAttackItem(string name)
		{
			return AttackMap[name];
		}
    }
}