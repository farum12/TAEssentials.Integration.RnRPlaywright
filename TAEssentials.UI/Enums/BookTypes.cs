using System.Runtime.Serialization;

namespace TAEssentials.UI.DataClasses.RepresentativeData
{
    public enum BookTypes
    {
        [EnumMember(Value = "Dystopian Fiction")]
        DystopianFiction,
        [EnumMember(Value = "Young Adult")]
        YoungAdult,
        [EnumMember(Value = "Fantasy")]
        Fantasy
    }
}