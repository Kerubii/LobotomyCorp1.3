using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class SoundOfAStar : ModProjectile
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Anime");
        }

		public override void SetDefaults() {
			projectile.width = 12;
			projectile.height = 12;
			projectile.aiStyle = -1;
			projectile.penetrate = 1;
			projectile.scale = 1f;
            projectile.timeLeft = 80;

            projectile.tileCollide = false;
			projectile.minion = true;
			projectile.friendly = true;
		}

        public override void AI() {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);

            projectile.ai[0]++;

            if (projectile.ai[0] < 70)
            {
                projectile.position = ownerMountedCenter + projectile.velocity * projectile.ai[1];
                if (projectile.ai[0] < 60)
                    projectile.ai[1] += 0.5f * (projectile.ai[0] > 20 ? 1f - ((projectile.ai[0] - 20f ) / 40f) : 1f);
            }
            else if (projectile.ai[0] == 70)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    Vector2 targetPos = Main.MouseWorld + new Vector2(Main.rand.Next(32), Main.rand.Next(32));
                    Projectile.NewProjectile(targetPos, Vector2.Zero, mod.ProjectileType("SoundOfAStarShoot"), projectile.damage, projectile.knockBack, projectile.owner);
                    projectile.velocity = Vector2.Normalize(targetPos - projectile.Center) * -4f;
                }
                for (int i = 0; i < 8; i++)
                {
                    Vector2 speed = new Vector2(4, 0).RotatedBy(MathHelper.ToRadians(i * 45));
                    Dust.NewDustPerfect(projectile.Center, 91, speed, 0, default(Color), 0.5f).noGravity = true;
                }
            }
            else
            {
                projectile.scale -= 0.1f;
                return;
            }

            if (projectile.ai[0] > 30)
            {
                projOwner.itemTime = 2;
                projOwner.itemAnimation = 2;
                projectile.direction = projOwner.direction;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.Draw(
                Main.projectileTexture[projectile.type],
                projectile.Center - Main.screenPosition + (projectile.ai[0] > 45 && projectile.ai[0] < 60 ? new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)) : Vector2.Zero),
                Main.projectileTexture[projectile.type].Frame(),
                lightColor,
                projectile.rotation,
                Main.projectileTexture[projectile.type].Size()/2,
                projectile.scale * 0.5f, 
                0f, 0f);
            return false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override bool CanHitPvp(Player target)
        {
            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return projectile.ai[0] > 70;
        }
    }
    
    public class SoundOfAStarShoot : ModProjectile
    {
        public override string Texture => "LobotomyCorp/Projectiles/SoundOfAStar";

        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.timeLeft = 5;
            projectile.alpha = 255;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;

            projectile.minion = true;
            projectile.friendly = true;
        }

        public override void AI()
        {
            if (projectile.ai[0]++ == 0)
            {
                for (int i = 0; i < 180; i++)
                {
                    Vector2 speed = new Vector2(4, 0).RotatedBy(MathHelper.ToRadians(i * 2));
                    Dust.NewDustPerfect(projectile.Center, 91, speed).noGravity = true;
                }
            }
        }
    }
}
