namespace DesktopApplication
{
	using Microsoft.Extensions.Configuration;
	using DesktopApplication.Configuration;
	using Newtonsoft.Json;
	using System.Text;

	public class ConfigurationDeliverer
	{
		private string GetPathWithConfiguration()
		{
			string pathWithConfiguration = "C:\\Users\\igorp\\MyProjects\\CollegeTestsProject\\CollegeTestsProject-DesktopClient\\DesktopApplication\\DesktopApplication\\bin\\Debug\\Configuration";

			if (!Directory.Exists(pathWithConfiguration))
			{
				Directory.CreateDirectory(pathWithConfiguration);

				Directory.CreateDirectory(pathWithConfiguration + "\\Tests");

				File.Create(pathWithConfiguration + "\\appsettings.json").Close();

				SourcePath sourcePath = new SourcePath();

				sourcePath.lines.Add("defaultPath", pathWithConfiguration);

				using (FileStream fs = new FileStream(pathWithConfiguration + "\\appsettings.json", FileMode.Create, FileAccess.Write))
				{
					string json = JsonConvert.SerializeObject(sourcePath);

					byte[] buffer = Encoding.UTF8.GetBytes(json);

					fs.Write(buffer, 0, buffer.Length);
				}
			}

			return pathWithConfiguration;
		}

		public string GetSourcePathFromConfiguration(string pathSectionName)
		{
			IConfiguration configuration = new ConfigurationBuilder()
					.SetBasePath(GetPathWithConfiguration())
					.AddJsonFile("appsettings.json")
					.Build();
				


			IConfigurationSection mainSection = configuration.GetSection("lines");

			IEnumerable<IConfigurationSection> mainSectionChildren = mainSection.GetChildren();

			foreach (IConfigurationSection section in mainSectionChildren)
			{
				if (section.Key == pathSectionName)
				{
					return section.Value;
				}
			}

			return "";
		}
	}
}
