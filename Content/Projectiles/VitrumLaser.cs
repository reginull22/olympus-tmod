using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace olympus.Content.Projectiles
{
    public class VitrumLaser : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 200;
            Projectile.penetrate = 15;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.extraUpdates = 100;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.DrillContainmentUnit);
            Main.dust[dust].noGravity = true;
        }
    }
}