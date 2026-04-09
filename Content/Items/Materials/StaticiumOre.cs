using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace olympus.Content.Items.Materials
{
    public class StaticiumOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.StaticiumOre>());
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 10, 0);
        }
    }
}