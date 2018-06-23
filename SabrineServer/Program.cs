﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net.Sockets;

namespace SabrineServer
{
	class Program
	{
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

				connection.Start(client, clients++);
			}
		}
	}

	class Client
	{
		TcpClient client;
		int clientID;
		StreamReader sr;
		StreamWriter sw;

		string username;

		public void Start(TcpClient socket, int clientID)
		{
			this.clientID = clientID;
			client = socket;
			sr = new StreamReader(client.GetStream());
			sw = new StreamWriter(client.GetStream());

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
					string msg = sr.ReadLine();
					Console.WriteLine("\t(" + clientID + ") " + username + ": " + msg);

					if (msg[0] == '/')
					{
						string[] tokens = msg.Substring(1).Split(' ');
						string command = tokens[0].ToLower();

						if (command == "setusername")
						{
							this.username = tokens[1];
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