using Microsoft.Xna.Framework;
using olympus.Content.Projectiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace olympus.Common.Players
{

    public class OlympusPlayer : ModPlayer
    {

        public bool isVitrumSet = false;
        public int shatterCooldown = 0;


        public override void ResetEffects()
        {
            isVitrumSet = false;
            if (shatterCooldown > 0)
            {
                shatterCooldown--;
            }
        }

        int shardCount = 8;
        public override void OnHurt(Player.HurtInfo info)
        {

            if (isVitrumSet == true && shatterCooldown == 0)
            {
                for (int i = 0; i < shardCount; i++)
                {
                    float angle = MathHelper.TwoPi / shardCount * i;
                    Projectile.NewProjectile(Player.GetSource_OnHurt(info.DamageSource), Player.Center, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 18f, ModContent.ProjectileType<VitrumArmorShard>(), 19, 0f, Player.whoAmI);
                }
                shatterCooldown = 900;
            }
        }
    }
}