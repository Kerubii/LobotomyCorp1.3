using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
    }
}