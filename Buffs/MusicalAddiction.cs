using Terraria;
using Terraria.ModLoader;

namespace LobotomyCorp.Buffs
{
	public class MusicalAddiction : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Musical Addiction");
			Description.SetDefault("I need to listen to that song at any cost");
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage += 0.12f;
            player.endurance -= 0.2f;
            LobotomyModPlayer.ModPlayer(player).HarmonyAddiction = true;
        }
    }
}