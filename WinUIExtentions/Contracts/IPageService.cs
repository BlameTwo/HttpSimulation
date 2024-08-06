using System;

namespace WinUIExtentions.Contracts;

public interface IPageService
{
    public Type GetPage(string key);
}