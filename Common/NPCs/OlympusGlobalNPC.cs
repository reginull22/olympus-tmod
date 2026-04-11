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
        public int fulgurShardCooldown = 0;

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

            if (fulgurShardCooldown > 0)
            {
                fulgurShardCooldown--;
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if ((projectile.minion == true || ProjectileID.Sets.MinionShot[projectile.type]) && npc.HasBuff(ModContent.BuffType<FulgurChainDebuff>()) && fulgurShardCooldown == 0)
            {
                Vector2 spawnPos = npc.Center + Main.rand.NextVector2Circular(20f, 20f);
                Projectile.NewProjectile(projectile.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<VitrumArmorShard>(), (int)(projectile.damage + 17), 0, projectile.owner);
                fulgurShardCooldown = 25;
            }
        }
    }
}