//css_ref ../tModLoader.dll
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Projectiles
{
	class WorkerBee : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 13;
			//Main.projPet[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
		}
		
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 32;
			projectile.height = 42;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 3600;
			projectile.tileCollide = false;

            projectile.localNPCHitCooldown = 15;
            projectile.usesLocalNPCImmunity = true;
            IntestinePhysics = Vector2.Zero;
		}

        private Vector2 IntestinePhysics = Vector2.Zero;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            CheckActive(player);
            bool moveLeft = false;
            bool moveRight = false;
            int Pos = 0;
            for (int i = projectile.whoAmI - 1; i >= 0; i--)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == projectile.type && Main.projectile[i].owner == projectile.owner) Pos++;
            }
            int targetFollowDist = 40 * (Pos + 1) * player.direction;
            if (player.Center.X < projectile.Center.X + (float)targetFollowDist - 10f)
            {
                moveLeft = true;
            }
            else if (player.Center.X > projectile.Center.X + (float)targetFollowDist + 10f)
            {
                moveRight = true;
            }

            int flyDistance = 1200 + 40 * projectile.minionPos;
            if (player.rocketDelay2 > 0)
            {
                projectile.ai[0] = 1f;
                projectile.netUpdate = true;
            }
            float distance = Vector2.Distance(projectile.Center, player.Center);
            if (distance > 2000f)
            {
                projectile.position.X = player.position.X + (float)(player.width / 2) - (float)(projectile.width / 2);
                projectile.position.Y = player.position.Y + (float)(player.height / 2) - (float)(projectile.height / 2);
            }
            else if (distance > flyDistance || Math.Abs(projectile.Center.Y - player.Center.Y) > 300f)
            {
                projectile.ai[0] = 1f;
                projectile.netUpdate = true;
            }

            if (projectile.ai[0] != 0f)
            {
                projectile.tileCollide = false;
                float moveDistX = player.position.X + (float)(player.width / 2) - projectile.Center.X - (float)(40 * player.direction);
                float viewRange = 600f;
                bool aggro = false;
                for (int k = 0; k < 200; k++)
                {
                    if (Main.npc[k].CanBeChasedBy(this))
                    {
                        distance = System.Math.Abs(player.Center.Y - Main.npc[k].Center.X) + Math.Abs(player.Center.Y - Main.npc[k].Center.Y);
                        if (distance < viewRange)
                        {
                            aggro = true;
                            break;
                        }
                    }
                }
                if (!aggro)
                {
                    moveDistX -= (float)(40 * projectile.minionPos * player.direction);
                }
                float moveDistY = player.position.Y + (float)(player.height / 2) - projectile.Center.Y;
                float moveDist = (float)System.Math.Sqrt((double)(moveDistX * moveDistX + moveDistY * moveDistY));
                float maxSpeed = 10f;
                if (moveDist < 200f && player.velocity.Y == 0f && projectile.position.Y + (float)projectile.height <= player.position.Y + (float)player.height && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    if (projectile.velocity.Y < -6f)
                    {
                        projectile.velocity.Y = -6f;
                    }
                    projectile.netUpdate = true;
                }
                if (moveDist < 60f)
                {
                    moveDistX = projectile.velocity.X;
                    moveDistY = projectile.velocity.Y;
                }
                else
                {
                    moveDist = maxSpeed / moveDist;
                    moveDistX *= moveDist;
                    moveDistY *= moveDist;
                }
                float acceleration = 0.2f;
                if (projectile.velocity.X < moveDistX)
                {
                    projectile.velocity.X += acceleration;
                    if (projectile.velocity.X < 0f)
                    {
                        projectile.velocity.X += acceleration * 1.5f;
                    }
                }
                if (projectile.velocity.X > moveDistX)
                {
                    projectile.velocity.X -= acceleration;
                    if (projectile.velocity.X > 0f)
                    {
                        projectile.velocity.X -= acceleration * 1.5f;
                    }
                }
                if (projectile.velocity.Y < moveDistY)
                {
                    projectile.velocity.Y += acceleration;
                    if (projectile.velocity.Y < 0f)
                    {
                        projectile.velocity.Y += acceleration * 1.5f;
                    }
                }
                if (projectile.velocity.Y > moveDistY)
                {
                    projectile.velocity.Y -= acceleration;
                    if (projectile.velocity.Y > 0f)
                    {
                        projectile.velocity.Y -= acceleration * 1.5f;
                    }
                }
                if ((double)projectile.velocity.X > 0.5)
                {
                    projectile.spriteDirection = -1;
                }
                else if ((double)projectile.velocity.X < -0.5)
                {
                    projectile.spriteDirection = 1;
                }
            }
            else
            {
                bool target = false;
                Vector2 targetCenter = projectile.position;
                float seperation = (float)(40 * Pos);

                float dist = 1500f;
                float currentTarget = -1;
                bool priority = false;
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    if (Main.npc[k].CanBeChasedBy(this))
                    {
                        NPC n = Main.npc[k];
                        float npcDist = Vector2.Distance(n.Center, projectile.Center);
                        if (!priority || LobotomyGlobalNPC.HornetTarget(n))
                        {
                            if (npcDist < dist)
                            {
                                if (currentTarget == -1 && npcDist <= dist)
                                {
                                    dist = npcDist;
                                }
                                if (Collision.CanHit(projectile.position, projectile.width, projectile.height, n.position, n.width, n.height))
                                {
                                    if (!priority && LobotomyGlobalNPC.HornetTarget(n))
                                    {
                                        priority = LobotomyGlobalNPC.HornetTarget(n);
                                        dist = 1500f;
                                        k = 0;
                                    }
                                    currentTarget = k;
                                    target = true;
                                    targetCenter = n.Center;
                                }
                            }
                        }
                    }
                }
                
                if (target)
                {
                    moveLeft = false;
                    moveRight = false;
                    if (targetCenter.X < projectile.Center.X)
                    {
                        moveLeft = true;
                    }
                    else if (targetCenter.X > projectile.Center.X)
                    {
                        moveRight = true;
                    }
                }

                projectile.rotation = 0f;
                projectile.tileCollide = true;
                float speed = 0.2f;
                float maxAccel = 9f;

                if (moveLeft)
                {
                    if ((double)projectile.velocity.X > 0)
                        projectile.velocity.X *= 0.8f;
                    projectile.velocity.X -= speed;
                }
                else if (moveRight)
                {
                    if ((double)projectile.velocity.X < 0)
                        projectile.velocity.X *= 0.8f;
                    projectile.velocity.X += speed;
                }
                else
                {
                    projectile.velocity.X *= 0.8f;
                    if (projectile.velocity.X >= -speed && projectile.velocity.X <= speed)
                        projectile.velocity.X = 0;
                }
                bool collide = false;
                if (moveLeft || moveRight)
                {
                    int x = (int)((projectile.Center.X + projectile.velocity.X) / 16);
                    int y = (int)(projectile.Center.Y / 16);
                    if (moveLeft)
                        --x;
                    if (moveRight)
                        ++x;
                    //x += (int)projectile.velocity.X/16;
                    if (WorldGen.SolidTile(x, y))
                        collide = true;
                }
                bool pBelow = player.Center.Y > projectile.position.Y + projectile.height;
                Collision.StepUp(ref projectile.position, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY, 1, false);
                if (projectile.velocity.Y == 0f)
                {
                    int dir = 0;
                    bool jump = false;
                    if (moveLeft)
                        dir = -1;
                    if (moveRight)
                        dir = 1;
                    bool targetAbove = projectile.position.Y + projectile.height > player.position.Y + player.height;
                    if (target)
                        targetAbove = projectile.position.Y > targetCenter.Y + Main.npc[(int)currentTarget].height/2;
                    if (targetAbove && (moveLeft || moveRight) && dir == (int)Math.Sign(projectile.velocity.X))
                    {
                        int y = (int)((projectile.position.Y + projectile.height + 8) / 16);
                        int x = (int)((projectile.Center.X + (projectile.width / 2 + 1) * dir) / 16);
                        if (!Main.tile[x, y].active() || !Main.tileSolid[Main.tile[x, y].type])
                        {
                            projectile.velocity.Y -= 8f;
                            jump = true;
                        }
                    }
                    else if (target && targetAbove)
                    {
                        Vector2 delta = Main.npc[(int)currentTarget].Center - projectile.Center;
                        if (projectile.position.X > Main.npc[(int)currentTarget].position.X - 32 && projectile.position.X + projectile.width < Main.npc[(int)currentTarget].position.X + Main.npc[(int)currentTarget].width + 32)
                        {
                            float time = Math.Abs(delta.Y/3);
                            if (time == 0)
                                time = 1;
                            projectile.velocity.Y = (delta.Y - 0.5f * 0.4f * time * time) / time;
                            if (projectile.velocity.Y < -20f)
                                projectile.velocity.Y = -20f;
                            Bite();
                            jump = true;
                        }
                    }
                    if (!pBelow && (projectile.velocity.X != 0f))
                    {
                        int i = (int)(projectile.Center.X) / 16;
                        int j = (int)(projectile.Center.Y) / 16 + 1;
                        if (moveLeft)
                            --i;
                        if (moveRight)
                            ++i;
                        WorldGen.SolidTile(i, j);
                    }
                    if (collide && !jump)
                    {
                        int i = (int)projectile.Center.X / 16;
                        int j = (int)(projectile.position.Y + projectile.height) / 16 + 1;
                        if (WorldGen.SolidTile(i, j) || Main.tile[i, j].halfBrick() || ((int)Main.tile[i, j].slope() > 0))
                        {
                            try
                            {
                                j--;
                                if (moveLeft)
                                    i--;
                                if (moveRight)
                                    i--;
                                i += (int)projectile.velocity.X;
                                if (!WorldGen.SolidTile(i, j - 1) && !WorldGen.SolidTile(i, j - 2))
                                    projectile.velocity.Y = -7f;
                                else if (!WorldGen.SolidTile(i, j - 2))
                                    projectile.velocity.Y = -9f;
                                else if (WorldGen.SolidTile(i, j - 5))
                                    projectile.velocity.Y = -14f;
                                else if (WorldGen.SolidTile(i, j - 4))
                                    projectile.velocity.Y = -13f;
                                else
                                    projectile.velocity.Y = -11f;
                            }
                            catch
                            {
                                projectile.velocity.Y = -9.1f;
                            }
                        }
                    }

                    if (projectile.velocity.X > maxAccel)
                        projectile.velocity.X = maxAccel;
                    if (projectile.velocity.X < -maxAccel)
                        projectile.velocity.X = -maxAccel;

                    if (projectile.velocity.X < 0.0)
                        projectile.direction = -1;
                    if (projectile.velocity.X > 0.0)
                        projectile.direction = 1;
                    if (projectile.velocity.X > speed & moveRight)
                        projectile.direction = 1;
                    if (projectile.velocity.X < -speed & moveLeft)
                        projectile.direction = -1;

                    if (moveLeft || moveRight)
                    {
                        projectile.spriteDirection = projectile.direction;
                    }
                    else if (target)
                    {
                        if (targetCenter.X < projectile.Center.X)
                            projectile.spriteDirection = -1;
                        else if (targetCenter.X > projectile.Center.X)
                            projectile.spriteDirection = 1;
                    }
                    else
                    {
                        projectile.spriteDirection = player.direction;
                    }
                }

                if (projectile.frame > 8)
                {
                    projectile.frameCounter++;
                    if (projectile.frameCounter > 3)
                    {
                        projectile.frameCounter = 0;
                        projectile.frame++;
                        if (projectile.frame > 12)
                        {
                            if (projectile.velocity.Y == 0)
                                projectile.frame = 0;
                            else
                                projectile.frame = 8;
                        }
                    }
                }
                else if (projectile.velocity.Y == 0)
                {
                    if (projectile.velocity.X == 0)
                    {
                        projectile.frameCounter++;
                        if (projectile.frameCounter > 5f)
                        {
                            projectile.frameCounter = 0;
                            projectile.frame++;
                            if (projectile.frame > 3)
                                projectile.frame = 0;
                        }
                    }
                    else if (projectile.velocity.X < -0.8f || projectile.velocity.X > 0.8f)
                    {
                        projectile.frameCounter = projectile.frameCounter + (int)(Math.Abs(projectile.velocity.X) / 2f);
                        projectile.frameCounter++;
                        if (projectile.frameCounter > 12)
                        {
                            projectile.frame++;
                            projectile.frameCounter = 0;
                        }
                        if (projectile.frame < 4 || projectile.frame > 7)
                            projectile.frame = 4;
                    }
                    else
                    {
                        projectile.frameCounter++;
                        if (projectile.frameCounter > 12f)
                        {
                            projectile.frameCounter = 0;
                            projectile.frame++;
                            if (projectile.frame > 3)
                                projectile.frame = 0;
                        }
                    }
                }
                else
                {
                    if (projectile.frame < 8)
                    {
                        projectile.frame = 8;
                        projectile.frameCounter = 0;
                    }
                }

                if (projectile.wet)
                {
                    projectile.Kill();
                }
                else
                    projectile.velocity.Y += 0.4f;
                if (projectile.velocity.Y > 10f)
                    projectile.velocity.Y = 10f;
            }
        }

        private void CheckActive(Player p)
        {
            if (!p.active || p.dead)
                projectile.Kill();
            if (p.statLife < p.statLifeMax * 0.25f)
            {
                projectile.Kill();
                Projectile.NewProjectile(projectile.Center, projectile.velocity, mod.ProjectileType("AngryWorkerBee"), 20, projectile.knockBack, 255, projectile.owner);
            }
        }

        private void Bite()
        {
            if (projectile.frame < 9)
            {
                projectile.frame = 9;
                projectile.frameCounter = 0;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("HornetDust"));
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.velocity.Y == 0)
            {
                Bite();
            }
            if (target.life <= 0 && Main.rand.Next(10) == 0)
            {
                LobotomyGlobalNPC.SpawnHornet(target, projectile.owner, 10, knockback);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
    }

    class AngryWorkerBee : ModProjectile
    {
        public override string Texture { get { return "LobotomyCorp/Projectiles/WorkerBee"; } }

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 13;
            //Main.projPet[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 42;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
        }
              
        public override void AI()
        {
            Player player = Main.player[(int)projectile.ai[0]];
            //CheckActive(player);
            bool moveLeft = false;
            bool moveRight = false;

            if (player.Center.X < projectile.Center.X)
            {
                moveLeft = true;
            }
            else if (player.Center.X > projectile.Center.X)
            {
                moveRight = true;
            }

            projectile.rotation = 0f;
            projectile.tileCollide = true;
            float speed = 0.08f;
            float maxAccel = 9f;

            if (moveLeft)
            {
                if ((double)projectile.velocity.X > -3.5)
                    projectile.velocity.X -= speed;
                projectile.velocity.X -= speed;
            }
            else if (moveRight)
            {
                if ((double)projectile.velocity.X < 3.5)
                    projectile.velocity.X += speed;
                projectile.velocity.X += speed;
            }
            else
                {
                    projectile.velocity.X *= 0.8f;
                    if (projectile.velocity.X >= -speed && projectile.velocity.X <= speed)
                        projectile.velocity.X = 0;
                }
                bool collide = false;
                if (moveLeft || moveRight)
                {
                    int x = (int)((projectile.Center.X + projectile.velocity.X) / 16);
                    int y = (int)(projectile.Center.Y / 16);
                    if (moveLeft)
                        --x;
                    if (moveRight)
                        ++x;
                    //x += (int)projectile.velocity.X/16;
                    if (WorldGen.SolidTile(x, y))
                        collide = true;
                }
                bool pBelow = player.Center.Y > projectile.position.Y + projectile.height;
                Collision.StepUp(ref projectile.position, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY, 1, false);
                if (projectile.velocity.Y == 0f)
                {
                    int dir = 0;
                    bool jump = false;
                    if (moveLeft)
                        dir = -1;
                    if (moveRight)
                        dir = 1;
                    bool targetAbove = projectile.position.Y + projectile.height > player.position.Y + player.height;
                    if (targetAbove && (moveLeft || moveRight) && dir == (int)Math.Sign(projectile.velocity.X))
                    {
                        int y = (int)((projectile.position.Y + projectile.height + 8) / 16);
                        int x = (int)((projectile.Center.X + (projectile.width / 2 + 1) * dir) / 16);
                        if (!Main.tile[x, y].active() || !Main.tileSolid[Main.tile[x, y].type])
                        {
                            projectile.velocity.Y -= 8f;
                            jump = true;
                        }
                    }
                    else if (targetAbove)
                    {
                        Vector2 delta = player.Center - projectile.Center;
                        if (projectile.position.X > player.position.X - 32 && projectile.position.X + projectile.width < player.position.X + player.width + 32)
                        {
                            float time = Math.Abs(delta.Y / 2);
                        if (time == 0)
                            time = 1;
                            projectile.velocity.Y = (Math.Abs(delta.Y) - 0.5f * 0.4f * time * time) / time;
                            if (projectile.velocity.Y < -12f)
                                projectile.velocity.Y = -12f;
                            Bite();
                            jump = true;
                        }
                    }
                    if (!pBelow && (projectile.velocity.X != 0f))
                    {
                        int i = (int)(projectile.Center.X) / 16;
                        int j = (int)(projectile.Center.Y) / 16 + 1;
                        if (moveLeft)
                            --i;
                        if (moveRight)
                            ++i;
                        WorldGen.SolidTile(i, j);
                    }
                    if (collide && jump)
                    {
                        int i = (int)projectile.Center.X / 16;
                        int j = (int)(projectile.position.Y + projectile.height) / 16 + 1;
                        if (WorldGen.SolidTile(i, j) || Main.tile[i, j].halfBrick() || ((int)Main.tile[i, j].slope() > 0))
                        {
                            try
                            {
                                j--;
                                if (moveLeft)
                                    i--;
                                if (moveRight)
                                    i--;
                                i += (int)projectile.velocity.X;
                                if (!WorldGen.SolidTile(i, j - 1) && !WorldGen.SolidTile(i, j - 2))
                                    projectile.velocity.Y = -7f;
                                else if (!WorldGen.SolidTile(i, j - 2))
                                    projectile.velocity.Y = -9f;
                                else if (WorldGen.SolidTile(i, j - 5))
                                    projectile.velocity.Y = -14f;
                                else if (WorldGen.SolidTile(i, j - 4))
                                    projectile.velocity.Y = -13f;
                                else
                                    projectile.velocity.Y = -11f;
                            }
                            catch
                            {
                                projectile.velocity.Y = -9.1f;
                            }
                        }
                    }

                    if (projectile.velocity.X > maxAccel)
                        projectile.velocity.X = maxAccel;
                    if (projectile.velocity.X < -maxAccel)
                        projectile.velocity.X = -maxAccel;

                    if (projectile.velocity.X < 0.0)
                        projectile.direction = -1;
                    if (projectile.velocity.X > 0.0)
                        projectile.direction = 1;
                    if (projectile.velocity.X > speed & moveRight)
                        projectile.direction = 1;
                    if (projectile.velocity.X < -speed & moveLeft)
                        projectile.direction = -1;

                    if (moveLeft || moveRight)
                    {
                        projectile.spriteDirection = projectile.direction;
                    }
                    else
                    {
                        projectile.spriteDirection = player.direction;
                    }
                }
                else
            {

            }

                if (projectile.frame > 8)
                {
                    projectile.frameCounter++;
                    if (projectile.frameCounter > 3)
                    {
                        projectile.frameCounter = 0;
                        projectile.frame++;
                        if (projectile.frame > 12)
                        {
                            if (projectile.velocity.Y == 0)
                                projectile.frame = 0;
                            else
                                projectile.frame = 8;
                        }
                    }
                }
                else if (projectile.velocity.Y == 0)
                {
                    if (projectile.velocity.X == 0)
                    {
                        projectile.frameCounter++;
                        if (projectile.frameCounter > 5f)
                        {
                            projectile.frameCounter = 0;
                            projectile.frame++;
                            if (projectile.frame > 3)
                                projectile.frame = 0;
                        }
                    }
                    else if (projectile.velocity.X < -0.8f || projectile.velocity.X > 0.8f)
                    {
                        projectile.frameCounter = projectile.frameCounter + (int)(Math.Abs(projectile.velocity.X) / 2f);
                        projectile.frameCounter++;
                        if (projectile.frameCounter > 12)
                        {
                            projectile.frame++;
                            projectile.frameCounter = 0;
                        }
                        if (projectile.frame < 4 || projectile.frame > 7)
                            projectile.frame = 4;
                    }
                    else
                    {
                        projectile.frameCounter++;
                        if (projectile.frameCounter > 12f)
                        {
                            projectile.frameCounter = 0;
                            projectile.frame++;
                            if (projectile.frame > 3)
                                projectile.frame = 0;
                        }
                    }
                }
                else
                {
                    if (projectile.frame < 8)
                    {
                        projectile.frame = 8;
                        projectile.frameCounter = 0;
                    }
                }

            if (projectile.wet)
            {
                projectile.Kill();
            }
            else
                projectile.velocity.Y += 0.4f;
            if (projectile.velocity.Y > 10f)
                projectile.velocity.Y = 10f;
            
        }

        /*public override void Kill(int timeLeft)
        {
            Main.NewText(projectile.position.X);
        }*/

        private void CheckActive(Player p)
        {
            if (!p.active || p.dead)
                projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("HornetDust"));
            }
        }

        private void Bite()
        {
            if (projectile.frame < 9)
            {
                projectile.frame = 9;
                projectile.frameCounter = 0;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.velocity.Y == 0)
            {
                Bite();
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
    }
}