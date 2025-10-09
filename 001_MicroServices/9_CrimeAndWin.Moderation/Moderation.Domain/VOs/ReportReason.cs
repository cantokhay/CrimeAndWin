namespace Moderation.Domain.VOs
{
    public readonly record struct ReportReason(string Value)
    {
        public static ReportReason Hile => new("Hile");
        public static ReportReason Kufur => new("Küfür");
        public static ReportReason Taciz => new("Taciz");
        public static ReportReason Spam => new("Spam");

        public override string ToString() => Value;
    }
}
