using System;

namespace Doctrina.Domain.Security
{
    public class BasicAuth
    {
        public Guid Id { get; set; }
        public string AllowedHosts { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}