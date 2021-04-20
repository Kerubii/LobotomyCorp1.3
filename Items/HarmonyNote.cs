//css_ref ../tModLoader.dll
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
    public class HarmonyNote : ModItem
    {
        private int frameY = 0;
        private float rotationR = 0f;
        private int decay = 0;

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = 0;
            item.rare = 1;
            item.maxStack = 999;
            frameY = Main.rand.Next(4);
            rotationR = Main.rand.NextFloat(-0.785f, 0.785f);
            item.direction = Main.rand.Next(2) == 0 ? 1 : -1;
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange = 80;
        }

        public override bool ItemSpace(Player player)
        {
            return true;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            decay++;
            if (decay > 90)
            {
                item.TurnToAir();
                item.active = !item.active;
            }
            gravity = 0;
            maxFallSpeed = 16f;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Vector2 position = item.position + item.Size/2 - Main.screenPosition;
            Vector2 origin = new Vector2(11, 10);
            Rectangle frame = new Rectangle(0, 22 * frameY, 22, 20);
            if (decay > 60)
                lightColor *= 1f - (float)(decay - 60) / 30f;
            spriteBatch.Draw(Main.itemTexture[item.type], position, (Rectangle?)frame, lightColor, rotation + rotationR, origin, 1f, item.direction > 0 ? SpriteEffects.None : SpriteEffects.FlipVertically, 0f);
            return false;
        }

        public override bool OnPickup(Player player)
        {
            LobotomyModPlayer.ModPlayer(player).HarmonyTime += 420;
            if (LobotomyModPlayer.ModPlayer(player).HarmonyTime > 600)
                LobotomyModPlayer.ModPlayer(player).HarmonyTime = 600;
            player.AddBuff(mod.BuffType("MusicalAddiction"), LobotomyModPlayer.ModPlayer(player).HarmonyTime, true);
            player.AddBuff(mod.BuffType("Satiated"), 300, true);
            return false;
        }
    }
}
