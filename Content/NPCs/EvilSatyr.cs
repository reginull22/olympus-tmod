using olympus.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

/*TODO:
 * 
 * SpawnConditions override
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

        public int CurrentState
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public float Timer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public int Variant
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 17;
        }

        public override void SetDefaults()
        {
            //NPC.width = ?; Don't have a sprite yet
            //NPC.height = ?; Don't have a sprite yet
            NPC.damage = 15;
            NPC.defense = 8;
            NPC.lifeMax = 400; //want it to be tankier as it would be a rare enemy (probably to be changed)
            NPC.HitSound = SoundID.NPCHit1; //to be changed
            NPC.DeathSound = SoundID.NPCDeath1; //to be changed;
            NPC.value = 1300f; //rarer enemy, drops 1g3s so its worth
            NPC.knockBackResist = 0.5f; //will have to fiddle with this
            NPC.aiStyle = -1; //For custom ai

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
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];
            float distance = NPC.Distance(player.Center);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Variant == 0)
                {
                    if ((player.ZoneForest && !WorldGen.crimson) || player.ZoneCorrupt)
                    {
                        Variant = 1;
                    }
                    else if ((player.ZoneForest && WorldGen.crimson) || player.ZoneCrimson)
                    {
                        Variant = 2;
                    }
                    else
                    {
                        Variant = WorldGen.crimson ? 2 : 1;
                    }

                    NPC.netUpdate = true;
                }
            }
            if (distance < 64f)
            {
                CurrentState = 3;
            }
            else if (CurrentState != 3 && CurrentState != 1)
            {
                if (distance < 170f)
                {
                    CurrentState = 2;
                }
                else if (distance < 250f)
                {
                    CurrentState = 1;
                }
                else
                {
                    CurrentState = 0;
                }
            }
        }
    }
}