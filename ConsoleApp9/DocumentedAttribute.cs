using System;

[AttributeUsage(AttributeTargets.Class)]
public class DocumentedAttribute : Attribute
{
public string Descriptioun {get; }
public DocumentedAttribute(string descriptioun)
{
    Descriptioun = descriptioun;
}
}
