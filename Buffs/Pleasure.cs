using System;
using Terraria;
using Terraria.ModLoader;

namespace LobotomyCorp.Buffs
{
	public class Pleasure : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Pleasure");
			Description.SetDefault("Pleasant feelings fills your head");
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
		
		public override void Update(NPC npc, ref int BuffIndex)
		{
			npc.loveStruck = true;
			LobotomyGlobalNPC.LNPC(npc).PleasureDebuff = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.loveStruck = true;
            LobotomyModPlayer.ModPlayer(player).PleasureDebuff = true;
        }
    }
}