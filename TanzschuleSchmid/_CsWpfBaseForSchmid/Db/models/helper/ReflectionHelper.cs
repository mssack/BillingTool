using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Markup;






namespace CsWpfBase.Db.models.helper
{
	internal static class ReflectionHelper
	{
		private static readonly Dictionary<Type, Dictionary<string, string[]>> PropertyWithDependencyCache = new Dictionary<Type, Dictionary<string, string[]>>();
		public static Dictionary<string, string[]> GetPropertyDependencys(Type t)
		{
			Dictionary<string, string[]> mapping;
			if (PropertyWithDependencyCache.TryGetValue(t, out mapping))
				return mapping;
			mapping = CreateDependsOnDict(t);
			PropertyWithDependencyCache.Add(t, mapping);
			return mapping;
		}
		private static Dictionary<string, string[]> CreateDependsOnDict(Type t)
		{
			var properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			var preDict = new Dictionary<string, string[]>();
			foreach (var property in properties)
			{
				var dependOnAttributes = property.GetCustomAttributes(typeof(DependsOnAttribute), false);
				if (dependOnAttributes.Length == 0)
					continue;
				preDict.Add(property.Name, dependOnAttributes.OfType<DependsOnAttribute>().Select(x => x.Name.ToString()).ToArray());
			}
			//Inverse relation
			var newDict = new Dictionary<string, string[]>();
			var newKeys = preDict.Values.SelectMany(v => v).Distinct();
			foreach (var nk in newKeys)
			{
				var vals = preDict.Keys.Where(k => preDict[k].Contains(nk));
				newDict.Add(nk, vals.ToArray());
			}

			return newDict;
		}
	}
}
