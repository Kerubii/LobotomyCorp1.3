﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace LobotomyCorp.Items.Tools
{
    public class LuminousBracelet : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("");
            Tooltip.SetDefault("\"This bracelet shall not forgive those who hold greed in their hearts\nThus it must only be worn by those in true need.\"n" +
                               "Increases health and life regeneration\n" +
                               "Wearing it having no injuries slowly increases maximum health" +
							   "When maximum health exceeds 150%, the owner dies from overregeneration");
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
            player.statLifeMax2 += 50;
            player.lifeRegen += 5;
            LobotomyModPlayer modPlayer = LobotomyModPlayer.ModPlayer(player);
            if (modPlayer.LuminousGreed > 0 && player.statLife < player.statLifeMax)
            {
                modPlayer.LuminousGreed--;
            }
            else
            {
                modPlayer.LuminousGreed += 2;
            }
			/*bool Healthy = player.statLife == player.statLifeMax2;
			player.statLifeMax2 += (int)(player.statLifeMax2 + (player.statLifeMax2 / 2 * (modPlayer.LuminousGreed/1200))
			if (Healthy)
			{
				player.statLife = player.statLifeMax2;
			}*/
        }
    }
}