using System;

namespace Common
{
	/*	Message class
	 *	Assemble and disassemble message packets
	 *  Stored in a string of bytes
	 *  contains: 
	 *		Command		- 1 byte
	 *		Message		- 255 bytes
	 *		Username	- 16 bytes
	 *		Channel ID	- 2 bytes (UNUSED)
	 */
	class Message
	{
		byte[] data = new byte[274];

		byte command = 0;

		string message = "";
		string username = "";

		char channelID = 0;
	}

	enum Commands
	{
		System=0,
		Message=1,
		Connection=2,
	}
}