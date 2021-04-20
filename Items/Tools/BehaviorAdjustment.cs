﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace LobotomyCorp.Items.Tools
{
    public class BehaviorAdjustment : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("");
            Tooltip.SetDefault("\"Eventually, intellect loses all meaning as they forget even how to exist.\"\n" +
                               "18% increased melee speed\n" +
							   "18% increased movement speed\n" +
							   "15% decreased damage reduction\n");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.accessory = true;
            item.value = 1000;
            item.rare = 1;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //player.statLifeMax2 = (int)(player.statLifeMax2 * 1.1f);
            player.moveSpeed += 18;
            player.meleeSpeed += 0.18f;
            //LobotomyModPlayer modPlayer = LobotomyModPlayer.ModPlayer(player);
        }
    }
}