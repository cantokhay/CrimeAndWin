namespace PlayerProfile.Domain.VOs
{
    public readonly record struct Stats(
        int Power, 
        int Defense, 
        int Agility, 
        int Luck
        );
}
