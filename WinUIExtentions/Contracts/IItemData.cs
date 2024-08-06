namespace WinUIExtentions.Contracts;

public interface IItemData<T>
{
    public T Data { get; set; }

    public void SetData(T data);
}
