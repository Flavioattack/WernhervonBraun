using System.Reflection.Metadata;

public class Command
{
    public int Id { get; set; } // Chave primária
    public string CommandString { get; set; }
    public List<Parameter> Parameters { get; set; }
}

