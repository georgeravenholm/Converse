using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using Common;
using Common.Packets;

namespace SabrineServer
{
	class Program
	{
		static private List<Client> clientlist = new List<Client>();

		static void Main(string[] args)
		{
			TcpListener listener = new TcpListener(System.Net.IPAddress.Any,6742);
			listener.Start();
			int clients = 0;

			for (;;)
			{
				Console.WriteLine("Waiting for new connection...");
				TcpClient client = listener.AcceptTcpClient(); // wait until a client appears

				Client connection = new Client();
				clientlist.Add(connection);

				connection.Start(client, clients++,BroadcastMsg);
			}
		}

		public static int BroadcastMsg(Message m)
		{
			clientlist.RemoveAll(ClientAintValid); // collect dead clients
			foreach (Client c in clientlist)
			{
				if (ClientAintValid(c)) continue;
				PacketIO.Send(c.socket, m);
			}
			return 0;
		}

		static bool ClientAintValid(Client c)
		{
			return !c.socket.Connected;
		}
	}

	class Client
	{
		public TcpClient socket;
		public Thread thread;

		//public Stack<Message> SendStack = null; // compiler throws warnings

		private Func<Message, int> callback;

		int clientID;

		string username;

		public void Start(TcpClient socket, int clientID, Func<Message,int> callback)
		{
			this.clientID = clientID;
			this.socket = socket;
			this.callback = callback;

			//SendStack = new Stack<Message>();

			username = "Anon" + clientID;

			// create thread
			thread = new Thread(ClientMain);
			thread.Start();
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

					if (msg.Length > 1 && msg[0] == '/')
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
					else
					{
						// Broadcast D O N G
						callback(new Message(msg, username));
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
