using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace LobotomyCorp
{
    public class LobotomyModPlayer : ModPlayer
    {
        public int SynchronizedEGO = -1;
        public bool Desync = false;

        public int HeavyWeaponHelper = 0;

        public int BeakParry = 0;
        public int BeakPunish = 0;

        public float FaintAromaPetal = 0;
        public int FaintAromaPetalMax = 60;
        public float FaintAromaDecay = 0.1f;

        public int HarmonyTime = 0;
        public bool HarmonyAddiction = false;
        public bool HarmonyConnected = false;

        public bool giftFourthMatchFlame = false;
        public int FourthMatchFlameR = 0;

        public int GrinderMk2Order = 0;
        public static int GrinderMk2BatteryMax = 5600;
        public int GrinderMk2Battery = GrinderMk2BatteryMax;
        public bool GrinderMk2Recharging = false;

        public int LoveAndHateHysteria = 0;

        public int LuminousGreed = 0;

        public bool SolemnSwitch = false;
        public int SolemnCooldown = 0;

        public int RealizedSword = 0;
        public bool RealizedSwordShoot = false;

        public bool WristCutterScars = false;

        public int RealizedWingbeatMeal = -1;
        public bool WingbeatFairyMeal = false;
        public bool WingbeatGluttony = false;

        public static LobotomyModPlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<LobotomyModPlayer>();
        }

        public override void ResetEffects()
        {
            Desync = false;

            if (HeavyWeaponHelper > 0)
                HeavyWeaponHelper--;

            if (BeakPunish > 0)
                BeakPunish--;

            if (FaintAromaPetal > 0)
            {
                FaintAromaPetal -= FaintAromaDecay;
            }

            giftFourthMatchFlame = false;

            RealizedSwordShoot = false;
            if (RealizedWingbeatMeal >= 0 && (!Main.npc[RealizedWingbeatMeal].active || Main.npc[RealizedWingbeatMeal].life <= 0))
                RealizedWingbeatMeal = -1;
            WingbeatFairyMeal = false;
            WingbeatGluttony = false;

            if (HarmonyTime > 0)
                HarmonyTime--;
            HarmonyAddiction = false;
            HarmonyConnected = false;

            WristCutterScars = false;
        }

        public override void UpdateDead()
        {
            SynchronizedEGO = -1;
            Desync = false;

            HarmonyTime = 0;
            HarmonyAddiction = false;
            LuminousGreed = 0;
            FaintAromaPetal = 0;
        }

        public override void PreUpdate()
        {
            if (BeakParry > 0)
            {
                BeakParry--;
            }

            if (GrinderMk2Recharging)
            {
                if (GrinderMk2Battery < 0)
                    GrinderMk2Battery = 0;
                GrinderMk2Battery += 32;
                if (GrinderMk2Battery > GrinderMk2BatteryMax)
                {
                    GrinderMk2Battery = GrinderMk2BatteryMax;
                    GrinderMk2Recharging = !GrinderMk2Recharging;
                }
            }

            if (RealizedWingbeatMeal > -1)
            {
                Main.npc[RealizedWingbeatMeal].GetGlobalNPC<LobotomyGlobalNPC>().WingbeatTarget = player.whoAmI;
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (WingbeatGluttony)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen = -20;
            }

            if (HarmonyAddiction && !HarmonyConnected)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen = -12;
            }

            if (WristCutterScars)
            {
                player.lifeRegen = 0;
            }
        }

        public override void UpdateLifeRegen()
        {
            if (WingbeatFairyMeal)
            {
                player.lifeRegen += 10;
            }
            if (FaintAromaPetal > 0 && player.lifeRegen < 0)
                player.lifeRegen = 0;
        }

        public override bool PreItemCheck()
        {
            if (SynchronizedEGO >= 0 && player.HeldItem.type != SynchronizedEGO)
            {
                SynchronizedEGO = -1;
            }
            return base.PreItemCheck();
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (player.HeldItem.type == mod.ItemType("RealizedBeak"))
            {
                if (BeakParry > 0)
                {
                    damage /= 2;
                    BeakParry = 0;
                }
                if (BeakPunish > 0)
                    damage /= 2;
                BeakPunish = 180;
                LobotomyGlobalNPC.LNPC(npc).BeakTarget = 180;
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (FaintAromaPetal > 0)
            {
                if (FaintAromaPetal < FaintAromaPetalMax)
                    damage = (int)(damage * (1.1f + ((float)FaintAromaPetal / (float)FaintAromaPetalMax)));
                else
                    damage = (int)(damage * 1.2f);
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (player.whoAmI == Main.myPlayer && LobotomyCorp.PassiveShow.JustPressed)
            {
                LobotomyCorp.ExtraPassiveShow = !LobotomyCorp.ExtraPassiveShow;
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (BeakParry > 0 || BeakPunish > 0)
            {
                /*Color color = Color.Red;
                Color drawColor = new Color(r, g, b, a);
                float BeakRed = BeakParry > BeakPunish ? BeakParry : BeakPunish;
                if (BeakRed <= 10)
                {
                    drawColor *= ((10f - (float)BeakRed) / 10f);
                    color *= ((float)BeakRed / 10f);
                    color.R += drawColor.R;
                    color.G += drawColor.G;
                    color.B += drawColor.B;
                    color.A += drawColor.A;
                }
                r = color.R;
                g = color.G;
                b = color.B;
                a = 255;*/
                fullBright = true;
                if (Main.netMode != NetmodeID.Server)
                {

                }
            }
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            if (FaintAromaPetal > 0 && player.HeldItem.type == mod.ItemType("FaintAromaS") && player.itemAnimation > 0 && npc.immune[player.whoAmI] > 0)
                return false;
            return base.CanBeHitByNPC(npc, ref cooldownSlot);
        }

        public void FourthMatchExplode()
        {
            if (giftFourthMatchFlame)
                return;
            player.AddBuff(BuffID.OnFire, 120);
            if (Main.rand.Next(100) == 0 && (player.statLife == player.statLifeMax2 || (float)(player.statLife / (float)player.statLifeMax2) < 0.25f))
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("FourthMatchFlameExplosion"), 500, 10f, player.whoAmI);
                player.statLife = 0;
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " was reduced to ashes..."), 4000, 1);
            }
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (damageSource.SourceProjectileType == mod.ProjectileType("DespairSword"))
            {
                if (Main.rand.Next(2) == 0)
                    damageSource.SourceCustomReason = player.name + " fell into Despair.";
                else
                    damageSource.SourceCustomReason = player.name + " was stricken with Grief.";
            }
            if (damageSource.SourceProjectileType == mod.ProjectileType("FourthMatchFlameExplosion"))
            {
                damageSource.SourceCustomReason = player.name + " was reduced to ashes...";
            }
            if (WingbeatGluttony && damageSource.SourceOtherIndex == 8)
            {
                damageSource.SourceCustomReason = player.name + " was starved of its prey";
            }
            if (LuminousGreed > 1200 && damageSource.SourceOtherIndex == 8)
            {
                damageSource.SourceCustomReason = player.name + "'s body turned into lumps of overgrown meat";
            }
            //could not finish their performance
            //"Goodbye"
            //could not satiate their addiction
            //'s head bursted from pleasure
            //tried to forcefully escape their established role
            //'s malice overtook their body
            //'s role as a villain has ended
            //thirst went unquenched
            //was judged to be sinful
            //disrespected the fairy's care
            //'s unbearable loneliness crushed them
            //averted their gaze 
            base.Kill(damage, hitDirection, pvp, damageSource);
        }

        public static float Lerp(float x, float x2, float progress, bool reverse = false)
        {
            if (progress < 0)
                progress = 0;
            if (progress > 1f)
                progress = 1f;
            if (reverse)
                return x2 * (1 - progress) + x * progress;
            else
                return x * (1 - progress) + x2 * progress;
        }

        public int shakeTimer = 0;
        public int shakeIntensity = 0;

        public override void ModifyScreenPosition()
        {
            if (shakeTimer > 0)
            {
                shakeTimer--;

                Main.screenPosition += new Vector2(shakeIntensity * Main.rand.NextFloat(), shakeIntensity * Main.rand.NextFloat());

                if (shakeTimer % 5 == 0 && shakeIntensity > 1)
                    shakeIntensity--;
            }
            else
                shakeIntensity = 0;
        }

        //Custom Weapon Drawing
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            MiscEffectsFront.visible = true;
            layers.Insert(24, MiscEffectsFront);
            Action<PlayerDrawInfo> layerTarget = s => DrawLobWeaponFront(s);
            PlayerLayer layer = new PlayerLayer("LobotomyCorp", "CustomeWeaponDrawFront", layerTarget);
            layers.Insert(layers.IndexOf(layers.FirstOrDefault(n => n.Name == "Arms")), layer);
            layerTarget = s => DrawLobWeaponBack(s);
            layer = new PlayerLayer("LobotomyCorp", "CustomeWeaponDrawBack", layerTarget);
            layers.Insert(layers.IndexOf(layers.FirstOrDefault(n => n.Name == "MiscEffectsBack")), layer);
        }

        private void DrawLobWeaponFront(PlayerDrawInfo info)
        {
            Player player = info.drawPlayer;
            if (!player.HeldItem.IsAir && player.itemAnimation != 0 && player.HeldItem.type == mod.ItemType("SolemnLamentS"))
            {
                LobotomyGlobalItem item = player.HeldItem.GetGlobalItem<LobotomyGlobalItem>();

                if (!item.CustomDraw)
                    return;

                Texture2D texture = mod.GetTexture("Items/SolemnLamentS2");

                Color color = Lighting.GetColor((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f));

                Vector2 position = info.itemLocation - Main.screenPosition;
                Vector2 origin = new Vector2(player.direction == 1 ? 0 : texture.Width, texture.Height);
                float rot = player.itemRotation;

                if (player.HeldItem.useStyle == 5)
                {
                    Vector2 textureCenter = new Vector2((float)(texture.Width / 2f), (float)(texture.Height / 2f));

                    float num = 10f;
                    Vector2 result = textureCenter;
                    result.X = num;
                    ItemLoader.HoldoutOffset(player.gravDir, player.HeldItem.type, ref result);


                    Vector2 playerItemPos = result;

                    int x = (int)playerItemPos.X;
                    textureCenter.Y = playerItemPos.Y;
                    origin = new Vector2(-x, texture.Height / 2);
                    if (player.direction == -1)
                    {
                        origin = new Vector2(texture.Width + x, texture.Height / 2);
                    }
                    position.X += textureCenter.X + (player.GetModPlayer<LobotomyModPlayer>().SolemnSwitch ? -6 : 0) * player.direction; ;
                    position.Y += textureCenter.Y;

                    if (player.GetModPlayer<LobotomyModPlayer>().SolemnSwitch)
                    {
                        rot -= MathHelper.ToRadians(30 + 75 * (1 - (float)player.itemAnimation / (float)player.itemAnimationMax)) * player.direction;
                    }
                }

                Main.playerDrawData.Add(
                    new DrawData(
                        texture, //pass our glowmask's texture
                        position, //pass the position we should be drawing at from the PlayerDrawInfo we pass into this method. Always use this and not player.itemLocation.
                        texture.Frame(), //our source rectangle should be the entire frame of our texture. If our mask was animated it would be the current frame of the animation.
                        color, //since we want our glowmask to glow, we tell it to draw with Color.White. This will make it ignore all lighting
                        rot, //the rotation of the player's item based on how they used it. This allows our glowmask to rotate with swingng swords or guns pointing in a direction.
                        origin, //the origin that our mask rotates about. This needs to be adjusted based on the player's direction, thus the ternary expression.
                        player.HeldItem.scale, //scales our mask to match the item's scale
                        info.spriteEffects, //the PlayerDrawInfo that was passed to this will tell us if we need to flip the sprite or not.
                        0 //we dont need to worry about the layer depth here
                    ));
            }
        }

        private void DrawLobWeaponBack(PlayerDrawInfo info)
        {
            Player player = info.drawPlayer;
            if (!player.HeldItem.IsAir && player.itemAnimation != 0 && player.HeldItem.type == mod.ItemType("SolemnLamentS"))
            {
                LobotomyGlobalItem item = player.HeldItem.GetGlobalItem<LobotomyGlobalItem>();

                if (!item.CustomDraw)
                    return;

                Texture2D texture = mod.GetTexture("Items/SolemnLamentS1");

                Color color = Lighting.GetColor((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f));

                Vector2 position = info.itemLocation - Main.screenPosition;
                Vector2 origin = new Vector2(player.direction == 1 ? 0 : texture.Width, texture.Height);
                float rot = player.itemRotation;

                if (player.HeldItem.useStyle == 5)
                {
                    Vector2 textureCenter = new Vector2((float)(texture.Width / 2f), (float)(texture.Height / 2f));

                    float num = 10f;
                    Vector2 result = textureCenter;
                    result.X = num;
                    ItemLoader.HoldoutOffset(player.gravDir, player.HeldItem.type, ref result);

                    /*if (player.GetModPlayer<LobotomyModPlayer>().SolemnSwitch)
                    {
                        player.itemRotation -= MathHelper.ToRadians(45) * player.direction;
                        rot += MathHelper.ToRadians(45) * player.direction;
                    }*/

                    Vector2 playerItemPos = result;

                    int x = (int)playerItemPos.X;
                    textureCenter.Y = playerItemPos.Y;
                    origin = new Vector2(-x, texture.Height / 2);
                    if (player.direction == -1)
                    {
                        origin = new Vector2(texture.Width + x, texture.Height / 2);
                    }
                    position.X += textureCenter.X + (!player.GetModPlayer<LobotomyModPlayer>().SolemnSwitch ? 6 : 3) * player.direction;
                    position.Y += textureCenter.Y;

                    if (!player.GetModPlayer<LobotomyModPlayer>().SolemnSwitch)
                    {
                        rot -= MathHelper.ToRadians(30 + 75 * (1 - (float)player.itemAnimation/(float)player.itemAnimationMax)) * player.direction;
                    }
                }

                Main.playerDrawData.Add(
                    new DrawData(
                        texture, //pass our glowmask's texture
                        position, //pass the position we should be drawing at from the PlayerDrawInfo we pass into this method. Always use this and not player.itemLocation.
                        texture.Frame(), //our source rectangle should be the entire frame of our texture. If our mask was animated it would be the current frame of the animation.
                        color, //since we want our glowmask to glow, we tell it to draw with Color.White. This will make it ignore all lighting
                        rot, //the rotation of the player's item based on how they used it. This allows our glowmask to rotate with swingng swords or guns pointing in a direction.
                        origin, //the origin that our mask rotates about. This needs to be adjusted based on the player's direction, thus the ternary expression.
                        player.HeldItem.scale, //scales our mask to match the item's scale
                        info.spriteEffects, //the PlayerDrawInfo that was passed to this will tell us if we need to flip the sprite or not.
                        0 //we dont need to worry about the layer depth here
                    ));
            }

            if (!player.HeldItem.IsAir && player.itemAnimation != 0 && player.HeldItem.type == mod.ItemType("FaintAromaS") && player.heldProj > -1 && Main.projectile[player.heldProj].type == mod.ProjectileType("FaintAromaS") )
            {
                Projectile projectile = Main.projectile[player.heldProj];
                
                Texture2D tex = Main.projectileTexture[projectile.type];
                float rot = projectile.ai[1];
                Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true) + new Vector2(8, 0).RotatedBy(rot);
                Vector2 position = ownerMountedCenter - Main.screenPosition;
                position.X += 8f * player.direction;
                Vector2 origin = new Vector2(2, 42);

                Main.playerDrawData.Add(
                    new DrawData(tex, position, tex.Frame(), Lighting.GetColor((int)player.position.X/16, (int)player.position.Y/16), rot + MathHelper.ToRadians(45), origin, projectile.scale * 1.2f, 0, 0));
            }
        }

        public static readonly PlayerLayer MiscEffectsFront = new PlayerLayer("LobotomyMod", "MiscEffectsFront", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("LobotomyCorp");
            LobotomyModPlayer modPlayer = ModPlayer(player);
            if (modPlayer.FaintAromaPetal > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (modPlayer.FaintAromaPetal <= modPlayer.FaintAromaPetalMax * i)
                        continue;

                    Texture2D texture = mod.GetTexture("Misc/AlriunePetal");
                    int drawX = (int)(drawInfo.position.X + player.width / 2f - Main.screenPosition.X);
                    int drawY = (int)(drawInfo.position.Y + player.height / 2f - Main.screenPosition.Y);

                    float rot = 0f;
                    if (i == 1)
                        rot += 90;
                    else if (i == 2)
                        rot += 45;
                    Vector2 Offset = new Vector2(-24 * player.direction, 0).RotatedBy(MathHelper.ToRadians(rot * player.direction));
                    Offset.Y -= 14;

                    rot = MathHelper.ToRadians(rot * player.direction + (player.direction == 1 ? 0 : 180)) - MathHelper.ToRadians(135);

                    float alpha = Lerp(0, 1f, ((modPlayer.FaintAromaPetal - modPlayer.FaintAromaPetalMax * i) / modPlayer.FaintAromaPetalMax));
                    Color color = Lighting.GetColor((int)(player.position.X / 16), (int)(player.position.Y / 16)) * alpha;

                    DrawData data = new DrawData(texture, new Vector2(drawX, drawY) + Offset, null, color, rot, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, 0, 0);
                    Main.playerDrawData.Add(data);
                }
            }
        });
    }
}