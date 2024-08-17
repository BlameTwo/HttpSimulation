using System;
using Microsoft.Extensions.DependencyInjection;

namespace WinUIExtentions;

public static class Setup
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public static void InitService(IServiceProvider provider)
    {
        ServiceProvider = provider;
    }

    public static T GetService<T>() => ServiceProvider.GetService<T>();

    public static T GetRequiredService<T>() => ServiceProvider.GetRequiredService<T>();
}
