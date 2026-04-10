using Microsoft.Xna.Framework;
using olympus.Content.Items.Materials;
using olympus.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace olympus.Content.Items.Weapons.Melee
{
    public class VitrumSabre : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.regStarterBag.hjson file.
        public int hitCount = 0;
        public int maxStacks = 10;
        public int weaponSpeed = 30;
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Melee;
            Item.width = 22;
            Item.height = 32;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = 10000;
            Item.rare = 3;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KerauniteBar>(), 7);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        // this fires every time the sword hits an enemy
        //public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        public override bool? UseItem(Player player)
        {
            if (hitCount < maxStacks)
            {
                hitCount++;
            }
            else if (hitCount >= maxStacks)
            {
                Vector2 mouseDirection = Main.MouseWorld - player.Center;
                mouseDirection.Normalize();

                float[] angles = { -0.2f, -0f, 0f, 0.2f };
                float[] speeds = {12f, 14f, 10f, 12f};

                for (int i = 0; i < angles.Length; i++)
                {
                    Vector2 velocity = mouseDirection.RotatedBy(angles[i]) * speeds[i];
                    Projectile.NewProjectile(
                        player.GetSource_ItemUse(Item),
                        player.Center,
                        velocity,
                        ModContent.ProjectileType<VitrumShard>(),
                        Item.damage / 2,
                        Item.knockBack,
                        player.whoAmI
                    );
                }
                hitCount = 0;
            }
            return true;
        } 

        public override float UseSpeedMultiplier(Player player)
        {
            float bonus = 1f + (hitCount * 0.175f);
            return bonus;
        }

        public override bool CanUseItem(Player player)
        {
            foreach (Projectile p in Main.projectile)
            { 
                if (p.active && p.owner == player.whoAmI &&
                    (p.type == ModContent.ProjectileType<VitrumShard>() ||
                    p.type == ModContent.ProjectileType<VitrumShardReturn>() ||
                    p.type == ModContent.ProjectileType<VitrumExplosion>()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}