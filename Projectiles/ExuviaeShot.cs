using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class ExuviaeShot : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.scale = 1f;
            projectile.timeLeft = 180;

            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
        }    
            
        public override void AI() {
            if (Main.rand.Next(15) == 0)
            {
                int i = Dust.NewDust(projectile.position, 10, 10, 36);
                Main.dust[i].noGravity = true;
            }
            projectile.rotation += 0.18f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(projectile.Center, new Vector2(Main.rand.NextFloat(-0.3f, 0.3f), Main.rand.NextFloat(-0.3f, 0.3f)), mod.ProjectileType("ExuviaeMist"), (int)(projectile.damage * 0.77f), 1f, projectile.owner);
            }
        }
    }
}
