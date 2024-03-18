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

				if (titles?.Count == 0)
				{
					stackForTests.Add(new Label { Text = "There are no tests", HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false), Margin = new Thickness(30), FontSize = 22 });
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
				Margin = new Thickness(10, 20, 0, 0),
				Stroke = Colors.Black,
				BackgroundColor = Color.FromArgb("c7c7c7"),
				StrokeShape = new RoundRectangle() { CornerRadius = 12 },
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
			btnUpdate.Clicked += BtnUpdateTest_Click;

			Button btnStart = new Button()
			{
				Text = "Start",
				FontSize = 14,
				Margin = new Thickness(130, 0, 0, 10),
				WidthRequest = 200
			};
			btnStart.Clicked += BtnStart_Click;

			Button btnDelete = new Button()
			{
				Text = "Delete",
				FontSize = 14,
				BackgroundColor = Colors.Red,
				Margin = new Thickness(130, 0, 0, 10),
				WidthRequest = 200
			};
			btnDelete.Clicked += BtnDeleteTest_Click;


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

		public async void BtnStart_Click(object? sender, EventArgs e)
		{
			string titleTest = "";

			Button btn = (Button)sender;

			Element horizontal = btn.Parent;

			Element stack = horizontal.Parent;

			foreach (Element element in (StackLayout)stack)
			{
				if (element is Label titleLable)
				{
					titleTest = titleLable.Text;
				}
			}

			await Navigation.PushAsync(new RunTestPage(titleTest));
		}

		public async void BtnUpdateTest_Click(object? sender, EventArgs e)
		{
			string titleTest = "";

			Button btn = (Button)sender;

			Element horizontal = btn.Parent;

			Element stack = horizontal.Parent;

			foreach (Element element in (StackLayout)stack)
			{
				if (element is Label titleLable)
				{
					titleTest = titleLable.Text;
				}
			}

			await Navigation.PushAsync(new UpdateTestPage(titleTest));
		}

		public async void BtnDeleteTest_Click(object? sender, EventArgs e)
		{
			bool displayResult = await DisplayAlert("Are you sure?", "", accept: "Remove", cancel: "Cancel");

			if (displayResult)
			{
				Button btn = (Button)sender;

				Element horizontal = btn.Parent;

				Element stack = horizontal.Parent;

				foreach (Element element in (StackLayout)stack)
				{
					if (element is Label titleLable)
					{
						client = new DesktopClient();

						try
						{
							client.DeleteTest(titleLable.Text);
						}
						catch
						{
							await DisplayAlert("Are Removed", "Something went wrong", "So sad..");
							return;
						}
					}
				}

				Element border = stack.Parent;

				stackForTests.Remove((Border)border);
			}
		}
	}
}
