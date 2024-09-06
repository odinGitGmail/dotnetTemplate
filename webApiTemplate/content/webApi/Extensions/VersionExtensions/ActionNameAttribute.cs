namespace webApi.Extensions.VersionExtensions;

public class ActionNameAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}