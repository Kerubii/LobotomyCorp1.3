using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LobotomyCorp.Items
{
	public class HornetS : SEgoItem
	{
		public override void SetStaticDefaults() 
		{
            // DisplayName.SetDefault("Penitence"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A complete E.G.O.\n" +
                               "\"If you feel an abdominal pain and a tingling sensation in your neck,\nthe best thing you can do now is look at the great blue sky you'll never get to see again.\"\n");
            PassiveText = "Loyalty - Summon 2 worker bees at your aide\n" +
                          "Spore - Hitting enemies with this weapon marks them with spores that produce more temporary worker bees when killed\n" +
                          "Pheromones - Release Pheromones that strengthens your worker bees, this ability has a 1 minute cooldown\n" +
                          "|Balling Embrace of Death - Having less that 25% of your maximum health will cause your worker bees to attack you";
            EgoColor = LobotomyCorp.WawRarity;
        }

		public override void SetDefaults() 
		{
			item.damage = 130;
			item.summon = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 26;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("RealizedHornet");
            item.shootSpeed = 122f;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void HoldItem(Player player)
        {
            /*if (player.ownedProjectileCounts[mod.ProjectileType("WorkerBee")] <= 1)
            {
                Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("WorkerBee"), item.damage, item.knockBack, player.whoAmI);
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(player.position, player.width, player.height, mod.DustType("HornetDust"));
                }
            }*/
        }

        public override bool SafeCanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/QueenBee_AtkBuff").WithVolume(0.25f);
                item.shoot = 0;
                for (int i = 0; i < 40; i++)
                {
                    Vector2 vel = new Vector2(4, 0).RotateRandom(6.28f);
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, mod.DustType("HornetDust"), vel.X, vel.Y)];
                    dust.noLight = false;
                    dust.fadeIn = Main.rand.NextFloat(2.2f, 2.8f);
                }
                for (int i = 0; i < 80; i++)
                {
                    Vector2 vel = new Vector2(4, 0).RotateRandom(6.28f);
                    Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, mod.DustType("HornetDust"), vel.X, vel.Y)];
                    dust.noLight = false;
                    dust.fadeIn = Main.rand.NextFloat(1.8f, 2f);
                }
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].chaseable && Vector2.Distance(player.Center, Main.npc[i].Center) < 1500f)
                        LobotomyGlobalNPC.LNPC(Main.npc[i]).QueenBeeSpore = 1200;
                }
            }
            else
            {
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/QueenBee_Queen_Stab").WithVolume(0.25f); 
                item.shoot = mod.ProjectileType("RealizedHornet");
            }
            return true;
        }

        public override void AddRecipes() 
		{
		}
	}
}