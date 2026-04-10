using Microsoft.Xna.Framework;
using olympus.Content.Projectiles;
using System;
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
    }
}