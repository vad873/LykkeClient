using System.Collections.Generic;
using System.Threading.Tasks;

namespace LykkeClient
{
	public interface ILykkeExchange
	{
		#region Public API Methods

		Task<IsAliveResponse> IsAlive();
		Task<List<AssetPair>> GetAssetPairs();
		Task<AssetPair> GetAssetPair(string assetPairId);
		Task<List<OrderBookEntry>> GetOrderBooks();
		Task<List<OrderBookEntry>> GetOrderBook(string assetPairId);

		#endregion

		#region Private API Methods

		Task<List<WalletBalance>> GetBalances();
		Task<string> PlaceLimitOrder(Order order);
		Task PlaceMarketOrder(Order order);
		Task<OrderStatus> GetOrderStatus(string orderId);
		Task<List<OrderStatus>> GetOrdersStatus();
		Task CancelOrder(string orderId);

		#endregion
	}
}