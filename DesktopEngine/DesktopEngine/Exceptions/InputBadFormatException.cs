﻿namespace DesktopEngine.Exception;

using System;

public class InputBadFormatException : Exception
{
	public InputBadFormatException() { }

	public InputBadFormatException(string message) : base(message) { }
}