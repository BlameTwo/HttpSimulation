using System.Threading.Tasks;
using WinUIExtentions.Models;

namespace WinUIExtentions.Contracts;

public interface ITabViewType
{
    public TabItemType TabItemType { get; }

    public Task<bool> SaveAsync();
}
