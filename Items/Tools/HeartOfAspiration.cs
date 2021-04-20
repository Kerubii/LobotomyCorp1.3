﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace LobotomyCorp.Items.Tools
{
    public class HeartOfAspiration : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("");
            Tooltip.SetDefault("\"Excessive aspiration would bring about unwarranted frenzy.\"\n" +
                               "Increases health, damage, defense, melee speed and movement speed\n" +
							   "Offensive boosts build up over time\n" +
							   "Hitting an enemy reduces Offensive boosts\n" +
							   "When Offensive boosts peak for 10 seconds, defensive boosts deactivate and gives the debuff Heart Attack\n" +
							   "Heart Attack decreases health and life regen, Offensive boosts never dissapears");
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
            player.statLifeMax2 = (int)(player.statLifeMax2 * 1.1f);
            player.moveSpeed += 5;
            player.meleeSpeed += 0.15f;
            //LobotomyModPlayer modPlayer = LobotomyModPlayer.ModPlayer(player);
        }
    }
}