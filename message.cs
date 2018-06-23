using System;
using System.Text;

namespace Common
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
		byte[] data = new byte[273]; // private because we generate

		public byte command;

		public string message = "";
		public string username = "";

		public byte channelID;

		public Message( string msg , string user )
		{
			command = (byte)Commands.Message;
			message = msg;
			username = user;
		}

		public Message( Commands command , string message, string username, byte channelID)
		{

			this.command = (byte)command;
			this.message = message;
			this.username = username;
			this.channelID = channelID;
		}

		public Message (byte[] packet) // Reassemble data
		{
			data = packet;
			command = data[0];
			channelID = data[272];

			message = Encoding.ASCII.GetString(data, 1, 255).Trim(new char[]{ ' ', '\0' });
			username = Encoding.ASCII.GetString(data, 256, 16).Trim(new char[]{ ' ', '\0' });
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
	}

	enum Commands
	{
		System = 0,
		Message = 1,
		Connection = 2,
	}
}