using CommandLine;
using CommandLine.Text;

namespace POGOBan
{
    public class Options
    {
        [OptionArray('t', "provider", Required = true, HelpText = "Login provider for Pokémon GO, use Google for Google login and PTC for Pokémon Trainer Club login.")]
        public string[] Provider { get; set; }

        [OptionArray('u', "username", Required = true, HelpText = "Username for your Pokémon GO account. For multiple accounts separate each username with a space.")]
        public string[] Username { get; set; }

        [OptionArray('p', "password", Required = true, HelpText = "Password for your Pokémon GO account. For multiple accounts separate each password with a space.")]
        public string[] Password { get; set; }

        [Option('x', "latitude", HelpText = "Use if you want to set the latitude.", DefaultValue = 40.7127837)]
        public double Latitude { get; set; }

        [Option('y', "longitude", HelpText = "Use if you want to set the longitude.", DefaultValue = -74.005941)]
        public double Longitude { get; set; }

        [Option('a', "all", HelpText = "Use if you want to set the same Login provider for all accounts.", DefaultValue = false)]
        public bool All { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
