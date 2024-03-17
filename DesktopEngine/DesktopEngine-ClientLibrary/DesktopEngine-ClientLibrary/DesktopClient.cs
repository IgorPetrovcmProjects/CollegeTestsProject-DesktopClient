namespace DesktopEngine_ClientLibrary
{
	using DesktopEngine.Model;
	using System.Text;
	using Newtonsoft.Json;

	public class DesktopClient 
	{
		private const string MainUrl = "http://127.0.0.1:3330";

		public void AssignSourcePath(string path)
		{
			Client client = new Client();

			using HttpResponseMessage response = client.SendGetCommand(MainUrl + "/sourcepath/?path=" + path);

			using Stream stream = response.Content.ReadAsStream();

			using StreamReader reader = new StreamReader( response.Content.ReadAsStream(), Encoding.UTF8 );

			string message = reader.ReadToEnd();


			if ((int)response.StatusCode > 300){
				throw new HttpRequestException(message);
			}
			
		}

		public List<string> GetTitles()
		{
			Client client = new Client();

			using HttpResponseMessage response = client.SendGetRequest(MainUrl + "/gettitles/");

			using Stream stream = response.Content.ReadAsStream();

			using StreamReader reader = new StreamReader(stream, Encoding.UTF8);

			string message = reader.ReadToEnd();

			if ((int)response.StatusCode > 300)
			{
				throw new HttpRequestException(message);
			}
			else
			{
				return JsonConvert.DeserializeObject <List<string>> (message);
			}
		}

		public List<Question> GetTest(string name)
		{
			Client client = new Client();

			using HttpResponseMessage response = client.SendGetRequest(MainUrl + "/gettest/?name=" + name);

			using Stream stream = response.Content.ReadAsStream();

			StreamReader reader = new StreamReader(stream, Encoding.UTF8);

			string message = reader.ReadToEnd();

			if ((int)response.StatusCode > 300){
				throw new HttpRequestException(message);
			}
			else {
				return JsonConvert.DeserializeObject <List<Question>> (message);
			}
		}

		public string CreateTest(string name, List<Question> questions)
		{
			Client client = new Client();

			string jsonWithQuestions = JsonConvert.SerializeObject(questions);

			using HttpResponseMessage response = client.SendPostRequest(MainUrl + "/createtest/?name=" + name, jsonWithQuestions);

			using Stream stream = response.Content.ReadAsStream();

			StreamReader reader = new StreamReader(stream, Encoding.UTF8);

			string message = reader.ReadToEnd();

			if ((int)response.StatusCode > 300){
				throw new HttpRequestException(message);
			}
			else {
				return message;
			}
		}

		public string DeleteTest(string name)
		{
			Client client = new Client();

			using HttpResponseMessage response = client.SendDeleteRequest(MainUrl + "/deletetest/?name=" + name);

			using Stream stream = response.Content.ReadAsStream();

			StreamReader reader = new StreamReader(stream, Encoding.UTF8);

			string message = reader.ReadToEnd();

			if ((int)response.StatusCode > 300){
				throw new HttpRequestException (message);
			}
			else {
				return message;
			}
		}

		public string UpdateTest(string name, List<Question> newQuestions)
		{
			Client client = new Client();

			string jsonWithNewTest = JsonConvert.SerializeObject(newQuestions);

			HttpResponseMessage response = client.SendPutRequest(MainUrl + "/updatetest/?name=" + name, jsonWithNewTest);

			using Stream stream = response.Content.ReadAsStream();

			StreamReader reader = new StreamReader(stream, Encoding.UTF8);

			string message = reader.ReadToEnd();

			if ((int)response.StatusCode > 300){
				throw new HttpRequestException(message);
			}
			else {
				return message;
			}
		}


	}
}
