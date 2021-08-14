using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class RealizedWingbeat : ModProjectile
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Gluttony");
        }

		public override void SetDefaults() {
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.scale = 1f;
			projectile.alpha = 0;
            projectile.timeLeft = 8;

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
			projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
			projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);
			projectile.position += projectile.velocity * projectile.ai[1];
            projectile.ai[1] += 0.5f;
            if (LobotomyModPlayer.ModPlayer(projOwner).RealizedWingbeatMeal < 0)
                return;
            NPC meal = Main.npc[LobotomyModPlayer.ModPlayer(projOwner).RealizedWingbeatMeal];
            if (!meal.active || meal.life <= 0 || projectile.ai[0] == 2)
                return;
            Vector2 delta = meal.Center - ownerMountedCenter;
            float dist = delta.Length();
            float rot = delta.ToRotation();
            dist -= meal.width < meal.height ? meal.height : meal.width;
            if (dist < 72)
            {
                projectile.position = meal.Center;
                if (Math.Abs(projectile.velocity.X) > 5.7f)//11.3)
                    projectile.velocity.X *= Math.Sign(projectile.velocity.X) == Math.Sign(delta.X) ? 1 : -1;
                if (Math.Abs(projectile.velocity.Y) > 5.7f)//11.3)
                    projectile.velocity.Y *= Math.Sign(projectile.velocity.Y) == Math.Sign(delta.Y) ? 1 : -1;
                projectile.ai[0]++;
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            return (projectile.ai[0] == 0 || (projectile.ai[0] == 1 && target.whoAmI == LobotomyModPlayer.ModPlayer(Main.player[projectile.owner]).RealizedWingbeatMeal));
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            float angle = projectile.velocity.ToRotation();
            Player owner = Main.player[projectile.owner];
            Vector2 OldPos = owner.position;
            float dist = owner.height + 16 + (target.width < target.height ? target.height : target.width) / 2;
            //owner.Teleport(target.Center + new Vector2(dist,0).RotatedBy(angle) - owner.Size/2, -1, -1);
            owner.velocity = (target.Center + new Vector2(dist, 0).RotatedBy(angle) - owner.Center) / (owner.itemAnimation / 2);
            for (int i = 0; i < 5; i++)
            {
                //Dust.NewDust(OldPos, owner.width, owner.height, 15);
                Dust.NewDust(owner.position, owner.width, owner.height, 15);
                Dust.NewDust(target.position, target.width, target.height, 5, 0, 0, 0, default(Color), 1.5f);
                Vector2 delt = (owner.position - OldPos) * i / 5f;
                Dust.NewDustPerfect(OldPos + delt, 5, null, 0, default(Color), 1.5f);
            }
            float scale = 0.2f;
            int random = Main.rand.Next(2) == 0 ? 1 : -1;
            int dir = Math.Sign(projectile.velocity.X);
            for (int i = -5; i < 6; i++)
            {
                Vector2 dustPos = target.Center + new Vector2(-4 * i, 0).RotatedBy(angle + 0.785398f * random);
                Dust dust = Dust.NewDustPerfect(dustPos, 87, Vector2.Zero, 0, default(Color), scale);
                dust.noGravity = true;
                dust.fadeIn = 1.2f;
                scale += 0.13f;
            }
            int heal = (int)(damage * (target.life/(float)target.lifeMax)) / 3 * (LobotomyGlobalNPC.LNPC(target).WingbeatFairyMeal ? 3 : 1);
            if (target.life <= 0)
                heal = damage / 10 * (LobotomyGlobalNPC.LNPC(target).WingbeatFairyMeal ? 3 : 1);
            owner.statLife += heal;
            owner.HealEffect(heal, false);

            projectile.timeLeft = owner.itemAnimation / 2 + 1;
            owner.itemAnimation /= 2;
            owner.immuneTime = owner.itemAnimation;
            owner.immune = true;
            owner.itemTime /= 2;
            //owner.velocity = projectile.velocity/3;
            owner.direction = dir;
            if (Main.rand.Next(6) != 0)
                LobotomyModPlayer.ModPlayer(owner).RealizedWingbeatMeal = target.whoAmI;
            else
            {
                LobotomyModPlayer.ModPlayer(owner).RealizedWingbeatMeal = 0;
            }
            projectile.ai[0] = 2;
        }

        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] == 2)
                Main.player[projectile.owner].velocity *= 0.05f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}
