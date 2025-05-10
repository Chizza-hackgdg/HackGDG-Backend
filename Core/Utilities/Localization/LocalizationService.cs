using System.Reflection;
using Microsoft.Extensions.Localization;

namespace Core.Utilities.Localization;

public class ApplicationResource{}

public class LocalizationService
{
    private readonly IStringLocalizer _localizer;

    public LocalizationService(IStringLocalizerFactory factory)
    {
        var type = typeof(ApplicationResource);
        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName!);
        _localizer = factory.Create(nameof(ApplicationResource), assemblyName.Name!);
    }

    public string Translate(string key, params object[] parameters)
    {
        var translation = string.Format(_localizer[key], parameters);
        return translation ?? string.Format(key, parameters);
    }

    public async Task<string> TranslateAsync(Task<string> key, params Task<object>[] parameters)
    {
        var resolvedKey = await key;

        var translation = string.Format(_localizer[resolvedKey], parameters.ToArray());
        return translation;
    }

}