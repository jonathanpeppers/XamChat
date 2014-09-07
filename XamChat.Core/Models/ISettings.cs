using System;

namespace XamChat.Core
{
	public interface ISettings
	{
		User User { get; set; }

		void Save();
	}
}

