using System.Runtime.Serialization;

namespace TAEssentials.UI.DataClasses.RepresentativeData
{
    public enum Something
    {
        [EnumMember(Value = "Value1")]
        Value1,
        [EnumMember(Value = "Value2")]
        Value2,
        [EnumMember(Value = "Value3")]
        Value3
    }
}