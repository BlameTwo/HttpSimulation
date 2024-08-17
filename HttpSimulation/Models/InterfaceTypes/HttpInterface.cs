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

    [ObservableProperty]
    HttpBodyData bodyData;

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
        obj.BodyData = (HttpBodyData)this.BodyData.Clone();
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

    [property: JsonPropertyName("cookies")]
    [ObservableProperty]
    ObservableCollection<HttpHeaderCookies> headerCookies;

    public object Clone()
    {
        return base.MemberwiseClone();
    }
}

public partial class HttpBodyData : ObservableObject, ICloneable
{
    [property: JsonPropertyName("selectBody")]
    [ObservableProperty]
    string selectBody;

    [property: JsonPropertyName("bodyFormData")]
    [ObservableProperty]
    ObservableCollection<BodyFormData> fromData;

    [property: JsonPropertyName("cookieData")]
    [ObservableProperty]
    ObservableCollection<HttpHeaderCookies> cookieData;

    [property: JsonPropertyName("bodyUrlencode")]
    [ObservableProperty]
    ObservableCollection<BodyUrlEncodeData> fromUrlencode;

    [property: JsonPropertyName("jsonData")]
    [ObservableProperty]
    string jsonData;

    [property: JsonPropertyName("xmlData")]
    [ObservableProperty]
    string xmlData;

    [property: JsonPropertyName("rawData")]
    [ObservableProperty]
    string rawData;

    [property: JsonPropertyName("getParams")]
    [ObservableProperty]
    ObservableCollection<HttpGetParam> getParams;

    public object Clone()
    {
        return base.MemberwiseClone();
    }
}
