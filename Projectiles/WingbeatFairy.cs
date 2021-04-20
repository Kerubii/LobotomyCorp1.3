using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class WingbeatFairy : ModProjectile
	{
		public override void SetStaticDefaults() {
            Main.projFrames[projectile.type] = 6;
        }

		public override void SetDefaults() {
			projectile.width = 30;
			projectile.height = 24;
			projectile.aiStyle = -1;
			projectile.penetrate = 1;
			projectile.scale = 1f;
            projectile.timeLeft = 60;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.friendly = true;
		}

        public override void AI()
        {
            if(Main.rand.Next(5) == 0)
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 15);
            projectile.frameCounter++;
            if (projectile.frameCounter > 36)
            {
                projectile.frameCounter = 0;
            }
            projectile.frame = (int)(projectile.frameCounter / 6);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Fairy"), 600);
        }

        public override void Kill(int timeLeft)
        {
           for (int i = 0; i < 8; i++)
           {
                Vector2 dir = new Vector2(1, 0).RotatedBy(MathHelper.ToRadians(45) * i);
                Dust.NewDustPerfect(projectile.Center + dir, 15, dir);
           }
            Main.PlaySound(SoundID.NPCHit5, projectile.Center);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = 1;
        }
    }
}
