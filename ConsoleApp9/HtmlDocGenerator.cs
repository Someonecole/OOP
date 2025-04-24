using System;
using System.Reflection;
using System.Linq;
using System.Text;
using System.IO;

using System;  
using System.Reflection;  
using System.Linq;  
using System.Text;  
using System.IO;
using System.Runtime.ConstrainedExecution;

public static class HtmlDocGenerator  
{  
    public static void GenerateDocumentation(string outputPath)  
    {  
        var assembly = Assembly.GetExecutingAssembly();  
        var types = assembly.GetTypes()  
            .Where(t => t.IsClass && t.GetCustomAttribute<DocumentedAttribute>() != null);  

        var sb = new StringBuilder();  
        sb.AppendLine("<html><head><meta charset='UTF-8'><title>Документация</title></head><body>");  
        sb.AppendLine("<h1>Автоматично генерирана документация</h1>");  
     
foreach (var type in types)  
{  
    var attr = type.GetCustomAttribute<DocumentedAttribute>();  
    sb.AppendLine($"<h2>{type.Name}</h2>");  
    sb.AppendLine($"<p><i>{attr?.Descriptioun}</i></p>");  

    sb.AppendLine($"<h3>Свойства</h3><ul>");  
    foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))  
    {  
        sb.AppendLine($"<li><b>{prop.Name}</b>: {prop.PropertyType.Name}</li>");  
    }  
    sb.AppendLine($"</ul>");  

    sb.AppendLine($"<h3>Методы</h3><ul>");  
    foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))  
    {  
        var paramList = string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));  
        sb.AppendLine($"<li><b>{method.Name}</b>({paramList})</li>");  
    }  
    sb.AppendLine($"</ul>");  
    sb.AppendLine("</body></html>");
    File.WriteAllText(outputPath, sb.ToString());

    System.Console.WriteLine("HTML домументация създадена успешно");
}
}
}
