using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using olympus.Content.Items.Weapons.Magic;
using System.Security.Policy;
using olympus.Common.NPCs;

namespace olympus.Content.Projectiles
{
    public class VitrumSceptreRay : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 30;
            Projectile.extraUpdates = 30;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 60;
        }

        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.YellowTorch, Vector2.Zero);
            dust.noGravity = true;
            dust.scale = 1.2f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            VitrumSceptre sceptre = player.HeldItem.ModItem as VitrumSceptre;
            if (sceptre != null)
            {
                sceptre.hitEnemies.Add(target.whoAmI);
            }

            OlympusGlobalNPC globalNPC = target.GetGlobalNPC<OlympusGlobalNPC>();
            if (globalNPC.vitrumSceptreStacks <= 12)
            {
                globalNPC.vitrumSceptreStacks++;
            }

            for (int i = 0; i < globalNPC.vitrumSceptreStacks; i++)
            {
                Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Electric);
                dust.color = Color.Lerp(Color.Yellow, Color.White, globalNPC.vitrumSceptreStacks / 12f);
                dust.noGravity = true;
            }
            globalNPC.vitrumSceptreDecayTimer = 60;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            OlympusGlobalNPC globalNPC = target.GetGlobalNPC<OlympusGlobalNPC>();
            modifiers.FlatBonusDamage += (int)(Projectile.damage * (globalNPC.vitrumSceptreStacks * 0.25f));
        }

        public override bool? CanHitNPC(NPC target)
        {
            Player player = Main.player[Projectile.owner];
            VitrumSceptre sceptre = player.HeldItem.ModItem as VitrumSceptre;
            if (sceptre != null && sceptre.hitEnemies.Contains(target.whoAmI))
            {
                return false;
            }
            return null;
        }
    }
}