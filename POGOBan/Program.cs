using Google.Protobuf;
using PGOBan;
using POGOLib.Net;
using POGOLib.Net.Authentication;
using POGOLib.Pokemon.Data;
using System;
using System.Threading;

namespace POGOBan
{
    class Program
    {
        static void Main(string[] args)
        {
            string output = "";
            Options options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                try
                {
                    for (int i = 0; i < options.Username.Length; i++)
                    {
                        LoginInfo login = new LoginInfo();
                        if (options.All == true)
                        {
                            if (options.Provider[0] == "Google")
                            {
                                login.Provider = LoginProvider.GoogleAuth;
                            }
                            else if (options.Provider[0] == "PTC")
                            {
                                login.Provider = LoginProvider.PokemonTrainerClub;
                            }
                            else
                            {
                                Console.WriteLine("Invalid provider. Use -h or --help for help.");
                                break;
                            }
                        }
                        else
                        {
                            if (options.Provider[i] == "Google")
                            {
                                login.Provider = LoginProvider.GoogleAuth;
                            }
                            else if (options.Provider[i] == "PTC")
                            {
                                login.Provider = LoginProvider.PokemonTrainerClub;
                            }
                            else
                            {
                                Console.WriteLine("Invalid provider. Use -h or --help for help.");
                                break;
                            }
                        }
                        login.Username = options.Username[i];
                        login.Password = options.Password[i];
                        login.Latitude = options.Latitude;
                        login.Longitude = options.Longitude;
                        output += Check(login) + ";";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Parameter missing. Use -h or --help for help.");
                    return;
                }
            }
            Console.WriteLine(output);
        }

        private static string Check(LoginInfo login)
        {
            Session session = null;
            try
            {
                session = Login.GetSession(login.Username, login.Password, login.Provider, login.Latitude, login.Longitude);
                session.Startup();
            }
            catch (Exception ex)
            {
                return "Incorrect Login";
            }
            try
            {
                Thread.Sleep(2500);
                ByteString GlobalSettings = session.GlobalSettings.ToByteString();
                return "Not Banned";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Value cannot be null."))
                {
                    session.Dispose();
                    return "Banned";
                }
                else
                {
                    session.Dispose();
                    return "Error: " + ex.Message;
                }
            }
        }
    }
}
