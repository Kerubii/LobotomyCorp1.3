using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	public class HomingInstinctBlock : ModProjectile
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
            projectile.timeLeft = 600;

            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.friendly = true;
        }    
            
        public override void AI() {
            if (Main.rand.Next(30) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 57);
            }
            int x = (int)(projectile.Center.X / 16), y = (int)(projectile.Center.Y / 16);
            if (projectile.ai[0] == 0)
            {
                WorldGen.PlaceTile(x, y, mod.TileType("YellowBrickRoad"), true);
                //if (Main.tile[x - 1, y + 1].active() || Main.tile[x - 1, y - 1].active() || Main.tile[x + 1, y + 1].active() || Main.tile[x + 1, y - 1].active())
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if ((x == 0 || j == 0))
                            continue;
                        if (projectile.velocity.Y != 0 && YellowBrick(x + i, y + j) && !YellowBrickAdjacent(x + i, y + j))
                        {
                            WorldGen.PoundPlatform(x + i, y + j);
                            if (((i == 1 && j == 1) || (i == -1 && j == -1)) && YellowBrickAboveBelow(x + i, y + j))
                                WorldGen.PoundPlatform(x + i, y + j);
                        }
                    }
                }
                projectile.ai[0]++;
            }

            if (!Main.tile[x,y].active() || Main.tile[x,y].type != mod.TileType("YellowBrickRoad"))
            {
                projectile.Kill();
            }

            if (projectile.timeLeft == 1)
            {
                WorldGen.KillTile(x, y);
            }
        }

        public override void Kill(int timeLeft)
        {
            
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        private bool YellowBrick(int x, int y)
        {
            return (Main.tile[x, y].active() && Main.tile[x, y].type == mod.TileType("YellowBrickRoad"));
        }
        
        private bool YellowBrickAdjacent(int x, int y)
        {
            return (Main.tile[x - 1, y].active() && Main.tile[x - 1, y].type == mod.TileType("YellowBrickRoad") ||
                    Main.tile[x + 1, y].active() && Main.tile[x + 1, y].type == mod.TileType("YellowBrickRoad"));
        }

        private bool YellowBrickAboveBelow(int x, int y)
        {
            return (Main.tile[x, y - 1].active() && Main.tile[x, y - 1].type == mod.TileType("YellowBrickRoad") ||
                    Main.tile[x, y + 1].active() && Main.tile[x, y + 1].type == mod.TileType("YellowBrickRoad"));
        }
    }
}
