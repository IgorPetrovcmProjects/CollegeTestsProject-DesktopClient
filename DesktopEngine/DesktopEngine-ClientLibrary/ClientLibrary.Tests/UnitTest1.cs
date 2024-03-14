namespace ClientLibrary.Tests
{
	using DesktopEngine_ClientLibrary;
	using DesktopEngine.Model;
	using Newtonsoft.Json;
	using System.Text;

	[TestFixture]
	public class Tests
	{

		[Test, Order(1)]
		public async Task CreateTest_CreateSingleTest_TestCreated()
		{
			DesktopClient client = new DesktopClient();

			string testTitle = "HowDay";

			List<Question> questions = new List<Question>()
			{
				new Question { question = "Как месяц "}
			};

			string json = JsonConvert.SerializeObject(questions);

			string serverAnswer = await client.CreateTest(testTitle, Encoding.UTF8.GetBytes(json));

			Assert.That(
				serverAnswer,
				Is.EqualTo("")
				);
		}

		[Test, Order(2)]
		public async Task GetTest_GetExistsTest_GetTest()
		{
			DesktopClient client = new DesktopClient();

			string testTitle = "HowDay";

			List<Question> questions = await client.GetTest(testTitle);

			Assert.That(
				questions.Count > 0,
				Is.True
				);
		}

		[Test, Order(4)]
		public async Task DeleteTest_DeleteExistsTest_TestDeleted()
		{
			DesktopClient client = new DesktopClient();

			string testTitle = "HowDay";

			string serverAnswer = await client.DeleteTest(testTitle);

			Assert.That(
				serverAnswer,
				Is.EqualTo("")
				);
		}

		[Test, Order(5)]
		public async Task UpdateTest_CreateTestAndUpdateIt_TestUpdated()
		{
			DesktopClient client = new DesktopClient();

			string testTitle = "HowDay";

			List<Question> questions = new List<Question>()
			{
				new Question { question = "Как месяц "}
			};

			string json = JsonConvert.SerializeObject(questions);

			string serverAnswer = await client.CreateTest(testTitle, Encoding.UTF8.GetBytes(json));

			Assert.That(
				serverAnswer,
				Is.EqualTo("")
				);

			string newTestTitle = testTitle + "NEW";

			Test test = new Test(newTestTitle, new List<Question>());

			string newJson = JsonConvert.SerializeObject(test);

			serverAnswer = await client.UpdateTest(testTitle, Encoding.UTF8.GetBytes(newJson));

			Assert.That(
				serverAnswer,
				Is.EqualTo(""));
		}
	}
}