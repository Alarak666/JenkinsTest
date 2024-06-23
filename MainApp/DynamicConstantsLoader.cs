using System.Reflection;

namespace MainApp;

public static class DynamicConstantsLoader
{
    public static string GetConstantValue(string assemblyPath, string namespaceName, string className, string constantName)
    {
        var assembly = Assembly.LoadFrom(assemblyPath);
        var type = assembly.GetType($"{namespaceName}.{className}");
        if (type == null)
        {
            throw new InvalidOperationException($"Type {namespaceName}.{className} not found in assembly {assemblyPath}");
        }

        var field = type.GetField(constantName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            throw new InvalidOperationException($"Constant {constantName} not found in type {type.FullName}");
        }

        return field.GetValue(null)?.ToString();
    }
}