using olympus.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace olympus.Content.Projectiles
{
    public class SatyrVine : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 8;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 300;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.GetModPlayer<OlympusPlayer>().vineCooldown = 45;
        }

        public override bool CanHitPlayer(Player target)
        {
            if (target.GetModPlayer<OlympusPlayer>().vineCooldown > 0)
            {
                return false;
            }
            return true;
        }
    }
}