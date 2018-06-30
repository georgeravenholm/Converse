using System;
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
		static TcpClient client;

		static void Main(string[] args)
		{
			try
			{
				if (args.Count() < 1) throw new Exception("No server specified!");
				string server = args[0];

				client = new TcpClient(server, 6742); // HTTP

				// create receiver
				Receiver r = new Receiver(client); // (autostarts)

				//for (; ; )
				//{
				//	Console.Write("message> ");
				//	string msg = Console.ReadLine();

				//	if (msg.ToLower() == "q") break;

				//	PacketIO.Send(client, new Message(msg));

				//}
				//Form1 f = new Form1(SendMessage);
				//f.Show();

				System.Windows.Forms.Application.Run(new Form1(SendMessage));
			}
			catch (Exception e)
			{
				Console.Beep();
				Console.WriteLine(e.Message);
			}
		}

		public static int SendMessage(string msg)
		{
			if (msg.ToLower() == "q")
			{
				client.Close();
				System.Environment.Exit(0); // end life
			}

			PacketIO.Send(client, new Message(msg));
			return 0;
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
					Message m = PacketIO.Receive(client); // wait for msg

					// Received message!
					switch ((Commands)m.command)
					{
						case Commands.Message:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(m.username + ": ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(m.message);

                            Console.ResetColor();
                            break;

						case Commands.System:
                            Console.ForegroundColor = ConsoleColor.Red;
							Console.Write("[SYSTEM] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.WriteLine(m.message);

                            Console.ResetColor();
							break;

						case Commands.Connection:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("* " + m.message);
                            Console.ResetColor();
                            break;

                        case Commands.Notify:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("* " + m.message);
                            Console.ResetColor();
                            break;

                        default:
							break;
					}
					
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