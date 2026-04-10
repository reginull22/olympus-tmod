using Microsoft.Xna.Framework;
using olympus.Content.Items.Materials;
using olympus.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace olympus.Content.Items.Weapons.Magic
{

    public class VitrumSceptre : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Magic;
            Item.width = 21;
            Item.height = 21;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 10000;
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<VitrumSceptreCone>();
            Item.mana = 10;
            Item.shootSpeed = 9f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<KerauniteBar>(7)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}