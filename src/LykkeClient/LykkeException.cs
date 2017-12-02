using System;
using System.Net;

namespace LykkeClient
{
	public class LykkeException : Exception
	{
		public HttpStatusCode HttpStatus { get; private set; }

		public LykkeException(HttpStatusCode httpStatus, string message) : base(message)
		{
			HttpStatus = httpStatus;
		}
	}
}
