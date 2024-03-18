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
					Color = Color.FromArgb("#E83737")
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

		Button btnCheckingResult = new Button()
		{
			BackgroundColor = Color.FromArgb("#E83737"),
			Text = "Checking Result",
			Margin = new Thickness(0,20,15,20),
			HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false)
		};
		btnCheckingResult.Clicked += BtnCheckingResult_Click;

		mainStack.Add(btnCheckingResult);
	}

	public async void BtnBack_Click(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new MainPage());
	}

	public async void BtnCheckingResult_Click(object? sender, EventArgs e)
	{
		Dictionary<string, int> results = new Dictionary<string, int>();

		foreach (IView mainStackViews in mainStack)
		{
			if (mainStackViews is ScrollView scrollInMainstack)
			{
				Border borderWithQuestionStack = (Border)scrollInMainstack.Content;

				VerticalStackLayout stackWithAnswers = (VerticalStackLayout)borderWithQuestionStack.Content;

				string titleQuestion = "";

				foreach (IView answersViews in stackWithAnswers)
				{
					if (answersViews is Label titleQuestionLable)
					{
						titleQuestion = titleQuestionLable.Text;
					}

					if (answersViews is Grid gridWithAnswers)
					{
						List<int> userAnswers = new List<int>();

						int difference = 0;

						int countAnswers = 1;

						foreach (IView horizontalStackWithAnswers in gridWithAnswers)
						{
							HorizontalStackLayout horizontalWithAnswer = (HorizontalStackLayout)horizontalStackWithAnswers;


							foreach (IView view in horizontalWithAnswer)
							{
								if (view is CheckBox userAnswer)
								{
									if (userAnswer.IsChecked == true)
									{
										userAnswers.Add(countAnswers);
									}

									countAnswers++;
								}
							}
						}

						Question question = questions.FirstOrDefault(x => x.question == titleQuestion);

						foreach (int trueAnswer in question.answers)
						{
							if (!userAnswers.Any(x => x == trueAnswer))
							{
								difference++;
							}
						}

						double fractional = (double)(question.answers.Count - difference) / question.answers.Count;

						results.Add(titleQuestion, (int)(fractional * 100));
					}
				}
			}
		}
	}
}