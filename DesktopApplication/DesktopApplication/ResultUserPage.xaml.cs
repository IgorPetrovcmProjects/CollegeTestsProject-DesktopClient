namespace DesktopApplication;

public partial class ResultUserPage : ContentPage
{
	public ResultUserPage(string testTitle, Dictionary<string, int> result)
	{
		InitializeComponent();

		titleTestLabel.Text = testTitle;

		foreach (var item in result)
		{
			double progress = (double)item.Value / 100.0;

			Label questionNameLabel = new Label()
			{
				Text = item.Key + "     " + item.Value + "%",
				FontSize = 26,
				Margin = new Thickness(10, 10, 0, 0)
			};

			ProgressBar progressQuestion = new ProgressBar()
			{ 
				Progress = progress,
				//HorizontalOptions = new LayoutOptions(LayoutAlignment.Start, false),
				HeightRequest = 30,
				Margin = new Thickness(5, 5, 5, 30)
			};

			if (item.Value <= 40)
			{
				progressQuestion.ProgressColor = Colors.Red;
			}
			else if (item.Value <= 70)
			{
				progressQuestion.ProgressColor = Colors.Yellow;
			}
			else if (item.Value <= 100)
			{
				progressQuestion.ProgressColor = Colors.Green;
			}

			mainStack.Add(questionNameLabel);
			mainStack.Add(progressQuestion);
		}
	}

	public async void BtnBack_Click(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new MainPage());
	}
}