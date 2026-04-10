using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using olympus.Content.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace olympus.Content.Items.Weapons.Ranged
{

    public class VitrumRailgun : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 28;
            Item.height = 8;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.channel = true;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 10000;
            Item.rare = 3;
            Item.UseSound = null;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<VitrumRailgunProjectile>();
            Item.shootSpeed = 34f;
            Item.useAmmo = AmmoID.None;
            Item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Materials.KerauniteBar>(), 7);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ModContent.ProjectileType<VitrumRailgunProjectile>())
            {
                return true;
            }
            return false;
        }
        /*
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7f, 2f);
        }
        */

        public int chargeLevel = 0;
        public float chargeTimer = 0;
        public override void HoldItem(Player player)
        {
            if (chargeTimer < 0)
            {
                chargeTimer++;
            }
            if (Main.mouseLeft)
            {
                chargeTimer++;
                if(chargeTimer%60 == 0)
                {
                    if (chargeLevel < 5)
                    {
                        chargeLevel++;
                        SoundEngine.PlaySound(SoundID.Item93);
                        for (int i = 0; i < chargeLevel; i++)
                        {
                            int dust = Dust.NewDust(player.Center - new Vector2(14, 4), 28, 8, DustID.Electric);
                            Main.dust[dust].noGravity = true;
                        }
                    }
                    else if (chargeLevel == 5)
                    {
                        SoundEngine.PlaySound(SoundID.Item93);
                        SoundEngine.PlaySound(SoundID.Item92);
                        for (int i = 0; i < chargeLevel*2; i++)
                        {
                            int dust = Dust.NewDust(player.Center - new Vector2(14, 4), 28, 8, DustID.Electric);
                            Main.dust[dust].noGravity = true;
                        }
                        Vector2 direction = Vector2.Normalize(Main.MouseWorld - player.Center);
                        Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, direction * 5f, ModContent.ProjectileType<VitrumLaser>(), 75, 8f, player.whoAmI);
                        chargeLevel = 0;
                        chargeTimer = -30;
                    }
                }
            }
            else
            {
                if (chargeTimer > 0)
                {
                    Vector2 direction = Vector2.Normalize(Main.MouseWorld - player.Center);
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, (direction * 2.5f) * (1f + chargeLevel * 0.40f), ModContent.ProjectileType<VitrumBullet>(), (int)(Item.damage * (1f + chargeLevel * 0.40f)), 3f, player.whoAmI);
                    chargeLevel = 0;
                    chargeTimer = -30;
                }
            }
        }
    }
}