namespace DesktopApplication;

using DesktopEngine_ClientLibrary;
using DesktopEngine.Model;
using Microsoft.Maui.Controls.Shapes;
using System.Security.Cryptography.X509Certificates;

public partial class RunTestPage : ContentPage
{
	DesktopClient client = new DesktopClient();

	string testTitle;

	List<Question> questions;

	public RunTestPage(string testTitle)
	{
		InitializeComponent();

		this.testTitle = testTitle;

		testTitleLabel.Text = testTitle;

		questions = client.GetTest(testTitle);

		foreach (Question question in questions)
		{
			ScrollView scrollWithBorders = new ScrollView();

			Border borderWithQuestion = new Border()
			{
				BackgroundColor = Color.FromArgb("#F5F5F5"),
				StrokeShape = new RoundRectangle { CornerRadius = 7 },
				Margin = new Thickness(0,20,0,0),
				HeightRequest = 250,
				WidthRequest = 800,
				HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false)
			};

			VerticalStackLayout verticalStack = new VerticalStackLayout();

			Label questionNameLabel = new Label()
			{
				FontSize = 30,
				Text = question.question,
				Margin = new Thickness(10,10,5,0),
				HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false)
			};

			Grid gridWithAnswers = new Grid()
			{
				ColumnDefinitions =
				{
					new ColumnDefinition(),
					new ColumnDefinition()
				},
				Margin = new Thickness(15, 15, 15, 15)
			};

			int column = 0;
			int row = 0;

			int rowsCount;

			if ((rowsCount = question.answerOptions.Count) % 2 > 0)
			{
				rowsCount++;
			}

			for (int i = 0; i < rowsCount; i++)
			{
				gridWithAnswers.RowDefinitions.Add(new RowDefinition());
			}

			for (int i = 0; i < question.answerOptions.Count; i++)
			{
				if (column == 2)
				{
					column = 0;
					row++;
				}

				gridWithAnswers.RowDefinitions.Add(new RowDefinition());

				HorizontalStackLayout horizontal = new HorizontalStackLayout();

				CheckBox checkBox = new CheckBox()
				{ 
					Color = Colors.Red
				};
				Label labelWithOption = new Label()
				{
					Text = question.answerOptions[i],
					FontSize = 22,
					VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false)
				};

				horizontal.Add(checkBox);
				horizontal.Add(labelWithOption);

				gridWithAnswers.Add(horizontal, column, row);

				column++;
			}

			verticalStack.Add(questionNameLabel);
			verticalStack.Add(gridWithAnswers);

			borderWithQuestion.Content = verticalStack;

			scrollWithBorders.Content = borderWithQuestion;

			mainStack.Add(scrollWithBorders);
		}

	}

	public async void BtnBack_Click(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new MainPage());
	}
}