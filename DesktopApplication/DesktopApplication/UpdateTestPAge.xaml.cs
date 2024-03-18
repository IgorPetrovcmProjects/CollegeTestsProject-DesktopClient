namespace DesktopApplication;

using DesktopEngine_ClientLibrary;
using DesktopEngine.Model;

public partial class UpdateTestPage : ContentPage
{
	DesktopClient client;

	string titleTest;

	List<Question> questions;

	public UpdateTestPage(string titleTest)
	{
		InitializeComponent();

		this.titleTest = titleTest;

		client = new DesktopClient();

		questions = client.GetTest(titleTest);

		inputNameTest.Text = titleTest;

		foreach (Question question in questions)
		{
			Border borderForQuestion = new Border()
			{
				HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, false),
				HeightRequest = 300,
				Margin = new Thickness(5, 10, 5, 0)
			};

			ScrollView scrollInBorder = new ScrollView();

			Button btnDeleteQuestion = new Button()
			{
				BackgroundColor = Colors.Red,
				Text = "Delete",
				HorizontalOptions = new LayoutOptions(LayoutAlignment.Start, false)
			};
			btnDeleteQuestion.Clicked += BtnDeleteQuestion_Click;

			Entry inputQuestionName = new Entry()
			{
				Text = question.question,
				Margin = new Thickness(20, 15, 10, 0)
			};

			Label nameQuestion = new Label()
			{
				BindingContext = inputQuestionName,
				Margin = new Thickness(20, 15, 10, 0),
				FontSize = 22
			};
			nameQuestion.SetBinding(Label.TextProperty, "Text");

			VerticalStackLayout stackForAnswers = new VerticalStackLayout() { Margin = new Thickness(20, 15, 15, 0) };

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

			verticalStack.Add(btnDeleteQuestion);
			verticalStack.Add(nameQuestion);
			verticalStack.Add(inputQuestionName);
			verticalStack.Add(stackForAnswers);

			scrollInBorder.Content = verticalStack;

			borderForQuestion.Content = scrollInBorder;

			stackForTest.Add(borderForQuestion);

			int countAnswerOprions = 1;

			foreach (string answerOption in question.answerOptions)
			{
				VerticalStackLayout stack = (VerticalStackLayout)btnAddAnswer.Parent;

				HorizontalStackLayout horizontalStack = new HorizontalStackLayout() { Margin = new Thickness(10, 10, 0, 0) };

				Entry inputAnswerText = new Entry()
				{
					WidthRequest = 300,
					Text = answerOption
				};

				CheckBox answer = new CheckBox()
				{
				};

				Button btnDeleteAnswer = new Button()
				{
					BackgroundColor = Colors.Red,
					Text = "Delete",
					FontSize = 9
				};
				btnDeleteAnswer.Clicked += BtnDeleteAnswer_Click;

				horizontalStack.Add(answer);
				horizontalStack.Add(inputAnswerText);
				horizontalStack.Add(btnDeleteAnswer);

				stack.Add(horizontalStack);

				if (question.answers.Any(x => x == countAnswerOprions))
				{
					answer.IsChecked = true;
				}

				countAnswerOprions++;
			}
		}
	}

	public async void BtnBack_Click(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new MainPage());
	}

	public void BtnDeleteQuestion_Click(object? sender, EventArgs e)
	{
		Button btn = (Button)sender;

		Element stack = btn.Parent;

		Element scroll = stack.Parent;

		Element border = scroll.Parent;

		VerticalStackLayout mainStack = (VerticalStackLayout)border.Parent;

		mainStack.Remove((Border)border);
	}

	public void BtnDeleteAnswer_Click(object? sender, EventArgs e)
	{
		Button btn = (Button)sender;

		Element stack = btn.Parent;

		VerticalStackLayout verticalStack = (VerticalStackLayout)stack.Parent;

		verticalStack.Remove((HorizontalStackLayout)stack);
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

		Button btnDeleteQuestion = new Button()
		{
			BackgroundColor = Colors.Red,
			Text = "Delete",
			HorizontalOptions = new LayoutOptions(LayoutAlignment.Start, false)
		};
		btnDeleteQuestion.Clicked += BtnDeleteQuestion_Click;

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

		VerticalStackLayout stackForAnswers = new VerticalStackLayout() { Margin = new Thickness(20, 15, 15, 0) };

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

		verticalStack.Add(btnDeleteQuestion);
		verticalStack.Add(nameQuestion);
		verticalStack.Add(inputQuestionName);
		verticalStack.Add(stackForAnswers);

		scrollInBorder.Content = verticalStack;

		borderForQuestion.Content = scrollInBorder;

		stackForTest.Add(borderForQuestion);
	}

	public async void BtnAddAnswer_Click(object? sender, EventArgs e)
	{
		if (sender is Button btn)
		{
			VerticalStackLayout stack = (VerticalStackLayout)btn.Parent;

			HorizontalStackLayout horizontalStack = new HorizontalStackLayout() { Margin = new Thickness(10, 10, 0, 0) };

			Entry inputAnswerText = new Entry()
			{
				WidthRequest = 300
			};

			CheckBox answer = new CheckBox()
			{
			};

			Button btnDeleteAnswer = new Button()
			{
				BackgroundColor = Colors.Red,
				Text = "Delete",
				FontSize = 9
			};
			btnDeleteAnswer.Clicked += BtnDeleteAnswer_Click;

			horizontalStack.Add(answer);
			horizontalStack.Add(inputAnswerText);
			horizontalStack.Add(btnDeleteAnswer);

			stack.Add(horizontalStack);
		}
	}

	public async void BtnSave_Click(object? sender, EventArgs e)
	{
		questions = new List<Question>();

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

		Test test = new Test(testNameLabel.Text, questions);

		client = new DesktopClient();
		client.UpdateTest(titleTest, test);

		await Navigation.PushAsync(new MainPage());
	}
}