using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Farser
{
	public class XmlParser
	{
		public static ExpandoObject ParseXml(string filePath)
		{
			if(File.Exists(filePath))
			{
				XDocument document = XDocument.Load(filePath);

				return GetAllAttrributesAndElements(document.Root);
			}

			return new ExpandoObject();
		}

		static dynamic GetAllAttrributesAndElements(XElement parentItem)
		{
			//create object for current element
			var currentItem = new ExpandoObject();

			//add current elements attributes as properties
			if (parentItem.Attributes().Any())
			{
				foreach (var attribute in parentItem.Attributes())
				{
					var a = currentItem as IDictionary<string, object>;

					//ensure propery names dont have a - in them
					a[attribute.Name.ToString().Replace('-', '_')] = attribute.Value;
				}
			}

			//check for child elements
			if (parentItem.Elements().Any())
			{
				foreach (var element in parentItem.Elements())
				{
					var e = currentItem as IDictionary<string, object>;
					XElement currentElement = element;
					var existing = e.Where(item => item.Key == currentElement.Name.ToString().Replace('-', '_'));

					List<dynamic> childElements;

					if (existing.Any())
						childElements = (List<dynamic>)existing.FirstOrDefault().Value;
					else
						childElements = new List<dynamic>();

					//ensure propery names dont have a - in them
					e[element.Name.ToString().Replace('-', '_')] = childElements;

					childElements.Add(GetAllAttrributesAndElements(element));
				}
			}
			return currentItem;
		}
	}
}
