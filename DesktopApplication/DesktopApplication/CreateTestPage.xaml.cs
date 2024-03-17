namespace DesktopApplication;

public partial class CreateTestPage : ContentPage
{
	public CreateTestPage()
	{
		InitializeComponent();
	}

	public void BtnAddQuestion_Click(object? sender, EventArgs e)
	{
		Border borderForQuestion = new Border()
		{
			HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, false),
			HeightRequest = 300,
			Margin = new Thickness(5,10,5,0)
		};

		Entry inputQuestionName = new Entry()
		{ 
			Text = "Question Name",
			Margin = new Thickness(20,15,10,0)
		};

		Label nameQuestion = new Label()
		{
			BindingContext = inputQuestionName,
			Margin = new Thickness(20,15,10,0),
			FontSize = 22
		};
		nameQuestion.SetBinding(Label.TextProperty, "Text");

		VerticalStackLayout stackForAnswers = new VerticalStackLayout();

		Button btnAddAnswer = new Button()
		{
			Text = "+Add",
			BackgroundColor = Colors.CornflowerBlue,
			HorizontalOptions = new LayoutOptions(LayoutAlignment.Start, false),
			FontSize = 14
		};

		stackForAnswers.Add(btnAddAnswer);

		ScrollView scrollForAnswers = new ScrollView() { Content = stackForAnswers, Margin = new Thickness(20,15,15,0) };

		VerticalStackLayout verticalStack = new VerticalStackLayout();

		verticalStack.Add(nameQuestion);
		verticalStack.Add(inputQuestionName);
		verticalStack.Add(scrollForAnswers);

		borderForQuestion.Content = verticalStack;

		stackForTest.Add(borderForQuestion);
	}

	public async void BtnBack_Click(object? sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}