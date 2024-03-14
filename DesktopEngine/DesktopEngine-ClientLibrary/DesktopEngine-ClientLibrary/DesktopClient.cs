namespace DesktopEngine_ClientLibrary
{
	using DesktopEngine.Model;

	public class DesktopClient 
	{
		private const string MainUrl = "http://127.0.0.1:3330";

		public async Task<string> AssignSourcePath(string path)
		{
			Client client = new Client();

			HttpResponseMessage response = await client.SendGetCommand(MainUrl + "/sourcepath/?path=" + path);

			if ((int)response.StatusCode > 300){
				return await response.Content.ReadAsStringAsync();
			}
			else {
				return "";
			}
		}

		public async Task<List<string>> GetTitles()
		{
			Client client = new Client();

			if (await client.SendGetRequest(MainUrl + "/gettitles/", typeof(List<string>)) is List<string> titles){
				return titles;
			}
			else {
				return new List<string>();
			}
		}

		public async Task<List<Question>> GetTest(string name)
		{
			Client client = new Client();

			if (await client.SendGetRequest(MainUrl + "/gettest/?name=" + name, typeof(List<Question>)) is List<Question> questions){
				return questions;
			}
			else {
				return new List<Question>();
			}
		}

		public async Task<string> CreateTest(string name, byte[] jsonInBytes)
		{
			Client client = new Client();

			HttpResponseMessage response = await client.SendPostRequest(MainUrl + "/createtest/?name=" + name, jsonInBytes);

			if ((int)response.StatusCode > 300){
				return await response.Content.ReadAsStringAsync();
			}
			else {
				return "";
			}
		}

		public async Task<string> DeleteTest(string name)
		{
			Client client = new Client();

			HttpResponseMessage response = await client.SendDeleteRequest(MainUrl + "/deletetest/?name=" + name);

			if ((int)response.StatusCode > 300){
				return await response.Content.ReadAsStringAsync();
			}
			else {
				return "";
			}
		}

		public async Task<string> UpdateTest(string name, byte[] jsonInBytes)
		{
			Client client = new Client();

			HttpResponseMessage response = await client.SendUpdateRequest(MainUrl + "/updatetest/?name=" + name, jsonInBytes);

			if ((int)response.StatusCode > 300){
				return await response.Content.ReadAsStringAsync();
			}
			else {
				return "";
			}
		}


	}
}
