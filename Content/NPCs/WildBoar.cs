using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using olympus.Content.Items.Materials;
using Terraria.GameContent.ItemDropRules;

namespace olympus.Content.NPCs
{

    public class WildBoar : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 18;
            NPC.damage = 10;
            NPC.lifeMax = 60;
            NPC.defense = 0;
            NPC.knockBackResist = 0.25f;
            NPC.value = 3300;
            NPC.HitSound = SoundID.Zombie39;
            NPC.DeathSound = SoundID.Zombie38;
            NPC.aiStyle = NPCAIStyleID.Unicorn;
            AIType = NPCID.Wolf;


        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ItemID.Leather, 10, 1, 2, 9));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<BoarTusk>(), 20, 1, 1, 7));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneForest && Main.dayTime)
            {
                return SpawnCondition.OverworldDaySlime.Chance * 0.35f;
            }
            return 0f;
        }

        public override void AI()
        {
            base.AI();
            NPC.spriteDirection = NPC.direction;
        }
    }
}