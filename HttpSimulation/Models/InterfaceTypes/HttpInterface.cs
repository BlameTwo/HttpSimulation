using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HttpSimulation.Messagers;
using HttpSimulation.Models.Enums;
using HttpSimulation.Models.InterfaceTypes.HttpInterfaces;

namespace HttpSimulation.Models.InterfaceTypes;

public partial class HttpInterface : ObservableObject, InterfaceType
{
    private string name;
    private string iD;
    private string method;

    [JsonPropertyName("name")]
    public string Name
    {
        get => name;
        set => SetProperty(ref name, value);
    }

    [JsonPropertyName("id")]
    public string ID
    {
        get => iD;
        set => SetProperty(ref iD, value);
    }

    [ObservableProperty]
    HttpData data;

    [JsonPropertyName("Type")]
    public RequestType Type => RequestType.Http;

    [JsonIgnore]
    public IRelayCommand ChangedInterfaceNameCommand => new RelayCommand(ChangedInterfaceName);

    [JsonIgnore]
    public IRelayCommand RemoveInteraceCommand => new RelayCommand(RemoveInterface);

    [JsonIgnore]
    public IRelayCommand OpenItemCommand => new RelayCommand(OpenItem);

    private void OpenItem() => WeakReferenceMessenger.Default.Send<OpenInterface>(new(this));

    private void RemoveInterface()
    {
        WeakReferenceMessenger.Default.Send<RemoveInterface>(new(this));
    }

    private void ChangedInterfaceName()
    {
        WeakReferenceMessenger.Default.Send<ReInterfaceName>(new(this));
    }

    public object Clone()
    {
        var obj = (HttpInterface)base.MemberwiseClone();
        obj.Data = (HttpData)this.Data.Clone();
        return obj;
    }
}

public partial class HttpData : ObservableObject, ICloneable
{
    [property: JsonPropertyName("method")]
    [ObservableProperty]
    string httpMethod;

    [property: JsonPropertyName("uri")]
    [ObservableProperty]
    string uri;

    [property: JsonPropertyName("getParams")]
    [ObservableProperty]
    ObservableCollection<HttpGetParam> getParams;

    public object Clone()
    {
        return base.MemberwiseClone();
    }
}
