namespace TAEssentials.UI.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Determines whether the specified enumerable is null or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
    }
}