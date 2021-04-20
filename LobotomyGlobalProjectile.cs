using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace LobotomyCorp
{
	public class LobotomyGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public byte Lament = 0;

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (Lament > 0 && LobotomyCorp.LamentValid(target, projectile) && target.CanBeChasedBy(projectile))
            {
                Projectile.NewProjectile(Main.player[projectile.owner].Center, new Vector2(6, 0).RotateRandom(6.28f), mod.ProjectileType("Kaleidoscope"), projectile.damage, projectile.knockBack, projectile.owner, target.whoAmI);
                Terraria.Audio.LegacySoundStyle ding = null;
                switch (Lament)
                {
                    case 1:
                        ding = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/ButterFlyMan_StongAtk_Black").WithVolume(0.5f);
                        break;
                    default:
                        ding = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/ButterFlyMan_StongAtk_White").WithVolume(0.5f);
                        break;
                }
                Main.PlaySound(ding, target.Center);
            }
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Lament > 0 && LobotomyCorp.LamentValid(target, projectile) && target.CanBeChasedBy(projectile))
            {
                damage = (int)(damage * 1.15f);
            }
        }
    }
}