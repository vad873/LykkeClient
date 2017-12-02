using System.Collections.Generic;

namespace LykkeClient
{
	public class IsAliveResponse
	{
		public string Version { get; set; }
		public string Env { get; set; }
		public bool IsDebug { get; set; }
		public List<IssueIndicator> IssueIndicators { get; set; }
	}
}
