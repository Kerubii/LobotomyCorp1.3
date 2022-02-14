using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static LobotomyCorp.LobotomyCorp;

namespace LobotomyCorp.Projectiles
{
    public class DiscordLingeringSlash : ModProjectile
    {
        public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
            Main.projFrames[projectile.type] = 9;
        }

        public override void SetDefaults() {
            projectile.width = 74;
            projectile.height = 112;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18;

            //projectile.hide = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
        }

        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 1)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
            }
            projectile.rotation = projectile.velocity.ToRotation();
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}
