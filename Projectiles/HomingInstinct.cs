using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class HomingInstinct : ModProjectile
	{
		public override void SetStaticDefaults() {
            //DisplayName.SetDefault("Spear");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.scale = 1f;
            projectile.timeLeft = 180;

            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
        }    
            
        public override void AI() {
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, 0, 0);
            }
            int x = (int)(projectile.Center.X / 16), y = (int)(projectile.Center.Y / 16);
            Tile tile = Main.tile[x, y];
            if (!tile.active())
            {
                Projectile.NewProjectile(new Vector2(x * 16,y * 16), projectile.velocity, mod.ProjectileType("HomingInstinctBlock"), 0, 0);
                /*WorldGen.PlaceObject(x, y, mod.TileType("YellowBrickRoad"));
                if (!(projectile.velocity.ToRotation() == 0 || projectile.velocity.ToRotation() == 180))
                    WorldGen.PoundPlatform(x, y);*/
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
    }
}
