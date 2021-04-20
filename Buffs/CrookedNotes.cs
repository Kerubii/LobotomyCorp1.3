using Terraria;
using Terraria.ModLoader;

namespace LobotomyCorp.Buffs
{
	public class CrookedNotes : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Crooked Notes");
			Description.SetDefault("I was born just for listening to this music");
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage += 0.08f;
            LobotomyModPlayer.ModPlayer(player).HarmonyConnected = true;
        }
    }
}