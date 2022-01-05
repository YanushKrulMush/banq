using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broker.Domain
{
    public record RegisterRequestDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

    public record RegisterDto
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string username { get; set; }

        public IEnumerable<Credentials> credentials { get; set; }

        public bool enabled { get; set; }
    }

    public record Credentials
    {
        public string value { get; set; }

        public string type { get; set; } = "password";

        public bool temporary { get; set; } = false;

    }


}
