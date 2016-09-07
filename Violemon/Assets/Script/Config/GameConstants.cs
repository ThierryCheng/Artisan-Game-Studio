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

		public static float Violemon_FeededPointDecreaseRate = 0.001f;
		public static float Violemon_InitialMaxFeededPoint = 100f;
		public static float Violemon_InitialMaxHealth = 200f;
		public static float Violemon_InitialMaxStamina = 100f;
		public static float Violemon_AbleToAttack = 1.0f;
		public static float Violemon_AbleToAttackRadius = 0.2f;
		public static float Violemon_CanBeAttacked = 2.4f;
		public static float Violemon_CanBeAttackedRadius = 0.7f;
		public static float Violemon_TurnMultiplier = 1f;
		public static float Violemon_TargetDirectionUpdateRate = 0f;
		public static float Violemon_MoveSpeed = 6f;

		public static float Hound_InitialMaxHealth = 200f;
		public static float Hound_InitialMaxStamina = 100f;
		public static float Hound_AbleToAttack = 2.2f;
		public static float Hound_AbleToAttackRadius = 0.2f;
		public static float Hound_CanBeAttacked = 1.8f;
		public static float Hound_CanBeAttackedRadius = 0.6f;
		public static float Hound_TurnMultiplier = 0.8f;
		public static float Hound_TargetDirectionUpdateRate = 0.5f;
		public static float Hound_MoveSpeed = 5f;

		public static float HumanKnight_InitialMaxHealth = 200f;
		public static float HumanKnight_InitialMaxStamina = 100f;
		public static float HumanKnight_AbleToAttack = 1f;
		public static float HumanKnight_AbleToAttackRadius = 0.2f;
		public static float HumanKnight_CanBeAttacked = 2.0f;
		public static float HumanKnight_CanBeAttackedRadius = 0.6f;
		public static float HumanKnight_TurnMultiplier = 1f;
		public static float HumanKnight_TargetDirectionUpdateRate = 0f;
		public static float HumanKnight_MoveSpeed = 4f;


		private static AttackItem Violemon_Attack_001 = new AttackItem (20, 0f, 0f, 0f);
		private static AttackItem Violemon_Attack_002 = new AttackItem (25, 0f, 0f, 0f);
		private static AttackItem Violemon_Attack_003 = new AttackItem (30, 0f, 7f, 0f);
		private static AttackItem HumanKnight_Attack  = new AttackItem (25, 0f, 0f, 0f);
		private static AttackItem Hound_Attack  = new AttackItem (25, 0f, 0f, 0f);
		private static Dictionary<string,AttackItem>  AttackMap = new Dictionary<string,AttackItem> ();
		static GameConstants()
		{
			AttackMap.Add ("Violemon_Attack 001", Violemon_Attack_001);
			AttackMap.Add ("Violemon_Attack 002", Violemon_Attack_002);
			AttackMap.Add ("Violemon_Attack 003", Violemon_Attack_003);
			AttackMap.Add ("HumanKnight_Attack", HumanKnight_Attack);
			AttackMap.Add ("Hound_Attack 01", Hound_Attack);
		}

		public static AttackItem GetAttackItem(string name)
		{
			return AttackMap[name];
		}
    }
}