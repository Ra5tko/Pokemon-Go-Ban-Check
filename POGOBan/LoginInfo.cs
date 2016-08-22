using POGOLib.Pokemon.Data;

namespace PGOBan
{
    public class LoginInfo
    {
        public LoginProvider Provider { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
