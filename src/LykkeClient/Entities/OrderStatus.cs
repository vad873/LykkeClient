using System;

namespace LykkeClient
{
	public class OrderStatus
	{
		public string Id { get; set; }
		public string ClientId { get; set; }
		public string Status { get; set; }
		public string AssetPairId { get; set; }
		public decimal Volume { get; set; }
		public decimal Price { get; set; }
		public decimal RemainingVolume { get; set; }
		public DateTime LastMatchTime { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime Registered { get; set; }
	}
}
