using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class HarmonyShot : ModProjectile
	{
		public override void SetDefaults() {
			projectile.width = 24;
			projectile.height = 24;
			projectile.aiStyle = -1;
			projectile.penetrate = 1;
			projectile.scale = 1f;

            projectile.alpha = 255;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.friendly = true;
		}

		public override void AI() {
			for (int i = 0; i < 3; i++)
            {
				int n = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("NoteDust"));
				Main.dust[n].scale = 0.5f;
				Main.dust[n].noGravity = true;
			}
			if (Main.rand.Next(3) == 0)
			{
				int i = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("NoteDust"));
				Main.dust[i].scale = 1f;
				Main.dust[i].noGravity = true;
			}
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			for (int i = 0; i < 8; i++)
			{
				Main.dust[Dust.NewDust(target.position, target.width, target.height, mod.DustType("NoteDust"))].velocity.Y -= 1f;
			}
		}
    }
}
