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
		byte[] data = new byte[273];

		byte command;

		string message = "";
		string username = "";

		byte channelID;

		public Message( string msg , string user )
		{
			command = (byte)Commands.Message;
			message = msg;
			username = user;
		}

		public Message( int command , string message, string username, byte channelID)
		{

			this.command = (byte)command;
			this.message = message;
			this.username = username;
			this.channelID = channelID;
		}

		public byte[] GetBytes()
		{
			data.Initialize(); // clear data
			data[0] = command;
			Encoding.ASCII.GetBytes(message, 0, (message.Length < 255 ? message.Length : 255), data, 1); // put message in
			Encoding.ASCII.GetBytes(username, 0, (username.Length < 16 ? username.Length : 16), data, 256); // put username in

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