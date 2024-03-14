namespace DesktopApplication
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new MainPage();
		}

		protected override Window CreateWindow(IActivationState? activationState)
		{
			Window window = base.CreateWindow(activationState);

			window.Height = 700;
			window.Width = 1000;
			window.MaximumWidth = 1000;
			window.MinimumWidth = 700;
			window.MaximumHeight = 700;
			window.MinimumHeight = 550;
			return window;
		}
	}
}
