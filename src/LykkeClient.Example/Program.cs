using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeClient.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			Main().Wait();
		}

		static async Task Main()
		{
			var publicClient = new LykkeExchange();

			// public api methods
			var pairs = await publicClient.GetAssetPairs();
			foreach (var pair in pairs)
			{
				Console.WriteLine(pair.Name);
			}

			var orderBook = await publicClient.GetOrderBook(KnownAssetPairs.BTC_USD);

			var assetPair = await publicClient.GetAssetPair(KnownAssetPairs.BTC_USD);
			Console.WriteLine(assetPair.Name);

			// private api methods
			var apiKey = "XXX";
			var privateClient = new LykkeExchange(apiKey);

			var ordersStatus = await privateClient.GetOrdersStatus();
			foreach (var status in ordersStatus)
			{
				Console.WriteLine(status.Id);
			}

			await privateClient.PlaceLimitOrder(Order.Buy("BTCUSD", 0.0001m, 100));
			await privateClient.PlaceMarketOrder(Order.Sell("BTCUSD", 0.0001m, 100));

			var balances = await privateClient.GetBalances();
			foreach (var asset in balances)
			{
				Console.WriteLine($"{asset.AssetId}: {asset.Balance}");
			}

			var orderId = "XXX";
			await privateClient.CancelOrder(orderId);
		}
	}
}
