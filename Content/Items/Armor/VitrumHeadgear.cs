using olympus.Content.Items.Materials;
using olympus.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace olympus.Content.Items.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Head)]
    public class VitrumHeadgear : ModItem
    {

        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {

            SetBonusText = this.GetLocalization("SetBonus");
        }

        public override void SetDefaults()
        {
            Item.width = 12; // Width of the item
            Item.height = 8; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = 3; // The rarity of the item
            Item.defense = 4; // The amount of defense the item will give when equipped
        }

        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<VitrumBreastplate>() && legs.type == ModContent.ItemType<VitrumGreaves>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value; // This is the setbonus tooltip: "Increases dealt damage by 20%"
            player.GetDamage(DamageClass.Generic) += 5f / 100f; // Increase dealt damage for all weapon classes by 5%
            player.GetModPlayer<OlympusPlayer>().isVitrumSet = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 40;
            player.GetCritChance(DamageClass.Generic) += 4;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<KerauniteBar>(6)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}