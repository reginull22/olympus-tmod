using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace olympus.Content.Items.Materials
{
    public class KerauniteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 0, 50, 0);
        }
    }
}