namespace ClientLibrary.Tests
{
	using DesktopEngine_ClientLibrary;
	using DesktopEngine.Model;
	using Newtonsoft.Json;
	using System.Text;
	using Microsoft.Extensions.Configuration;

	[TestFixture]
	public class DesktopClientTests
	{
		private string GetPathWithConfiguration()
		{
			string pathWithConfiguration;
#if DEBUG
			pathWithConfiguration = Environment.CurrentDirectory;
#else
			pathWithConfiguration = Environment.CurrentDirectory + "\\bin\\Debug\\net8.0\\";
#endif
			return pathWithConfiguration;
		}

		private string GetSourcePathFromConfiguration(string pathSectionName)
		{
			IConfigurationRoot configuration= new ConfigurationBuilder()
											.SetBasePath(GetPathWithConfiguration())
											.AddJsonFile("appsettings.json")
											.Build();

			IConfigurationSection mainSection = configuration.GetSection("sourcePath");

			IEnumerable<IConfigurationSection> mainSectionChildren = mainSection.GetChildren();

			foreach (IConfigurationSection section in mainSectionChildren)
			{
				if (section.Key == pathSectionName)
				{
					return section.Value;
				}
			}

			return "";
		}

		[Test, Order(0)]
		public void AssignSourcePath_AttemptToAssignSourcePath_SourcePathAssigned()
		{
			DesktopClient client = new DesktopClient();

			string sourcePath = GetSourcePathFromConfiguration("defaultPath");

			client.AssignSourcePath(sourcePath);
		}

		/*[Test, Order(1)]
		public void CreateTest_CreateSingleTest_TestCreated()
		{
			DesktopClient client = new DesktopClient();

			string testTitle = "HowDay";

			List<Question> questions = new List<Question>()
			{
				new Question { question = "Как месяц "}
			};

			string json = JsonConvert.SerializeObject(questions);

			string serverAnswer = client.CreateTest(testTitle, json);

			Assert.That(
				serverAnswer,
				Is.EqualTo("OK")
				);
		}*/

		[Test, Order(2)]
		public async Task GetTest_GetExistsTest_GetTest()
		{
			DesktopClient client = new DesktopClient();

			string testTitle = "HowDay";

			List<Question> questions = client.GetTest(testTitle);

			Assert.That(
				questions.Count > 0,
				Is.True
				);
		}

		[Test, Order(3)]
		public async Task GetTitles_GetAllTitles_GetAllTestTitles()
		{
			DesktopClient client = new DesktopClient();

			List<string> titles = client.GetTitles();

			Assert.That(
				titles.Count > 0,
				Is.True
				);
		}

		[Test, Order(4)]
		public async Task DeleteTest_DeleteExistsTest_TestDeleted()
		{
			DesktopClient client = new DesktopClient();

			string testTitle = "HowDay";

			string serverAnswer = client.DeleteTest(testTitle);

			Assert.That(
				serverAnswer,
				Is.EqualTo("OK")
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

			string serverAnswer = client.CreateTest(testTitle, questions);

			Assert.That(
				serverAnswer,
				Is.EqualTo("OK")
				);

			string newTestTitle = testTitle + "NEW";

			List<Question> newQuestion = new List<Question>();

			Test newTest = new Test(newTestTitle, questions);

			serverAnswer = client.UpdateTest(testTitle, newTest);

			Assert.That(
				serverAnswer,
				Is.EqualTo("OK"));
		}
	}
}