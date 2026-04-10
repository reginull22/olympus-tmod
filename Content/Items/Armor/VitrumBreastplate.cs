using olympus.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace olympus.Content.Items.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Body)]
    public class VitrumBreastplate : ModItem
    {

        public static LocalizedText SetBonusText { get; private set; }

        public override void SetDefaults()
        {
            Item.width = 15; // Width of the item
            Item.height = 10; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = 3; // The rarity of the item
            Item.defense = 5; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
            player.manaCost -= 0.10f;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<KerauniteBar>(12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}