using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace LykkeClient
{
	public class LykkeExchange : ILykkeExchange
	{
		private readonly string _apiKey;
		private readonly string _baseApiAddress = "https://hft-api.lykke.com/api";
		private readonly string _apiKeyHeader = "api-key";
		private readonly string _orderLimitResource = "Orders/limit";
		private readonly string _orderMarketResource = "Orders/market";
		private readonly string _orderBookResource = "OrderBooks";
		private readonly string _orderStatusResource = "Orders";
		private readonly string _specificOrderStatusResource = "Orders/{id}";
		private readonly string _orderCancellationResource = "Order/{id}/Cancel";
		private readonly string _isAliveResource = "IsAlive";
		private readonly string _walletResource = "Wallets";
		private readonly string _assetPairsResource = "AssetPairs";
		private readonly string _specificAssetPairsResource = "AssetPairs/{assetPairId}";
		private RestClient _client => new RestClient(_baseApiAddress);

		public LykkeExchange() {}

		public LykkeExchange(string apiKey)
		{
			Guard.IsNotNullOrWhiteSpace(apiKey, nameof(apiKey));

			_apiKey = apiKey;
		}

		#region Public API Methods

		public Task<List<AssetPair>> GetAssetPairs()
		{
			return PerformGetRequest<List<AssetPair>>(_assetPairsResource);
		}

		public Task<AssetPair> GetAssetPair(string assetPairId)
		{
			var request = new RestRequest(_specificAssetPairsResource, Method.GET);
			request.AddParameter("assetPairId", assetPairId, ParameterType.UrlSegment);

			return PerformRequest<AssetPair>(request);
		}

		public Task<List<OrderBook>> GetOrderBooks()
		{
			return PerformGetRequest<List<OrderBook>>(_orderBookResource);
		}

		public Task<OrderBook> GetOrderBook(string assetPairId)
		{
			var resource = _orderBookResource + "/" + assetPairId;
			return PerformGetRequest<OrderBook>(resource);
		}

		public Task<IsAliveResponse> IsAlive()
		{
			return PerformGetRequest<IsAliveResponse>(_isAliveResource);
		}

		
		#endregion

		#region Private API Methods

		public Task<string> PlaceLimitOrder(Order order)
		{
			return PerformPostRequest<string>(_orderLimitResource, true);
		}

		public Task PlaceMarketOrder(Order order)
		{
			var request = new RestRequest(_orderMarketResource);
			request.AddJsonBody(order);

			return PerformRequest<Task>(request, shouldAddApiKey: true);
		}

		public Task CancelOrder(string orderId)
		{
			var request = new RestRequest(_orderCancellationResource);
			request.AddParameter("id", orderId, ParameterType.UrlSegment);

			return PerformRequest<Task>(request, shouldAddApiKey: true);
		}

		public Task<OrderStatus> GetOrderStatus(string orderId)
		{
			var request = new RestRequest(_specificOrderStatusResource, Method.GET);
			request.AddParameter("id", orderId, ParameterType.UrlSegment);

			return PerformRequest<OrderStatus>(request, shouldAddApiKey: true);
		}

		public Task<List<OrderStatus>> GetOrdersStatus()
		{
			return PerformGetRequest<List<OrderStatus>>(_orderStatusResource, shouldAddApiKey: true);
		}

		public Task<List<WalletBalance>> GetBalances()
		{
			return PerformGetRequest<List<WalletBalance>>(_walletResource, shouldAddApiKey: true);
		}

		#endregion

		#region Common methods

		private void EnsureSuccessResponse(IRestResponse response)
		{
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new LykkeException(response.StatusCode,
					$"{response.StatusDescription}: {response.ErrorMessage}");
			}
		}

		private Task<T> PerformGetRequest<T>(string resource, bool shouldAddApiKey = false)
		{
			return PerformRequest<T>(Method.GET, resource, shouldAddApiKey);
		}

		private Task<T> PerformPostRequest<T>(string resource, bool shouldAddApiKey = false)
		{
			return PerformRequest<T>(Method.POST, resource, shouldAddApiKey);
		}

		private async Task<T> PerformRequest<T>(Method method, string resource, bool shouldAddApiKey = false)
		{
			var request = new RestRequest(resource, method);
			return await PerformRequest<T>(request, shouldAddApiKey);
		}

		private async Task<T> PerformRequest<T>(RestRequest request, bool shouldAddApiKey = false)
		{
			if (shouldAddApiKey)
			{
				request.AddHeader(_apiKeyHeader, _apiKey);
			}
			var response = await _client.ExecuteTaskAsync<T>(request);
			EnsureSuccessResponse(response);
			return response.Data;
		}
		#endregion
	}
}
