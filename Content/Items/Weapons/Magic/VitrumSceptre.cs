using Microsoft.Xna.Framework;
using olympus.Content.Items.Materials;
using olympus.Content.Projectiles;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
            Item.damage = 5;
            Item.DamageType = DamageClass.Magic;
            Item.width = 21;
            Item.height = 21;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 10000;
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<VitrumSceptreRay>();
            Item.mana = 7;
            Item.shootSpeed = 6f;
            Item.UseSound = SoundID.Item91;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<KerauniteBar>(7)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public List<int> hitEnemies = new List<int>();
        public int rayCount = 8;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            hitEnemies.Clear();
            float angle = 0f;
            float distance = 45f;
            Vector2 modifiedVelocity = new Vector2();
            if (type == ModContent.ProjectileType<VitrumSceptreRay>())
            {
                for(int i = 0; i < rayCount; i++)
                {
                    angle = -MathHelper.PiOver4 + (MathHelper.PiOver2 / (rayCount - 1)) * i;
                    modifiedVelocity = velocity.RotatedBy(angle);
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center + Vector2.Normalize(velocity) * distance, modifiedVelocity, ModContent.ProjectileType<VitrumSceptreRay>(), Item.damage, Item.knockBack, player.whoAmI);
                }
            }

            return false;
        }
    }
}