namespace DesktopApplication
{
	using DesktopEngine_ClientLibrary;
	using DesktopEngine.Model;
	using Microsoft.Maui.Controls.Shapes;

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
					stackForTests.Add(new Label { Text = "There are no tests", HorizontalOptions = new LayoutOptions(LayoutAlignment.Center,false), Margin = new Thickness(30), FontSize = 22});
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

		private Border CreateBorderForTest(string title)
		{
			Border border = new Border()
			{
				HorizontalOptions = new LayoutOptions(LayoutAlignment.Start, false),
				Margin = new Thickness(10,20,0,0),
				Stroke = Colors.Black,
				BackgroundColor = Color.FromArgb("c7c7c7"),
				StrokeShape = new RoundRectangle() { CornerRadius = 12},
				WidthRequest = 900,
				HeightRequest = 150,
			};

			StackLayout stack = new StackLayout();

			Label label = new Label()
			{
				Text = title,
				FontSize = 24,
				Margin = new Thickness(5, 5, 0, 0),
				HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false)
			};

			stack.Add(label);

			HorizontalStackLayout horizontalStack = new HorizontalStackLayout()
			{
				VerticalOptions = new LayoutOptions(LayoutAlignment.End, true)
			};

			Button btnUpdate = new Button()
			{
				Text = "Update",
				FontSize = 14,
				Margin = new Thickness(25, 0, 0, 10),
				HorizontalOptions = new LayoutOptions(LayoutAlignment.Start, false),
				VerticalOptions = new LayoutOptions(LayoutAlignment.End, false),
				WidthRequest = 200
			};

			Button btnStart = new Button()
			{ 
				Text = "Start",
				FontSize = 14,
				Margin = new Thickness(130,0,0,10),
				WidthRequest = 200
			};

			Button btnDelete = new Button()
			{
				Text = "Delete",
				FontSize = 14,
				Margin = new Thickness(130, 0, 0, 10),
				WidthRequest = 200
			};

			horizontalStack.Add(btnUpdate);
			horizontalStack.Add(btnStart);
			horizontalStack.Add(btnDelete);

			stack.Add(horizontalStack);

			border.Content = stack;

			return border;
		}

		public async void CreateTestNavigate(object? sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("CreateTestPage");
		}
	}

}
