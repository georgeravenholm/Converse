using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SabrineServer
{
    class Config
    {
        public string MOTD="Welcome to the server!";
        public int port = 6742;

        private const string motd_path = "MOTD.txt";
        //private const string cfg_path = "config.txt";

        public Config()
        {
            // MOTD
            if (File.Exists(motd_path))
            {
                // 
                FileStream motdstream = new FileStream(motd_path, FileMode.Open);
                MOTD = new StreamReader(motdstream).ReadToEnd();
                motdstream.Close();
            }
            else
            {
                // write default MOTD to file
                Console.WriteLine("[CONFIG] Creating MOTD.txt");
                StreamWriter s = File.AppendText(motd_path);
                s.WriteLine(MOTD);
                s.Flush();
                s.Close();
            }
        }
    }
}
