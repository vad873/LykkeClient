namespace LykkeClient
{
	public static class OrderActions
	{
		public static string Buy = "Buy";
		public static string Sell = "Sell";
	}

	public class Order
	{
		public string AssetPairId { get; private set; }
		public string OrderAction { get; private set; }
		public decimal Volume { get; private set; }
		public decimal Price { get; private set; }

		protected Order() {}

		public static Order Buy(string assetPairId, decimal volume, decimal price)
		{
			return new Order
			{
				AssetPairId = assetPairId,
				Price = price,
				Volume = volume,
				OrderAction = OrderActions.Buy
			};
		}

		public static Order Sell(string assetPairId, decimal volume, decimal price)
		{
			return new Order
			{
				AssetPairId = assetPairId,
				Price = price,
				Volume = volume,
				OrderAction = OrderActions.Sell
			};
		}
	}
}
