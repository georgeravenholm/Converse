using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Diagnostics;
using Common;

namespace SabrineClient
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				if (args.Count() < 1) throw new Exception("No server specified!");
				string server = args[0];

				TcpClient client = new TcpClient(server, 6742); // HTTP

				StreamReader sr = new StreamReader(client.GetStream());
				StreamWriter sw = new StreamWriter(client.GetStream());

				// create receiver
				Receiver r = new Receiver(client); // (autostarts)

				for (;;)
				{
					Console.Write("message> ");
					string msg = Console.ReadLine();

					if (msg.ToLower() == "q") break;

					sw.WriteLine(msg);
					sw.Flush();

					// teste
					Message message = new Message(msg , "aaaaaaaaaaaaaaaaa");
					byte[] teste = message.GetBytes();
					// teste
					int a = 0;
					foreach (byte b in teste) Console.WriteLine((a++).ToString() + "=" + b);

				}

				client.Close();
			}
			catch (Exception e)
			{
				Console.Beep();
				Console.WriteLine(e.Message);
			}
		}
	}

	class Receiver
	{
		private TcpClient client;
		private StreamReader sr;

		public Receiver(TcpClient client)
		{
			this.client = client;
			sr = new StreamReader(client.GetStream());

			Thread doink = new Thread(ReceiverMain);
			doink.Start();

		}

		private void ReceiverMain()
		{
			try
			{
				for (;;)
				{
					string msg = sr.ReadLine(); // wait for msg
					Console.WriteLine(msg);
				}
			}
			catch (Exception e)
			{
				Console.Beep();
				Console.WriteLine(e.Message);
			}
		}
	}
}