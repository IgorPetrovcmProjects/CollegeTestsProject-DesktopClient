namespace DesktopApplication
{
	using DesktopEngine_ClientLibrary;
	using DesktopEngine.Model;
	public partial class MainPage : ContentPage
	{
		DesktopClient? client;

		List<string>? titles;

		public MainPage()
		{
			InitializeComponent();

			client = new DesktopClient();

			client.AssignSourcePath("C:\\Users\\Honor\\Desktop\\MyProjects\\CollegeTestsProject\\CollegeTestsProject-DesktopClient\\DesktopEngine\\DesktopEngine.Tests\\source");

			titles = client.GetTitles().Result;

			foreach (string title in titles)
			{
				stackForTests.Add(CreateBorderForTest(title));
			}
		}

		public Border CreateBorderForTest(string title)
		{
			Border border = new Border()
			{
				WidthRequest = 900,
				HeightRequest = 200,
			};

			return border;
		}


	}

}
