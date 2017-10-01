using Events.Models;

namespace Events.Web.Helpers
{
    public static class EnumHelpers
    {
        public static string GetPluralizedName(this AudienceType me)
        {
            switch (me)
            {
                case AudienceType.Developer:
                    return "Developers";

                case AudienceType.IT:
                    return "IT Professionals";

                case AudienceType.Marketing:
                    return "Marketing People";

                case AudienceType.Sales:
                    return "Salespeople";

                default:
                    return string.Empty;
            }
        }
    }
}