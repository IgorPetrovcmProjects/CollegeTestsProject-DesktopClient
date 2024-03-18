namespace DesktopApplication;

using DesktopEngine_ClientLibrary;
using DesktopEngine.Model;
using Microsoft.Maui.Controls.Shapes;

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
				RowDefinitions =
				{
					new RowDefinition()
				},

				Margin = new Thickness(15, 15, 15, 15)
			};

			int columnCount = 0;
			for (int i = 0; i < question.answerOptions.Count; i++)
			{
				int column = 0;

				if (columnCount == 2)
				{
					columnCount = 1;
				}

				gridWithAnswers.RowDefinitions.Add(new RowDefinition());

				HorizontalStackLayout horizontal = new HorizontalStackLayout();

				CheckBox checkBox = new CheckBox()
				{ 
					
				};
				Label labelWithOption = new Label()
				{
					Text = question.answerOptions[i],
					FontSize = 18
				};

				horizontal.Add(checkBox);
				horizontal.Add(labelWithOption);

				gridWithAnswers.Add(horizontal, column, i);

				columnCount++;
			}

			verticalStack.Add(questionNameLabel);
			verticalStack.Add(gridWithAnswers);

			borderWithQuestion.Content = verticalStack;

			scrollWithBorders.Content = borderWithQuestion;

			mainStack.Add(scrollWithBorders);
		}
	}
}