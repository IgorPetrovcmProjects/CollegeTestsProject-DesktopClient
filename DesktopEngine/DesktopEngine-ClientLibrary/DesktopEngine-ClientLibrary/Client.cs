namespace DesktopEngine_ClientLibrary
{

	using System.Net.Http;
    using System.Net.Http.Json;
	using System.Text;

	public class Client
	{
		private static HttpClient? _client;

		public HttpResponseMessage SendGetRequest(string url)
		{
			_client = new HttpClient();

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

			return _client.Send(request);

			/*Stream stream = response.Content.ReadAsStream();

			byte[] buffer = new byte[1204];

			stream.Read(buffer, 0, (int)stream.Length);

			return Encoding.UTF8.GetString(buffer);*/
		}

		public async Task<object> GetTitlesCommand(string url)
		{
			_client = new HttpClient();

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

			object? json = await _client.GetFromJsonAsync(url, typeof(List<string>));

			return json;
		}

		public HttpResponseMessage SendGetCommand(string url)
		{
			_client = new HttpClient();

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "url");

			return _client.Send(request);
		}

		public HttpResponseMessage SendPostRequest(string url, string jsonForRequest)
		{
			_client = new HttpClient();

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

			request.Content = new StringContent(jsonForRequest, Encoding.UTF8, "application/json");

			return _client.Send(request);
		}

		public HttpResponseMessage SendDeleteRequest(string url)
		{
			_client = new HttpClient();

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);

			return _client.Send(request);
		}

		public HttpResponseMessage SendPutRequest(string url, string jsonForRequest)
		{
			_client = new HttpClient();

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url);

			request.Content = new StringContent(jsonForRequest, Encoding.UTF8, "application/json");

			return _client.Send(request);
		}
	}
}
