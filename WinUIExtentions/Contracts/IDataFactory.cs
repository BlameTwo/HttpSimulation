using System.Collections.Generic;

namespace WinUIExtentions.Contracts;

public interface IDataFactory
{
    public Object CreateItemData<Object, Value>(Value value)
        where Object : class, IItemData<Value>;

    public List<Object> CreateItemDatas<Object, Value>(List<Value> value)
        where Object : class, IItemData<Value>;


}
