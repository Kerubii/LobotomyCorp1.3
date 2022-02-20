using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp
{
	public class LobotomyGlobalItem : GlobalItem
    {
        public static LobotomyGlobalItem LobItem(Item item)
        {
            return item.GetGlobalItem<LobotomyGlobalItem>();
        }

        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public bool CustomDraw = false;
        public Texture2D CustomTexture = null;

        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context == "bossBag")
            {
                if (arg == ItemID.QueenBeeBossBag && Main.rand.Next(2) == 0)
                    player.QuickSpawnItem(mod.ItemType("Hornet"));
                else if (arg == ItemID.WallOfFleshBossBag && Main.rand.Next(4) == 0)
                    player.QuickSpawnItem(mod.ItemType("Censored"));
            }
            else if (context == "present")
            {
                if (Main.rand.Next(100) == 0)
                    player.QuickSpawnItem(mod.ItemType("Christmas"));
            }
        }
    }
}