using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace olympus.Content.Projectiles //where it's stored. Replace Mod with the name of your mod. This file is stored in the folder \Mod Sources\(mod name, folder can't have spaces)\Projectiles.
{
    public class VitrumShardReturn : ModProjectile //the class of the projectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 5; //sprite is 2 pixels wide
            Projectile.height = 9; //sprite is 20 pixels tall
            Projectile.aiStyle = 0; //projectile moves in a straight line
            Projectile.friendly = true; //player projectile
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 direction = player.Center - Projectile.Center;
            direction.Normalize();
            Projectile.velocity = direction * 18f;

            if (Vector2.Distance(Projectile.Center, player.Center) < 20f)
            {
                Projectile.Kill();
            }

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.4f);
        }
    }
}