using System;
using ActionCommandGame.Model;
using ActionCommandGame.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ActionCommandGame.Repository
{
    public class ActionButtonGameUiDbContext : DbContext
    {
        public ActionButtonGameUiDbContext(DbContextOptions<ActionButtonGameUiDbContext> options) : base(options)
        {
            
        }
        public DbSet<PositiveGameEvent> PositiveGameEvents { get; set; }
        public DbSet<NegativeGameEvent> NegativeGameEvents { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerItem> PlayerItems { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureRelationships();
            
            modelBuilder.Entity<Player>().HasData(
                new Player { Name= "NewPlayer", Money = 0, Experience = 0, Id = Guid.NewGuid()},
                new Player { Name = "John Doe", Money = 100, Id = Guid.NewGuid()},
                new Player { Name = "John Francks", Money = 100000, Experience = 2000, Id = Guid.NewGuid() },
                new Player { Name = "Luc Doleman", Money = 500, Experience = 5, Id = Guid.NewGuid() },
                new Player { Name = "Emilio Fratilleci", Money = 12345, Experience = 200, Id = Guid.NewGuid() }
                );
            
            modelBuilder.Entity<PositiveGameEvent>()
                .HasData(
                    new PositiveGameEvent { Id = Guid.NewGuid(), Name = "Nothing but boring rocks", Probability = 1000 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "The biggest Opal you ever saw.", Description = "It slips out of your hands and rolls inside a crack in the floor. It is out of reach.", Probability = 500 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Sand, dirt and dust", Probability = 1000 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "A piece of empty paper", Description = "You hold it to the light and warm it up to reveal secret texts, but it remains empty.", Probability = 1000 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "A small water stream", Description = "The water flows around your feet and creates a dirty puddle.", Probability = 1000 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Junk", Money = 1, Experience = 1, Probability = 2000 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Murphy's idea bin", Money = 1, Experience = 1, Probability = 300 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Donald's book of excuses", Money = 1, Experience = 1, Probability = 300 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Children's Treasure Map", Money = 1, Experience = 1, Probability = 300 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Trinket", Money = 5, Experience = 3, Probability = 1000 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Old Tool", Money = 10, Experience = 5, Probability = 800 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Old Equipment", Money = 10, Experience = 5, Probability = 800 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Ornate Shell", Money = 10, Experience = 5, Probability = 800 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Fossil", Money = 12, Experience = 6, Probability = 700 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Cave Shroom", Money = 20, Experience = 8, Probability = 650 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Artifact", Money = 30, Experience = 10, Probability = 500 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Scrap Metal", Money = 50, Experience = 13, Probability = 400 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Jewelry", Money = 60, Experience = 15, Probability = 400 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Peculiar Mask", Money = 100, Experience = 40, Probability = 350 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Quartz Geode", Money = 140, Experience = 50, Probability = 300 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Ancient Weapon", Money = 160, Experience = 80, Probability = 300 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Ancient Instrument", Money = 160, Experience = 80, Probability = 300 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Ancient Texts", Money = 180, Experience = 80, Probability = 300 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Gemstone", Money = 300, Experience = 100, Probability = 110 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Mysterious Potion", Money = 300, Experience = 100, Probability = 80 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Meteorite", Money = 400, Experience = 150, Probability = 200 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Ancient Bust", Money = 500, Experience = 150, Probability = 150 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Buried Treasure", Money = 1000, Experience = 200, Probability = 100 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Alien DNA", Money = 60000, Experience = 1500, Probability = 5 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Rare Collector's Item", Money = 3000, Experience = 400, Probability = 30 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Pure Gold", Money = 2000, Experience = 350, Probability = 30 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Safe Deposit Box Key", Money = 20000, Experience = 1000, Probability = 10 },
                    new PositiveGameEvent { Id = Guid.NewGuid(),Name = "Advanced Bio Tech", Money = 30000, Experience = 1500, Probability = 10 }
                );
            modelBuilder.Entity<NegativeGameEvent>()
                .HasData(
                    new NegativeGameEvent
                    {
                        Id = Guid.NewGuid(),
                        Name = "Rockfall",
                        Description = "As you are mining, the cave walls rumble and rocks tumble down on you",
                        DefenseWithGearDescription = "Your mining gear allows you and your tools to escape unscathed",
                        DefenseWithoutGearDescription = "You try to cover your face but the rocks are too heavy. That hurt!",
                        DefenseLoss = 2,
                        Probability = 100
                    },
                    new NegativeGameEvent
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cave Rat",
                        Description = "As you are mining, you feel something scurry between your feet!",
                        DefenseWithGearDescription = "It tries to bite you, but your mining gear keeps the rat's teeth from sinking in.",
                        DefenseWithoutGearDescription = "It tries to bite you and nicks you in the ankles. It already starts to glow dangerously.",
                        DefenseLoss = 3,
                        Probability = 50
                    },
                    new NegativeGameEvent
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sinkhole",
                        Description = "As you are mining, the ground suddenly gives way and you fall down into a chasm!",
                        DefenseWithGearDescription = "Your gear grants a safe landing, protecting you and your pickaxe.",
                        DefenseWithoutGearDescription = "You tumble down the dark hole and take a really bad landing. That hurt!",
                        DefenseLoss = 2,
                        Probability = 100
                    },
                    new NegativeGameEvent
                    {
                        Id = Guid.NewGuid(),
                        Name = "Ancient Bacteria",
                        Description = "As you are mining, you uncover a green slime oozing from the cracks!",
                        DefenseWithGearDescription = "Your gear barely covers you from the noxious goop. You are safe.",
                        DefenseWithoutGearDescription = "The slime covers your hands and arms and starts biting through your flesh. This hurts!",
                        DefenseLoss = 3,
                        Probability = 50
                    });
            modelBuilder.Entity<Item>()
                .HasData(
                    // Generate Attack Items
                    new Item { Id = Guid.NewGuid(),Name = "Basic Pickaxe", Attack = 50, Price = 50 },
                    new Item { Id = Guid.NewGuid(),Name = "Enhanced Pick", Attack = 300, Price = 300 },
                    new Item { Id = Guid.NewGuid(),Name = "Turbo Pick", Attack = 500, Price = 500 },
                    new Item { Id = Guid.NewGuid(),Name = "Mithril Warpick", Attack = 5000, Price = 15000 },
                    new Item { Id = Guid.NewGuid(),Name = "Thor's Hammer", Attack = 50, Price = 1000000 },
                    // Generate Defence Items
                    new Item { Id = Guid.NewGuid(),Name = "Torn Clothes", Defense = 20, Price = 20 },
                    new Item { Id = Guid.NewGuid(),Name = "Hardened Leather Gear", Defense = 150, Price = 200 },
                    new Item { Id = Guid.NewGuid(),Name = "Iron plated Armor", Defense = 500, Price = 1000 },
                    new Item { Id = Guid.NewGuid(),Name = "Rock Shield", Defense = 2000, Price = 10000 },
                    new Item { Id = Guid.NewGuid(),Name = "Emerald Shield", Defense = 2000, Price = 10000 },
                    new Item { Id = Guid.NewGuid(),Name = "Diamond Shield", Defense = 20000, Price = 10000 },
                    // Generate Food Items
                    new Item { Id = Guid.NewGuid(),Name = "Apple", ActionCooldownSeconds = 50, Fuel = 4, Price = 8 },
                    new Item { Id = Guid.NewGuid(),Name = "Energy Bar", ActionCooldownSeconds = 45, Fuel = 5, Price = 10 },
                    new Item { Id = Guid.NewGuid(),Name = "Field Rations", ActionCooldownSeconds = 30, Fuel = 30, Price = 300 },
                    new Item { Id = Guid.NewGuid(),Name = "Abbye cheese", ActionCooldownSeconds = 25, Fuel = 100, Price = 500 },
                    new Item { Id = Guid.NewGuid(),Name = "Abbye Beer", ActionCooldownSeconds = 25, Fuel = 100, Price = 500 },
                    new Item { Id = Guid.NewGuid(),Name = "Celestial Burrito", ActionCooldownSeconds = 15, Fuel = 500, Price = 10000 },
#if DEBUG
                    new Item { Id = Guid.NewGuid(),Name = "Developer Food", ActionCooldownSeconds = 1, Fuel = 1000, Price = 1 },
#endif
                    // Generate Decorative Items
                    new Item { Id = Guid.NewGuid(),Name = "Balloon", Description = "Does nothing. Do you feel special now?", Price = 10 },
                    new Item { Id = Guid.NewGuid(),Name = "Blue Medal", Description = "For those who cannot afford the Crown of Flexing.", Price = 100000 },
                    new Item { Id = Guid.NewGuid(),Name = "Crown of Flexing", Description = "Yes, show everyone how much money you are willing to spend on something useless!", Price = 500000 }
                );
            

            base.OnModelCreating(modelBuilder);
        }
    }
}