using Doctrina.Application.Interfaces;
using Doctrina.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;

namespace Doctrina.Persistence
{
    public class DoctrinaInitializer : IDoctrinaInitializer
    {
        private readonly DoctrinaDbContext _context;
        private readonly ILogger<DoctrinaInitializer> _logger;

        public DoctrinaInitializer(IDoctrinaDbContext context, ILogger<DoctrinaInitializer> logger)
        {
            _context = (DoctrinaDbContext)context;
            _logger = logger;
        }

        public void SeedEverything(CancellationToken cancellationToken = default)
        {
            _context.Database.EnsureCreated();
            SeedAdminUser(cancellationToken);
        }

        public async void SeedAdminUser(CancellationToken cancellationToken)
        {
            try
            {
                var user = new DoctrinaUser
                {
                    UserName = "Admin@bitflipping.net",
                    NormalizedUserName = "admin@bitflipping.net",
                    Email = "admin@bitflipping.net",
                    NormalizedEmail = "admin@bitflipping.net",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                if (!_context.Roles.Any(r => r.Name == "admin"))
                {
                    var role = new DoctrinaRole { Name = "admin", NormalizedName = "admin" };
                    using (var roleStore = new RoleStore<DoctrinaRole>(_context))
                    {
                        await roleStore.CreateAsync(role, cancellationToken);
                    }
                    _logger.LogDebug("Default role: {UserName} was seeded.", role.Name);
                }

                if (!_context.Users.Any())
                {
                    var password = new PasswordHasher<DoctrinaUser>();
                    var hashed = password.HashPassword(user, "password");
                    user.PasswordHash = hashed;
                    using (var userStore = new UserStore<DoctrinaUser, DoctrinaRole, DoctrinaDbContext>(_context))
                    {
                        var result = await userStore.CreateAsync(user, cancellationToken);
                        if (result.Succeeded)
                        {
                            await userStore.AddToRoleAsync(user, "admin", cancellationToken);
                            _logger.LogDebug("Default user: {UserName} was seeded.", user.UserName);
                        }
                        else
                        {
                            _logger.LogError("Failed to seed default user.");
                        }
                    }

                }

                await _context.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to seed default user.");
                throw;
            }
        }
    }
}
