using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class BoundaryOfDeathInitial : ModProjectile
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Gluttony");
        }

		public override void SetDefaults() {
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.penetrate = 2;
			projectile.scale = 1f;
			projectile.alpha = 0;
            projectile.timeLeft = 30;

			//projectile.hide = true;
			projectile.ownerHitCheck = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.friendly = true;
			
			projectile.extraUpdates = 30;
		}

		public override void AI() {
			if (projectile.timeLeft == 1)
			{
				Main.player[projectile.owner].Teleport(projectile.Center - Main.player[projectile.owner].Size / 2, 5 , 5);
			}
        }

        public override bool? CanHitNPC(NPC target)
        {
            return projectile.penetrate > 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0, 0, mod.ProjectileType("BoundaryOfDeath"), projectile.damage, knockback, Main.myPlayer, target.whoAmI);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false;
        }
    }
}
