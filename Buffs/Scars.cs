using Terraria;
using Terraria.ModLoader;

namespace LobotomyCorp.Buffs
{
	public class Scars : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Scars");
			Description.SetDefault("A wound that will never heal");
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
		
		public override void Update(NPC npc, ref int BuffIndex)
		{
            npc.buffTime[BuffIndex] = 10;
			LobotomyGlobalNPC.LNPC(npc).WristCutterScars = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            LobotomyModPlayer.ModPlayer(player).WristCutterScars = true;
            player.buffTime[buffIndex] = 10;
        }
    }
}