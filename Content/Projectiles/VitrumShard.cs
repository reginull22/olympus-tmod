using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace olympus.Content.Projectiles //where it's stored. Replace Mod with the name of your mod. This file is stored in the folder \Mod Sources\(mod name, folder can't have spaces)\Projectiles.
{
    public class VitrumShard : ModProjectile //the class of the projectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 5; //sprite is 2 pixels wide
            Projectile.height = 9; //sprite is 20 pixels tall
            Projectile.aiStyle = 0; //projectile moves in a straight line
            Projectile.friendly = true; //player projectile
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 45;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.velocity *= 0.885f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.4f);
        }
    }
}