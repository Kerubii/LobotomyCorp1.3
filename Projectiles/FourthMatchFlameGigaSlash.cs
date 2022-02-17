using System;
using LobotomyCorp.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class FourthMatchFlameGigaSlash : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 126;
            projectile.height = 126;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.timeLeft = 100;

            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
            projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);
            projectile.position += projectile.velocity * 120;
            if (player.itemAnimation == 1)
                projectile.Kill();

            if (projectile.localAI[0] < 12)
            {
                projectile.localAI[0]++;
                for (int i = 0; i < 8; i++)
                {
                    float distance = Main.rand.NextFloat(1f);
                    float rotation = Main.rand.Next(-240, 240);

                    Vector2 dustPos;
                    dustPos.X = (290 + 40 * distance) * (float)Math.Cos(rotation);
                    dustPos.Y = (45 + 10 * distance) * (float)Math.Sin(rotation);

                    Vector2 dustVel = new Vector2(3f, 0).RotatedBy(dustPos.ToRotation() - 1.57f * Math.Sign(projectile.velocity.X));
                    dustPos = dustPos.RotatedBy(projectile.velocity.ToRotation());


                    Dust d = Dust.NewDustPerfect(projectile.Center + dustPos, DustID.Fire, dustVel);

                    if (Main.rand.Next(3) < 2)
                    {
                        d.fadeIn = Main.rand.NextFloat(0.8f, 1.8f);
                        d.noGravity = true;
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.HasBuff(mod.BuffType("Matchstick")))
                target.buffTime[target.FindBuffIndex(mod.BuffType("Matchstick"))] += 300;
            else
                target.AddBuff(mod.BuffType("Matchstick"), 300);
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (target.HasBuff(mod.BuffType("Matchstick")))
                target.buffTime[target.FindBuffIndex(mod.BuffType("Matchstick"))] += 300;
            else
                target.AddBuff(mod.BuffType("Matchstick"), 300);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int i = -1; i < 3; i++)
            {
                if (i == 0)
                    continue;

                float rotation = projectile.velocity.ToRotation();
                Vector2 center = new Vector2(projHitbox.X, projHitbox.Y) + new Vector2(projectile.width * i,0).RotatedBy(rotation);

                if (new Rectangle((int)center.X, (int)center.Y, projectile.width, projectile.height).Intersects(targetHitbox))
                    return true;
            }
            return base.Colliding(projHitbox, targetHitbox);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];
            CustomShaderData shader = LobotomyCorp.LobcorpShaders["FourthMatchFlame"].UseOpacity(0.5f * (float)Math.Cos(3.15f * ((float)player.itemAnimation/(float)player.itemAnimationMax)) + 0.5f);

            int dir = Math.Sign(projectile.velocity.X);
            SlashTrail trail = new SlashTrail(180, 45, 0);
            trail.color = Color.Red;
            trail.DrawEllipse(projectile.Center, projectile.velocity.ToRotation(), 1.47f * dir - 0.698132f * (float)Math.Sin(1.57f * (1 - ((float)player.itemAnimation / (float)player.itemAnimationMax))) * dir, dir, 400, 75, 128, shader);

            return false;
        }
    }

}
