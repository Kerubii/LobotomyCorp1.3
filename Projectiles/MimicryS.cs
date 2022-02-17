using LobotomyCorp.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class MimicryS : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
        }

		public override void SetDefaults() {
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.scale = 1f;
			projectile.alpha = 0;
            projectile.timeLeft = 60;

			//projectile.hide = true;
			projectile.ownerHitCheck = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.friendly = true;
		}
        
		public override void AI() {
			Player projOwner = Main.player[projectile.owner];
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			projectile.direction = projOwner.direction;
			//projOwner.heldProj = projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;

            float rot = projectile.velocity.ToRotation();

            int rest = 180 * projectile.direction;
            int peak = 100 * projectile.direction;
            int between = rest + peak;

            projectile.direction = Math.Sign(projectile.velocity.X);

            if (projOwner.itemAnimation > 22)
            {
                float windUp = projOwner.itemAnimationMax - 20;
                float anim = projOwner.itemAnimation - 20;
                rot += (MathHelper.ToRadians(rest) - (MathHelper.ToRadians(between) * (1 - (anim / windUp))));
                if (projectile.ai[0] < 25)
                    projectile.ai[0] += 1.56f;
            }
            else if (projOwner.itemAnimation <= 6)
            {
                rot += MathHelper.ToRadians(rest);
            }
            else if (projOwner.itemAnimation <= 19)
            {
                float Slash = 19 - 6;
                float anim = projOwner.itemAnimation - 6;
                rot -= (MathHelper.ToRadians(peak) - (MathHelper.ToRadians(between) * (1 - (anim / Slash))));
                if (projOwner.itemAnimation <= 14 && projectile.ai[0] > 0)
                    projectile.ai[0] -= 4.8f;
                else
                    projectile.ai[0] += 7.23f;
            }
            else
            {
                rot -= MathHelper.ToRadians(peak);
                for (int i = 0; i < 6; i++)
                {
                    projectile.oldRot[i] = rot + MathHelper.ToRadians(projectile.direction == 1 ? 45 : 135);
                }
            }

            Vector2 velRot = new Vector2(1, 0).RotatedBy(rot);
            projOwner.itemRotation = (float)Math.Atan2(velRot.Y * projectile.direction, velRot.X * projectile.direction);
            projectile.rotation = rot + MathHelper.ToRadians(projectile.direction == 1 ? 45 : 135);

            projectile.position = ownerMountedCenter;

            if (projOwner.itemAnimation == 1)
                projectile.Kill();
		}

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            Player projOwner = Main.player[projectile.owner];
            //Sweetspot
            if (projOwner.itemAnimation <= 19 && projOwner.itemAnimation > 6)
            {
                Vector2 Center = new Vector2(84, 0).RotatedBy(projectile.velocity.ToRotation());
                
                if (projOwner.itemAnimation <= 11)
                    Center = new Vector2(48, 0).RotatedBy(projectile.velocity.ToRotation() + MathHelper.ToRadians(160) * projectile.direction);
                else if (projOwner.itemAnimation <= 15)
                    Center = new Vector2(55, 0).RotatedBy(projectile.velocity.ToRotation() + MathHelper.ToRadians(90) * projectile.direction);
                
                Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true) + Center;
                hitbox = new Rectangle((int)ownerMountedCenter.X - 40, (int)ownerMountedCenter.Y - 40, 80, 80);
            }
            if ( projOwner.itemAnimation > 22 )
            {
                Vector2 Center = new Vector2(55, 0).RotatedBy(projectile.velocity.ToRotation());

                if (projOwner.itemAnimation > 28)
                    Center = new Vector2(55, 0).RotatedBy(projectile.velocity.ToRotation() + MathHelper.ToRadians(90) * projectile.direction);

                Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true) + Center;
                hitbox = new Rectangle((int)ownerMountedCenter.X - 40, (int)ownerMountedCenter.Y - 40, 80, 80);
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player projOwner = Main.player[projectile.owner];
            damage = Main.DamageVar(projOwner.GetWeaponDamage(projOwner.HeldItem));
            if (projOwner.itemAnimation <= 19 && projOwner.itemAnimation > 15)
            {
                Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("MimicrySEffect"), 0, 0, projectile.owner, projectile.direction);
                damage = (damage * 3);
                target.immune[projectile.owner] = projOwner.itemAnimation;
            }
            else if (projOwner.itemAnimation > 22)
            {
                knockback = 0.2f;
                target.immune[projectile.owner] = projOwner.itemAnimation - 20;
            }
        }

        public Vector2[] trailPos = new Vector2[6];

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 position = projectile.Center - Main.screenPosition;
            position = projectile.Center - Main.screenPosition - new Vector2(projectile.direction == 1 ? 8 : 16, 10) + new Vector2(10, 0).RotatedBy(projectile.rotation);
            Vector2 originOffset = new Vector2(projectile.ai[0], 0).RotatedBy(MathHelper.ToRadians(projectile.direction == 1 ? 135 : 45));
            Vector2 origin = new Vector2(56 + (projectile.direction == 1 ? 0 : 35), 60) + originOffset;
            SpriteEffects spriteEffect = projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (Main.player[projectile.owner].itemAnimation <= 19)
            {
                /*for (int i = 0; i < 10; i++)
                {
                    position = projectile.oldPos[i] + projectile.Size / 2 - Main.screenPosition - new Vector2(projectile.direction == 1 ? 8 : 16, 10) + new Vector2(10, 0).RotatedBy(projectile.rotation);
                    Texture2D tex = mod.GetTexture("Projectiles/MimicrySBlur");
                    Color color = lightColor;
                    //color.A = (byte)(color.A * 0.15f);
                    color *= (1 - ((float)i / 6f)) * 0.4f;

                    spriteBatch.Draw(tex, position, new Microsoft.Xna.Framework.Rectangle?
                                        (
                                            new Rectangle
                                            (
                                                0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height
                                            )
                                        ),
                    color, projectile.oldRot[i], origin, projectile.scale, spriteEffect, 0f);
                }*/
                SlashTrail trail = new SlashTrail(24, 0.785f - MathHelper.ToRadians(projectile.direction == 1 ? 0 : 90));
                Vector2[] trailPos = new Vector2[projectile.oldPos.Length];
                for (int i = 0; i < trailPos.Length; i++)
                {
                    if (projectile.oldPos[i].Length() > 0)
                        trailPos[i] = projectile.position + new Vector2(44 + projectile.ai[0], 0).RotatedBy(projectile.oldRot[i] - 0.785f - MathHelper.ToRadians(projectile.direction == 1 ? 0 : 90));
                }

                Player projOwner = Main.player[projectile.owner];
                float prog = 1f - (float) projOwner.itemAnimation / 19f;
                CustomShaderData mimicry = LobotomyCorp.LobcorpShaders["MimicrySlash"].UseOpacity(prog);

                trail.DrawSpecific(trailPos, projectile.oldRot, Vector2.Zero, mimicry);
            }                     

            position = projectile.Center - Main.screenPosition - new Vector2(projectile.direction == 1 ? 8 : 16, 10) + new Vector2(10, 0).RotatedBy(projectile.rotation);
            spriteBatch.Draw(Main.projectileTexture[projectile.type], position, new Microsoft.Xna.Framework.Rectangle?
                                    (
                                        new Rectangle
                                        (
                                            0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height
                                        )
                                    ),
                lightColor * ((float)(255 - projectile.alpha) / 255f), projectile.rotation, origin, projectile.scale, spriteEffect, 0f);
            return false;
        }
    }

    class MimicrySEffect : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.alpha = 255;
            projectile.timeLeft = 15;
            projectile.tileCollide = false;
            projectile.friendly = true;

        }

        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 velocity = new Vector2(0, Main.rand.NextFloat(-8.00f, 8.00f)).RotatedBy(MathHelper.ToRadians(45 * projectile.ai[0]));
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, velocity.X, velocity.Y)];
                dust.fadeIn = Main.rand.NextFloat(1f, 2f);
                dust.noGravity = true;
            }
            float n = (projectile.ai[1] - 7) * 4;
            Vector2 offset = new Vector2(0, n).RotatedBy(MathHelper.ToRadians(45) * projectile.ai[0]);
            Vector2 pos = projectile.Center + offset;
            Dust dust2 = Dust.NewDustPerfect(pos, 87, Vector2.Zero, 0, default(Color), 1.2f *(projectile.ai[1] / 15f));
            dust2.noGravity = true;
            dust2.fadeIn = 0.2f + 1.2f * (projectile.ai[1] / 15f);
            projectile.ai[1]++;
        }
    }

    class MimicrySHello : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.penetrate = -1;
            projectile.timeLeft = 60;
            projectile.extraUpdates = 60;
            projectile.friendly = true;
            projectile.melee = true;
        }

        public override void AI()
        {
            if (projectile.ai[0] == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    Vector2 dustVel = projectile.velocity.RotatedBy(Main.rand.NextFloat(-0.47f, 0.47f));
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, dustVel.X, dustVel.Y)];
                    dust.fadeIn = 1.4f;
                }
                projectile.ai[0]++;
            }
            if (Main.rand.Next(10) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, projectile.velocity.X, projectile.velocity.Y)];
                dust.fadeIn = 1.4f;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 splosh = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
                Dust dust2 = Dust.NewDustPerfect(projectile.Center, 133, splosh, 0, default(Color), 0.1f);
                dust2.fadeIn = Main.rand.NextFloat(0.5f, 1.2f);
            }
            for (int i = 0; i < 10; i++)
            {
                Vector2 splosh = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
                Dust dust2 = Dust.NewDustPerfect(projectile.Center, 5, splosh, 0, default(Color), 1f);
                dust2.fadeIn = Main.rand.NextFloat(1f, 1.6f);
            }
        }
    }
}
