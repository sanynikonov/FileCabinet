using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DAL
{
    public class AudioLibraryContext : IdentityDbContext
    {
        //public virtual DbSet<User> AppUsers { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<SongsContainer> SongsContainers { get; set; }
        public virtual DbSet<Commentary> Commentaries { get; set; }

        public AudioLibraryContext() : base("name=AudioLibraryDbConnection")
        {
        }

        static AudioLibraryContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserLogin>()
            //    .HasRequired(p => p.UserProfile).WithRequiredDependent(p => p.UserLogin);

            modelBuilder.Entity<SongsContainer>()
                .HasMany(p => p.Genres)
                .WithMany(p => p.SongsContainers);

            modelBuilder.Entity<Song>()
                .HasMany(p => p.Playlists)
                .WithMany(p => p.Songs);

            modelBuilder.Entity<User>()
                .HasMany(p => p.LikedSongs)
                .WithMany(p => p.Likes);

            modelBuilder.Entity<User>()
                .HasMany(p => p.Followers)
                .WithMany(p => p.Followings);

            modelBuilder.Entity<User>()
                .HasMany(p => p.Playlists)
                .WithMany(p => p.Likes);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class DbInitializer : CreateDatabaseIfNotExists<AudioLibraryContext>
    {
        protected override void Seed(AudioLibraryContext context)
        {
            Country ukraine = new Country { Name = "Ukraine" };
            Country russia = new Country { Name = "Russia" };
            Country belarus = new Country { Name = "Belarus" };
            Country poland = new Country { Name = "Poland" };
            Country germany = new Country { Name = "Germany" };
            Country lithuania = new Country { Name = "Lithuania" };
            Country austria = new Country { Name = "Austria" };
            Country uk = new Country { Name = "UK" };
            Country[] countries = { russia, belarus, poland, germany, lithuania }; //ukraine, austria, uk will be added with users

            Genre rock = new Genre { Name = "Rock" };
            Genre pop = new Genre { Name = "Pop" };
            Genre disco = new Genre { Name = "Disco" };
            Genre jazz = new Genre { Name = "Jazz" };
            Genre rocknroll = new Genre { Name = "Rock'n'roll" };
            Genre rnb = new Genre { Name = "R&B" };
            Genre metal = new Genre { Name = "Metal" };
            Genre rap = new Genre { Name = "Rap" };
            Genre[] genres = { rock, pop, disco, jazz, rocknroll, rnb, metal, rap };


            UserRole user = new UserRole { Name = "User", Description = "Can listen to music, create own playlists" };
            UserRole artist = new UserRole { Name = "Artist", Description = "Can post albums with new songs" };
            UserRole admin = new UserRole { Name = "Admin", Description = "Can verify user as artist, delete users, artists, albums, playlists, commentaries" };
            UserRole seniorAdmin = new UserRole { Name = "Senior admin", Description = "Can manage admins" };

            //UserLogin vlad = new UserLogin
            //{
            //    UserName = "vladyslav",
            //    Email = "vladon@gmail.com",
            //    PhoneNumber = "0986666666",
            //};
            //UserLogin sany = new UserLogin
            //{
            //    UserName = "sanynikonov",
            //    Email = "sanynikonov@gmail.com",
            //    PhoneNumber = "0985555555",
            //};
            //UserLogin falco = new UserLogin
            //{
            //    UserName = "falco",
            //    Email = "falco@gmail.com",
            //    PhoneNumber = "0984444444",
            //};
            //UserLogin deeppurple = new UserLogin
            //{
            //    UserName = "deeppurple",
            //    Email = "deeppurple@gmail.com",
            //    PhoneNumber = "0983333333",
            //};
            //UserLogin[] users = { vlad, sany, falco, deeppurple };

            var vladProfile = new User
            {
                FirstName = "Vlad",
                LastName = "Vandam",
                Country = ukraine,
                //UserLogin = vlad,
                UserName = "vladyslav",
                Email = "vladon@gmail.com",
                PhoneNumber = "0986666666",
            };
            var sanyProfile = new User
            {
                FirstName = "Olexandr",
                LastName = "Nikonov",
                //UserLogin = sany,
                Country = ukraine,
                UserName = "sanynikonov",
                Email = "sanynikonov@gmail.com",
                PhoneNumber = "0985555555",
            };
            var falcoProfile = new User
            {
                FirstName = "Johann",
                LastName = "Holzel",
                Country = austria,
                //UserLogin = falco,
                UserName = "falco",
                Email = "falco@gmail.com",
                PhoneNumber = "0984444444",
            };
            var deeppurpleProfile = new User
            {
                FirstName = "Deep Purple",
                LastName = "",
                Country = uk,
                //UserLogin = deeppurple,
                UserName = "deeppurple",
                Email = "deeppurple@gmail.com",
                PhoneNumber = "0983333333",
            };
            User[] userProfiles = { vladProfile, sanyProfile, falcoProfile, deeppurpleProfile };

            using (RoleManager roleManager = new RoleManager(context))
            {
                roleManager.Create(user);
                roleManager.Create(artist);
                roleManager.Create(admin);
                roleManager.Create(seniorAdmin);
            }

            using (UserManager userManager = new UserManager(context))
            {
                userManager.Create(vladProfile, "vladvladvlad");
                userManager.Create(sanyProfile, "sanysanysany");
                userManager.Create(falcoProfile, "amadeus");
                userManager.Create(deeppurpleProfile, "gillandrunk");


                userManager.AddToRoles(vladProfile.Id, user.Name);
                userManager.AddToRoles(sanyProfile.Id, user.Name, admin.Name, seniorAdmin.Name);
                userManager.AddToRoles(falcoProfile.Id, user.Name, artist.Name);
                userManager.AddToRoles(deeppurpleProfile.Id, user.Name, artist.Name);
            }
            


            vladProfile.Followings.Add(deeppurpleProfile);
            sanyProfile.Followings.Add(deeppurpleProfile);
            sanyProfile.Followings.Add(falcoProfile);

            //context.UserProfiles.AddRange(userProfiles);



            context.Genres.AddRange(genres);
            context.Countries.AddRange(countries);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
