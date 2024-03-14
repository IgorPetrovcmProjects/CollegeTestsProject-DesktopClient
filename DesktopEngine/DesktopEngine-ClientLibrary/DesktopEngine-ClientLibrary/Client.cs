namespace DesktopEngine_ClientLibrary
{

	using System.Net.Http;
    using System.Net.Http.Json;

    public class Client
	{
		private static HttpClient? _client;

		public async Task<object> SendGetRequest(string url, Type returnedType)
		{
			_client = new HttpClient();

			object? json = await _client.GetFromJsonAsync(url, returnedType);

			if (json == null){
				/*object ob = new object();
				return Convert.ChangeType(ob, returnedType);*/
				return null;
			}

			return json;
		}

		public async Task<HttpResponseMessage> SendGetCommand(string url)
		{
			_client = new HttpClient();

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "url");

			return await _client.SendAsync(request);
		}

		public async Task<HttpResponseMessage> SendPostRequest(string url, byte[] bytesForRequest)
		{
			_client = new HttpClient();

			ByteArrayContent byteArrayContent = new ByteArrayContent(bytesForRequest);

			return await _client.PostAsync(url, byteArrayContent);
		}
	}
}
