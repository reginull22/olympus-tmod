using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace olympus.Content.Projectiles
{
    public class VitrumSceptreCone : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 96;
            Projectile.height = 96;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.timeLeft = 20;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 direction = Vector2.Normalize(Main.MouseWorld - player.Center);
            Vector2 offset = direction * 59f;
            Projectile.Center = player.Center + offset;
            Projectile.rotation = direction.ToRotation();
        }
    }
}