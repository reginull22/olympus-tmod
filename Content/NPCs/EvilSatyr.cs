//using olympus.Content.Projectiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using olympus.Content.Projectiles;

/*TODO
 * 
 * Sprite
 * 
 * AI
 * 
 * Banner
 * 
 * Bestiary
 * 
 */

namespace olympus.Content.NPCs
{

    public class EvilSatyr : ModNPC
    {

        public float VineTimer
        {
            get => NPC.localAI[0];
            set => NPC.localAI[0] = value;
        }

        public int Variant
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 48;
            NPC.damage = 15;
            NPC.defense = 8;
            NPC.lifeMax = 400; //want it to be tankier as it would be a rare enemy (probably to be changed)
            NPC.HitSound = SoundID.NPCHit1; //to be changed
            NPC.DeathSound = SoundID.NPCDeath1; //to be changed;
            NPC.value = 1300f; //rarer enemy, drops 1g3s so its worth
            NPC.knockBackResist = 0.5f; //will have to fiddle with this
            NPC.aiStyle = NPCAIStyleID.Fighter; //For custom ai
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;

            //Banner = ?; Don't have banner set up
            //BannerItem = ?; ^
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if ((spawnInfo.Player.ZoneForest && !Main.dayTime) && !NPC.AnyNPCs(Type))
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.2f;
            }
            if ((spawnInfo.Player.ZoneCorrupt || spawnInfo.Player.ZoneCrimson) && !NPC.AnyNPCs(Type))
            {
                return 0.2f;
            }

            return 0f;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Variant == 0)
                {
                    if ((player.ZoneForest && !WorldGen.crimson) || player.ZoneCorrupt)
                        Variant = 1;
                    else if ((player.ZoneForest && WorldGen.crimson) || player.ZoneCrimson)
                        Variant = 2;
                    else if (player.ZoneHallow)
                        Variant = 3;
                    else
                        Variant = WorldGen.crimson ? 2 : 1;

                    NPC.netUpdate = true;
                }
            }

            VineTimer--;

            if (VineTimer <= 0 && Math.Abs(NPC.velocity.X) > 0.5f && NPC.collideY)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom - new Vector2(0, 8), Vector2.Zero, ModContent.ProjectileType<SatyrVine>(), 10, 0, Main.myPlayer);
                VineTimer = 30;
            }
        }
    }
}