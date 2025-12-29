namespace TAEssentials.UI.Extensions
{
    /// <summary>
    /// Extension methods for Enums
    /// </summary>
    public static class EnumExtensions
    {
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            
            return (T)attributes.Single();
        }
    }
}