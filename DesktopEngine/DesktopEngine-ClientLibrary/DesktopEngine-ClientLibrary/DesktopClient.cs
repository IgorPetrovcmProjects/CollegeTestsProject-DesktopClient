namespace DesktopEngine_ClientLibrary
{
	using DesktopEngine.Model;
	using System.Text;
	using Newtonsoft.Json;
	using System.Xml.Linq;

	public class DesktopClient 
	{
		private const string MainUrl = "http://127.0.0.1:3330";

		public void AssignSourcePath(string path)
		{
			Client client = new Client();

			using HttpResponseMessage response = client.SendGetCommand(MainUrl + "/sourcepath/?path=" + path);

			using Stream stream = response.Content.ReadAsStream();

			byte[] buffer = new byte[stream.Length];

			stream.Write(buffer, 0, buffer.Length);

			if ((int)response.StatusCode > 300){
				throw new HttpRequestException(Encoding.UTF8.GetString(buffer));
			}
			
		}

		public List<string> GetTitles()
		{
			Client client = new Client();

			using HttpResponseMessage response = client.SendGetRequest(MainUrl + "/gettitles/");

			using Stream stream = response.Content.ReadAsStream();

			byte[] buffer = new byte[stream.Length];

			stream.Write(buffer, 0, buffer.Length);

			if ((int)response.StatusCode > 300)
			{
				throw new HttpRequestException(Encoding.UTF8.GetString(buffer));
			}
			else
			{
				return JsonConvert.DeserializeObject <List<string>> (Encoding.UTF8.GetString(buffer));
			}
		}

		public List<Question> GetTest(string name)
		{
			Client client = new Client();

			using HttpResponseMessage response = client.SendGetRequest(MainUrl + "/gettest/?name=" + name);

			using Stream stream = response.Content.ReadAsStream();

			byte[] buffer = new byte[stream.Length];

			stream.Write(buffer, 0, buffer.Length);

			if ((int)response.StatusCode > 300){
				throw new HttpRequestException(Encoding.UTF8.GetString(buffer));
			}
			else {
				return JsonConvert.DeserializeObject <List<Question>> (Encoding.UTF8.GetString(buffer));
			}
		}

		public string CreateTest(string name, string jsonWithTest)
		{
			Client client = new Client();

			using HttpResponseMessage response = client.SendPostRequest(MainUrl + "/createtest/?name=" + name, jsonWithTest);

			using Stream stream = response.Content.ReadAsStream();

			byte[] buffer = new byte[stream.Length];

			if ((int)response.StatusCode > 300){
				throw new HttpRequestException(Encoding.UTF8.GetString(buffer));
			}
			else {
				return Encoding.UTF8.GetString(buffer);
			}
		}

		public string DeleteTest(string name)
		{
			Client client = new Client();

			using HttpResponseMessage response = client.SendDeleteRequest(MainUrl + "/deletetest/?name=" + name);

			using Stream stream = response.Content.ReadAsStream();

			byte[] buffer = new byte[stream.Length];

			if ((int)response.StatusCode > 300){
				throw new HttpRequestException (Encoding.UTF8.GetString(buffer));
			}
			else {
				return Encoding.UTF8.GetString (buffer);
			}
		}

		public string UpdateTest(string name, string jsonWithNewTest)
		{
			Client client = new Client();

			HttpResponseMessage response = client.SendPutRequest(MainUrl + "/updatetest/?name=" + name, jsonWithNewTest);

			using Stream stream = response.Content.ReadAsStream();

			byte[] buffer = new byte[stream.Length];

			if ((int)response.StatusCode > 300){
				throw new HttpRequestException(Encoding.UTF8.GetString(buffer));
			}
			else {
				return Encoding.UTF8.GetString(buffer);
			}
		}


	}
}
