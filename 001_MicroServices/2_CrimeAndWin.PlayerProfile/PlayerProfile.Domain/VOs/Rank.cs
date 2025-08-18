namespace PlayerProfile.Domain.VOs
{
    public readonly record struct Rank(
        int RankPoints, 
        int? Position
        );
}
