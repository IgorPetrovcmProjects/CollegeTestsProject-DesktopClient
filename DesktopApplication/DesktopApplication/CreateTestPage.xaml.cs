namespace DesktopApplication;

using DesktopEngine.Model;
using DesktopEngine_ClientLibrary;

public partial class CreateTestPage : ContentPage
{
	List<Question> questions;

	public CreateTestPage()
	{
		InitializeComponent();

		questions = new List<Question>();
	}

	public void BtnAddQuestion_Click(object? sender, EventArgs e)
	{
		Border borderForQuestion = new Border()
		{
			HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, false),
			HeightRequest = 300,
			Margin = new Thickness(5, 10, 5, 0)
		};

		ScrollView scrollInBorder = new ScrollView();

		Entry inputQuestionName = new Entry()
		{
			Text = "Question Name",
			Margin = new Thickness(20, 15, 10, 0)
		};

		Label nameQuestion = new Label()
		{
			BindingContext = inputQuestionName,
			Margin = new Thickness(20, 15, 10, 0),
			FontSize = 22
		};
		nameQuestion.SetBinding(Label.TextProperty, "Text");

		VerticalStackLayout stackForAnswers = new VerticalStackLayout() { Margin = new Thickness(20, 15, 15, 0)  };

		Button btnAddAnswer = new Button()
		{
			Text = "+Add",
			BackgroundColor = Colors.CornflowerBlue,
			HorizontalOptions = new LayoutOptions(LayoutAlignment.Start, false),
			FontSize = 14,
		};
		btnAddAnswer.Clicked += BtnAddAnswer_Click;

		stackForAnswers.Add(btnAddAnswer);

		VerticalStackLayout verticalStack = new VerticalStackLayout();

		verticalStack.Add(nameQuestion);
		verticalStack.Add(inputQuestionName);
		verticalStack.Add(stackForAnswers);

		scrollInBorder.Content = verticalStack;

		borderForQuestion.Content = scrollInBorder;

		stackForTest.Add(borderForQuestion);
	}

	public async void BtnBack_Click(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new MainPage());
	}

	public void BtnAddAnswer_Click(object? sender, EventArgs e)
	{
		if (sender is Button btn)
		{
			VerticalStackLayout stack = (VerticalStackLayout)btn.Parent;

			HorizontalStackLayout horizontalStack = new HorizontalStackLayout() { Margin = new Thickness(10, 10,0,0)};

			Entry inputAnswerText = new Entry()
			{ 
				WidthRequest = 300
			};

			CheckBox answer = new CheckBox()
			{
			};

			horizontalStack.Add(answer);
			horizontalStack.Add(inputAnswerText);

			stack.Add(horizontalStack);
		}
	}

	public async void BtnSave_Click(object? sender, EventArgs e)
	{
		foreach (IView view in stackForTest.Children)
		{
			if (view is Border borderForQuestion)
			{
				ScrollView scrollInBorder = (ScrollView)borderForQuestion.Content;

				VerticalStackLayout mainStack = (VerticalStackLayout)scrollInBorder.Content;

				Question question = new Question();

				foreach (IView viewInStack in mainStack)
				{
					if (viewInStack is Label labelWithName)
					{
						question.question = labelWithName.Text;
					}
					if (viewInStack is VerticalStackLayout stackWithAnswers)
					{
						int countAnswers = 0;

						foreach (IView viewInAnswers in stackWithAnswers.Children)
						{
							if (viewInAnswers is HorizontalStackLayout stackWithAnswer)
							{
								countAnswers++;

								foreach (IView answerOption in stackWithAnswer)
								{
									if (answerOption is Entry answerEntry)
									{
										question.answerOptions.Add(answerEntry.Text);
									}
									if (answerOption is CheckBox answer)
									{
										if (answer.IsChecked)
										{
											question.answers.Add(countAnswers);
										}
									}
								}
							}
						}
					}
				}

				questions.Add(question);
			}

		}

		DesktopClient desktopClient = new DesktopClient();
		desktopClient.CreateTest(testNameLabel.Text, questions);

		await Navigation.PushAsync(new MainPage());
	}
}
