using System.Collections.Concurrent;
using Reqnroll;

namespace TAEssentials.UI.Extensions
{
    public static class ReqnrollExtensions
    {
        public static void AddOrUpdate(this FeatureContext featureContext, string key, object value)
        {
            if (featureContext.ContainsKey(key))
            {
                featureContext.Remove(key);
                featureContext.Add(key, value);
                return;
            }

            featureContext.Add(key, value);
        }

        public static void AddToList<T>(this ScenarioContext scenarioContext, string key, T value)
        {
            if (scenarioContext.ContainsKey(key))
            {
                var list = scenarioContext.Get<List<T>>(key);
                list.Add(value);
                scenarioContext.AddOrUpdate(key, list);
            }
            else
            {
                ScenarioContext.Current.Add(key, new List<T> { value });
            }
        }

        public static void AddFromBagToList<T>(this ScenarioContext scenarioContext, string key, ConcurrentBag<T> bag)
        {
            foreach (var item in bag)
            {
                scenarioContext.AddToList(key, item);
            }
        }

        public static void AddOrUpdate(this ScenarioContext scenarioContext, string key, object value)
        {
            if (scenarioContext.ContainsKey(key))
            {
                scenarioContext.Remove(key);
                scenarioContext.Add(key, value);
            } else
            {
                scenarioContext.Add(key, value);
            }
        }
    }
}