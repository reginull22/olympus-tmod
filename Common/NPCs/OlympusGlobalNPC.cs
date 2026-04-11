using Microsoft.Xna.Framework;
using olympus.Content.Buffs;
using olympus.Content.Projectiles;
using System;
using System.Reflection.Metadata;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace olympus.Common.NPCs
{

    public class OlympusGlobalNPC : GlobalNPC
    {

        public int vitrumSceptreStacks = 0;
        public int vitrumSceptreDecayTimer = 60;

        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            if (vitrumSceptreStacks > 0)
            {
                if (vitrumSceptreDecayTimer > 0)
                {
                    vitrumSceptreDecayTimer--;
                }
                else
                {
                    vitrumSceptreStacks = 0;
                    vitrumSceptreDecayTimer = 60;
                    SoundEngine.PlaySound(SoundID.Item94, npc.position);
                }
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.minion == true && npc.HasBuff(ModContent.BuffType<FulgurChainDebuff>()))
            {
                Vector2 spawnPos = npc.Center + Main.rand.NextVector2Circular(20f, 20f);
                Projectile.NewProjectile(projectile.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<VitrumArmorShard>(), (int)(projectile.damage * 0.65f), 0, projectile.owner);
            }
        }
    }
}