using System;
using System.Text;
using System.Net.Sockets;

namespace Common
{
	namespace Packets
	{
		/*	Message class
		 *	Assemble and disassemble message packets
		 *  Stored in a string of bytes
		 *  contains: 
		 *		Command		- 1 byte
		 *		Message		- 255 bytes
		 *		Username	- 16 bytes
		 *		Channel ID	- 1 byte (UNUSED)
		 */
		class Message
		{
			public static int Length = 273;
			byte[] data = new byte[Length]; // private because we generate

			public byte command;

			public string message = "";
			public string username = "";

			public byte channelID;

			public Message(string msg)
			{
				command = (byte)Commands.Message;
				message = msg;
				username = "";
			}

			public Message(string msg, string user)
			{
				command = (byte)Commands.Message;
				message = msg;
				username = user;
			}

			public Message(Commands command, string message, string username, byte channelID)
			{

				this.command = (byte)command;
				this.message = message;
				this.username = username;
				this.channelID = channelID;
			}

			public Message(byte[] packet) // Reassemble data
			{
				data = packet;
				command = data[0];
				channelID = data[272];

				message = Encoding.ASCII.GetString(data, 1, 255).Trim(new char[] { ' ', '\0' });
				username = Encoding.ASCII.GetString(data, 256, 16).Trim(new char[] { ' ', '\0' });
			}

			public byte[] GetBytes()
			{
				data.Initialize(); // clear data
				data[0] = command;
				Encoding.ASCII.GetBytes(message, 0, (message.Length < 255 ? message.Length : 255), data, 1); // put message in
				Encoding.ASCII.GetBytes(username, 0, (username.Length < 16 ? username.Length : 16), data, 256); // put username in
				data[272] = channelID;

				return data;
			}

			public byte[] GetMessageStringAsBytes()
			{
				return Encoding.ASCII.GetBytes(message);
			}
		}

		enum Commands
		{
			System = 0,
			Message = 1,
			Connection = 2,
			Fail = 3,
            Notify = 4,
		}


		/*
		 * helper functions for
		 * byte streams on both
		 * ends
		*/
		class PacketIO
		{
			// SEND
			public static void Send(TcpClient connection, Message m)
			{
				byte[] data = m.GetBytes();
				connection.GetStream().Write(data, 0, data.Length);
			}

			// GET
			public static Message Receive(TcpClient connection)
			{
				byte[] data = new byte[Message.Length];
				int received = connection.GetStream().Read(data, 0, data.Length);
				return new Message(data);
			}
		}
	}
}