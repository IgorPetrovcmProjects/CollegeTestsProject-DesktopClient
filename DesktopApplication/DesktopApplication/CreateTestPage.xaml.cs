namespace DesktopApplication;

using DesktopEngine.Model;

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
		await Navigation.PopAsync();
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
}
