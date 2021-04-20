using Terraria;
using Terraria.ModLoader;

namespace LobotomyCorp.Buffs
{
	public class Gluttony : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Predation");
			Description.SetDefault("The hunger is too much");
		}

        public override void Update(Player player, ref int buffIndex)
        {
            LobotomyModPlayer.ModPlayer(player).WingbeatGluttony = true;
        }
    }
}