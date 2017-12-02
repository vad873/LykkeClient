using System;
using System.Collections.Generic;

namespace LykkeClient
{
	public class OrderBookEntry
	{
		public string AssetPair { get; set; }
		public bool IsBuy { get; set; }
		public DateTime Timestamp { get; set; }
		public List<PriceValue> Prices { get; set; }
	}
}
