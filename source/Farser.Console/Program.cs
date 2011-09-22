using System.IO;

namespace Farser.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = System.Console.ReadLine();

			var parsedData = XmlParser.ParseXml(input);

			var dataAsJson = parsedData.ToJson();

			if (input != null)
				using (var writer = File.CreateText(input.Replace("xml","json")))
				{
					writer.Write(dataAsJson);
				}

			System.Console.WriteLine(dataAsJson);

			System.Console.ReadLine();
		}
	}
}
