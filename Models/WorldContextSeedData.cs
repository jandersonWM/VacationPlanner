using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldContextSeedData
    {
        private WorldContext _context;
        private UserManager<WorldUser> _userMgr;

        public WorldContextSeedData(WorldContext context, UserManager<WorldUser> userMgr)
        {
            _context = context;
            _userMgr = userMgr;
        }

        public async Task EnsureSeedData()
        {
            if (await _userMgr.FindByEmailAsync("sam.hastings@theworld.com") == null)
            {
                var user = new WorldUser()
                {
                    UserName = "samhastings",
                    Email = "sam.hastings@theworld.com"
                };

                await _userMgr.CreateAsync(user, "P@ssw0rd");
            }

            if (!_context.Trips.Any())
            {
                var usTrip = new Trip()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "US Trip",
                    UserName = "samhastings",
                    Stops = new List<Stop>()
                    {
                        new Stop() {Name = "Philadelphia, PA", Arrival = new DateTime(2018, 3, 1), Latitude = 7.1, Longitude = 5.2, Order = 1 },
                        new Stop() {Name = "Pittsburgh, PA", Arrival = new DateTime(2018, 3, 4), Latitude = 6.1, Longitude = 10.2, Order = 2 }
                    }
                };

                _context.Trips.Add(usTrip);
                _context.Stops.AddRange(usTrip.Stops);

                var FranceTrip = new Trip()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "France Trip",
                    UserName = "samhastings",
                    Stops = new List<Stop>()
                    {
                        new Stop() {Name = "Paris", Arrival = new DateTime(2018, 7, 1), Latitude = 7.1, Longitude = 5.2, Order = 1 },
                        new Stop() {Name = "Marseille", Arrival = new DateTime(2018, 7, 4), Latitude = 6.1, Longitude = 10.2, Order = 2 }
                    }
                };

                _context.Trips.Add(FranceTrip);
                _context.Stops.AddRange(FranceTrip.Stops);

                await _context.SaveChangesAsync();
            }
        }
    }
}
