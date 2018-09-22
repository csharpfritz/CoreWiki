﻿using System;

namespace CoreWiki.Application.Common
{
	public class CommandResult
	{
		public bool Successful { get; set; }

		public Exception Exception { get; set; }

		public static CommandResult Success()
		{
			return new CommandResult {Successful = true};
		}

		public static CommandResult Error(Exception exception)
		{
			return new CommandResult {Successful = false, Exception = exception};
		}
	}
}
