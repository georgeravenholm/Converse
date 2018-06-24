using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using Common;
using Common.Packets;

namespace SabrineServer
{
	class Program
	{
		static void Main(string[] args)
		{
			TcpListener listener = new TcpListener(System.Net.IPAddress.Any,6742);

			listener.Start();

			int clients = 0;

			List<Client> clientlist = new List<Client>();

			for (;;)
			{
				Console.WriteLine("Waiting for new connection...");
				TcpClient client = listener.AcceptTcpClient(); // wait until a client appears

				Client connection = new Client();
				clientlist.Add(connection);

				connection.Start(client, clients++);
			}
		}
	}

	class Client
	{
		public TcpClient socket;
		int clientID;

		string username;

		public void Start(TcpClient socket, int clientID)
		{
			this.clientID = clientID;
			this.socket = socket;

			username = "Anon" + clientID;

			// create thread
			Thread doink = new Thread(ClientMain);
			doink.Start();
		}

		private void ClientMain()
		{
			Console.WriteLine("Client with ID " + clientID + " thread started");

			try
			{
				while (true)
				{
					Message message = PacketIO.Receive(socket);
					if (message.command != (byte)Commands.Message) continue;
					string msg = message.message;

					Console.WriteLine("\t(" + clientID + ") " + username + ": " + msg);

					if (msg.Length > 1)
					{
						if (msg[0] == '/')
						{
							string[] argv = msg.Substring(1).Split(' ');
							int argc = argv.Count();
							string command = argv[0].ToLower();

							if (command == "setusername" && argc>1)
							{
								this.username = argv[1];
								PacketIO.Send(socket,new Message(Commands.System, "[SYSTEM] Username updated","",0));
							}
							else
							{
								PacketIO.Send(socket, new Message(Commands.System, "[SYSTEM] Command error!", "", 0));
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.Beep();
				Console.WriteLine("Thread " + clientID + ": " + e.Message);
			}

			Console.WriteLine("Client with ID " + clientID + " thread ended");
		}
	}
}
