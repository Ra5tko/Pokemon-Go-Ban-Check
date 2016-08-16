namespace PokemonGOBanCheck
{
    public class SessionSettings
    {

        public string PtcUsername;
        public string PtcPassword;
        public string GoogleUsername;
        public string GooglePassword;
        public AuthType AuthType;

        public static SessionSettings Default => new SessionSettings();

    }

}
