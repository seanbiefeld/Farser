using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace Farser
{
	public class XmlParser
	{
		public static ExpandoObject GetAllAttrributesAndElements(XElement parentItem)
		{
			var currentItem = new ExpandoObject();

			if (parentItem.Attributes().Any())
			{
				foreach (var attribute in parentItem.Attributes())
				{
					var a = currentItem as IDictionary<string, object>;

					a[attribute.Name.ToString().Replace('-', '_')] = attribute.Value;
				}
			}

			if (parentItem.Elements().Any())
			{
				foreach (var element in parentItem.Elements())
				{
					var e = currentItem as IDictionary<string, object>;
					XElement currentElement = element;
					var existing = e.Where(item => item.Key == currentElement.Name.ToString().Replace('-', '_'));

					IList<ExpandoObject> childElements;

					if (existing.Any())
						childElements = (List<ExpandoObject>)existing.FirstOrDefault().Value;
					else
						childElements = new List<ExpandoObject>();

					e[element.Name.ToString().Replace('-', '_')] = childElements;

					childElements.Add(GetAllAttrributesAndElements(element));
				}
			}
			return currentItem;
		}
	}
}
