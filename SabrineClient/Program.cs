using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace SabrineClient
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				string server = args[0];

				TcpClient client = new TcpClient(server, 6742); // HTTP

				StreamReader sr = new StreamReader(client.GetStream());
				StreamWriter sw = new StreamWriter(client.GetStream());

				for (;;)
				{
					Console.Write("message> ");
					string msg = Console.ReadLine();

					if (msg.ToLower() == "q") break;

					sw.WriteLine(msg);
					sw.Flush();
				}

				client.Close();
			}
			catch (Exception c)
			{
				Console.Beep();
				Console.WriteLine(c.Message);
			}
		}
	}
}