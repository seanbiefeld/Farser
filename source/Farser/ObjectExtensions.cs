using Newtonsoft.Json;

namespace Farser
{
	public static class ObjectExtensions
	{
		public static string ToJson(this object current)
		{
			return JsonConvert.SerializeObject(current);
		}
	}
}
