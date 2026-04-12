//using olympus.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

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
            Main.npcFrameCount[Type] = 13;
        }

        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 64;
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

        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 0;
            bool shouldLoop = false;

            switch (CurrentState)
            {
                case 0:
                    startFrame = 0;
                    finalFrame = 5;
                    shouldLoop = true;
                    break;
                case 1:
                    startFrame = 6;
                    finalFrame = 8;
                    break;
                case 2:
                    startFrame = 0;
                    finalFrame = 5;
                    shouldLoop = true;
                    break;
                case 3:
                    startFrame = 9;
                    finalFrame = 12;
                    break;
            }

            if (NPC.frame.Y < startFrame * frameHeight || NPC.frame.Y > finalFrame * frameHeight)
            {
                NPC.frame.Y = startFrame * frameHeight;
            }

            int frameSpeed = 10;
            NPC.frameCounter++;
            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (shouldLoop)
                {
                    if (NPC.frame.Y > finalFrame * frameHeight)
                    {
                        NPC.frame.Y = startFrame * frameHeight;
                    }
                }
                else
                {
                    if (NPC.frame.Y > finalFrame * frameHeight)
                    {
                        NPC.frame.Y = finalFrame * frameHeight;
                    }
                }
            }
        }

        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];
            float distance = NPC.Distance(player.Center);
            NPC.direction = NPC.Center.X < player.Center.X ? 1 : -1;

            Main.NewText($"State: {CurrentState}, Distance: {distance}");

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Variant == 0)
                {
                    if ((player.ZoneForest && !WorldGen.crimson) || player.ZoneCorrupt)
                        Variant = 1;
                    else if ((player.ZoneForest && WorldGen.crimson) || player.ZoneCrimson)
                        Variant = 2;
                    else
                        Variant = WorldGen.crimson ? 2 : 1;

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
                    CurrentState = 2;
                else if (distance < 250f)
                    CurrentState = 1;
                else
                    CurrentState = 0;
            }

            switch (CurrentState)
            {
                case 0: DoWalk(player); break;
                case 1: DoRangedAttack(player); break;
                case 2: DoWalkAway(player); break;
                case 3: DoStomp(player); break;
            }
        }

        private void DoWalk(Player player)
        {
            NPC.velocity.X = NPC.direction * 2f;
            if (NPC.collideX && NPC.velocity.Y == 0)
            {
                NPC.velocity.Y = -6f;
            }
        }
        private void DoWalkAway(Player player)
        {
            NPC.velocity.X = -NPC.direction * 2f;
            if (NPC.collideX && NPC.velocity.Y == 0)
            {
                NPC.velocity.Y = -6f;
            }
        }

        private void DoStomp(Player player)
        {
            NPC.velocity.X = 0f;
            if(Timer >= 60)
            {
                Timer = 0;
                CurrentState = 2;
            }
            else if (Timer == 45)
            {
                //spawn shockwave
            }
            Timer++;
        }

        private void DoRangedAttack(Player player)
        {
            NPC.velocity.X = 0f;
            if (Timer > 120) { Timer = 29; }

            if (Timer > 29 && (Timer - 30) % 15 == 0)
            {
                int shotNumber = (int)((Timer - 30) / 15);
                if (shotNumber % 2 == 0)
                {
                    //shoot cos
                }
                else
                {
                    //shoot sin
                }
                   
            }
            Timer++;
        }
    }
}