﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Diagnostics;
using Common;
using Common.Packets;

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

				// create receiver
				Receiver r = new Receiver(client); // (autostarts)

				for (;;)
				{
					Console.Write("message> ");
					string msg = Console.ReadLine();

					if (msg.ToLower() == "q") break;

					PacketIO.Send(client, new Message(msg));
					
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

		public Receiver(TcpClient client)
		{
			this.client = client;

			Thread doink = new Thread(ReceiverMain);
			doink.Start();

		}

		private void ReceiverMain()
		{
			try
			{
				for (;;)
				{
					string msg = PacketIO.Receive(client).message; // wait for msg
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