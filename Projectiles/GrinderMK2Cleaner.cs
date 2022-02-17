using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LobotomyCorp.Utils;

namespace LobotomyCorp.Projectiles
{
    public class GrinderMk2Cleaner : ModProjectile
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Cleaning Tools");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.timeLeft = 1000;

            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 15;
        }

        public float order
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }

        public Vector2 projectileRoot
        {
            get => projectile.Center + projectileRootOffset;
        }

        public Vector2 projectileRootOffset
        {
            get => new Vector2(-2 * projectile.direction, 14).RotatedBy(projectile.rotation);
        }

        public Vector2 projectileElbow(Player player)
        {
            Vector2 elbow = player.RotatedRelativePoint(player.MountedCenter, true);
            int legLength = 34;
            float Length = Vector2.Distance(projectileRoot, elbow);
            if (Length > legLength * 2)
                Length = legLength * 2;
            float Angle = (float)Math.Acos(Length * Length / (2 * legLength * Length));
            int dir = -1;
            if (order < 3 && order > 0)
                dir = 1;
            float Rotation = (projectileRoot - elbow).ToRotation() + Angle * dir;
            elbow = elbow + new Vector2(legLength, 0).RotatedBy(Rotation);
            return elbow;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.direction = player.direction;

            projectile.rotation = (projectile.Center - ownerMountedCenter).ToRotation() + 1.57f;

            int dirX = order % 2 == 0 ? -1 : 1;
            int dirY = order < 2 ? -1 : 1;

            Vector2 targetPos = ownerMountedCenter + new Vector2(32 * dirX, 42 * dirY);
            float speed = 6f;
            if (player.HeldItem.type == mod.ItemType("GrinderMk2S"))
            {
                if (player.channel && player.altFunctionUse != 2)
                {
                    projectile.ai[1]++;
                    Vector2 offset = new Vector2(70 * dirX, 70 * dirY).RotatedBy(MathHelper.ToRadians(projectile.ai[1] * 14 * player.direction));
                    targetPos = ownerMountedCenter + offset;
                    speed = 24f;
                    LobotomyModPlayer.ModPlayer(player).GrinderMk2Battery--;
                    player.immune = true;
                    player.immuneNoBlink = true;
                    player.immuneTime = 5;
                }
                else
                {
                    projectile.ai[1] = 0;
                }

                if ((LobotomyModPlayer.ModPlayer(player).GrinderMk2Order == order || LobotomyModPlayer.ModPlayer(player).GrinderMk2Order == 4) && player.altFunctionUse == 2 && player.itemAnimation > player.itemAnimationMax / 2)
                {
                    LobotomyModPlayer.ModPlayer(player).GrinderMk2Battery--;
                    targetPos = Main.MouseWorld;
                    if (Vector2.Distance(targetPos, ownerMountedCenter) > 68)
                    {
                        targetPos = Main.MouseWorld - ownerMountedCenter;
                        targetPos.Normalize();
                        targetPos *= 68;
                        targetPos += ownerMountedCenter;
                    }
                    speed = 16f;
                }
            }

            Vector2 delta = targetPos - projectileRoot;
            if (delta.Length() > speed)
            {
                projectile.velocity = new Vector2(speed, 0).RotatedBy(delta.ToRotation());
            }
            else
                projectile.velocity = delta;
            projectile.Center += projectile.velocity;
            if (speed == 16)
                projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
            delta = projectileRoot - ownerMountedCenter;
            if (delta.Length() > 68)
            {
                projectile.Center = ownerMountedCenter + new Vector2(68, 0).RotatedBy(delta.ToRotation()) - projectileRootOffset;
            }
            projectile.timeLeft = 300;
            LobotomyModPlayer.ModPlayer(player).GrinderMk2Battery--;
            if (LobotomyModPlayer.ModPlayer(player).GrinderMk2Battery <= 0 || LobotomyModPlayer.ModPlayer(player).GrinderMk2Recharging)
            {
                projectile.Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.player[projectile.owner].AddBuff(BuffID.Frozen, 180);
            LobotomyModPlayer.ModPlayer(Main.player[projectile.owner]).GrinderMk2Recharging = true;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            
            //7/3, 41
            Texture2D texture = mod.GetTexture("Projectiles/GrinderMk2Arm2");
            Vector2 pos = ownerMountedCenter - Main.screenPosition;
            Vector2 origin = new Vector2(texture.Width/2, 5);
            Rectangle frame = new Rectangle(0, 0, texture.Width, texture.Height);
            float rotation = (projectileElbow(player) - ownerMountedCenter).ToRotation() - 1.57f;
            spriteBatch.Draw(texture, pos, new Rectangle?(frame), lightColor, rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            
            pos = projectileElbow(player) - Main.screenPosition;
            rotation = (projectileRoot - projectileElbow(player)).ToRotation() - 1.57f;
            spriteBatch.Draw(texture, pos, new Rectangle?(frame), lightColor, rotation, origin, projectile.scale, SpriteEffects.None, 0f);

            texture = Main.projectileTexture[projectile.type];
            if (order < 3 && order > 0)
                texture = mod.GetTexture("Projectiles/GrinderMk2Cleaner2");

            pos = projectile.Center - Main.screenPosition;
            origin = texture.Size() / 2;
            frame = new Rectangle(0, 0, texture.Width, texture.Height);
            spriteBatch.Draw(texture, pos, new Rectangle?(frame), lightColor, projectile.rotation, origin, projectile.scale, projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);

            texture = mod.GetTexture("Projectiles/GrinderMk2Battery");
            ownerMountedCenter.Y -= 48;
            pos = ownerMountedCenter - Main.screenPosition;
            origin = texture.Size() / 2;
            frame = new Rectangle(0, 0, texture.Width, texture.Height);
            lightColor = Lighting.GetColor((int)ownerMountedCenter.X/16, (int)ownerMountedCenter.Y/16);
            spriteBatch.Draw(texture, pos, new Rectangle?(frame), lightColor, 0, origin, projectile.scale, SpriteEffects.None, 0f);
        
            texture = mod.GetTexture("Projectiles/GrinderMk2Bar");
            LobotomyModPlayer modPlayer = LobotomyModPlayer.ModPlayer(player);
            int frameY = (int)((LobotomyModPlayer.GrinderMk2BatteryMax - (float)modPlayer.GrinderMk2Battery)/((float)LobotomyModPlayer.GrinderMk2BatteryMax/5)) * texture.Height/6;
            if (modPlayer.GrinderMk2Battery < LobotomyModPlayer.GrinderMk2BatteryMax / 5 && modPlayer.GrinderMk2Battery % 120 < 60)
                frameY += texture.Height/6;
            frame = new Rectangle(0, frameY, texture.Width, texture.Height/6);
            spriteBatch.Draw(texture, pos, new Rectangle?(frame), Color.White, 0, origin, projectile.scale, SpriteEffects.None, 0f);

            /*TrailingShader Trail = default(TrailingShader);
            Trail.ColorStart = Color.Blue;
            Trail.ColorEnd = Color.Blue;
            Trail.Draw(projectile);*/

            return false;
        }
    }

}
