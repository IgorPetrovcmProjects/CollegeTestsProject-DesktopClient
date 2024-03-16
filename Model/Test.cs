namespace DesktopEngine.Model;


public class Test
{
	public string title;

	public List<Question> questions;

	public Test(string title, List<Question> questions)
	{
		this.title = title;
		this.questions = questions;
	}
}
