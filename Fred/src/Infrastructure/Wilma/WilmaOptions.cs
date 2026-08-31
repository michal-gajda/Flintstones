namespace Fred.Infrastructure.Wilma;

public sealed record class WilmaOptions
{
    public static string SectionName { get; } = "Wilma";
    public string Address { get; init; } = "http://localhost:5081";
}
