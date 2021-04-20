using Terraria;
using Terraria.ModLoader;

namespace LobotomyCorp.Buffs
{
	public class WillBeBad : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Blue Moon");
			Description.SetDefault("10% increased melee damage");
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeDamage += 0.1f;
        }
    }
}