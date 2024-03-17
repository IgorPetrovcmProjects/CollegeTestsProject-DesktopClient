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

			ConfigurationDeliverer configuration = new ConfigurationDeliverer();

			client = new DesktopClient();

			client.AssignSourcePath(configuration.GetSourcePathFromConfiguration("defaultPath"));

			try
			{
				titles = client.GetTitles();

				if (titles.Count == 0)
				{
					stackForTests.Add(new Label { Text = "There are no tests" });
					return;
				}

				foreach (string title in titles)
				{
					stackForTests.Add(CreateBorderForTest(title));
				}
			}
			catch (Exception ex)
			{
				stackForTests.Add(new Label { Text = ex.Message });
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
