using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using WinUIExtentions;
using WinUIExtentions.Contracts;

namespace WinUIExtentions.Contracts;

public class DataFactory : IDataFactory
{
    public Object CreateItemData<Object, Value>(Value value)
        where Object : class, IItemData<Value>
    {
        var item = Setup.ServiceProvider.GetService<Object>();
        item.SetData(value);
        return item;
    }

    public List<Object> CreateItemDatas<Object, Value>(List<Value> value)
        where Object : class, IItemData<Value>
    {
        List<Object> items = new();
        foreach (var item in value)
        {
            items.Add(CreateItemData<Object, Value>(item));
        }
        return items;
    }
}