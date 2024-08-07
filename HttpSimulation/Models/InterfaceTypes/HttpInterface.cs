using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HttpSimulation.Messagers;
using HttpSimulation.Models.Enums;
using System.Text.Json.Serialization;

namespace HttpSimulation.Models.InterfaceTypes;

public class HttpInterface : ObservableObject,InterfaceType
{
    private string name;
    private string iD;
    private string method;

    [JsonPropertyName("name")]
    public string Name { get => name; 
        set => SetProperty(ref name, value); }

    [JsonPropertyName("id")]
    public string ID { get => iD; set => SetProperty(ref iD, value); }

    [JsonPropertyName("Type")]
    public RequestType Type => RequestType.Http;

    [JsonPropertyName("HttpMethod")]
    public string Method { get => method; set => SetProperty(ref method, value); }

    [JsonIgnore]
    public IRelayCommand ChangedInterfaceNameCommand => new RelayCommand(ChangedInterfaceName);

    [JsonIgnore]
    public IRelayCommand RemoveInteraceCommand => new RelayCommand(RemoveInterface);

    private void RemoveInterface()
    {
        WeakReferenceMessenger.Default.Send<RemoveInterface>(new(this));
    }

    private void ChangedInterfaceName()
    {
        WeakReferenceMessenger.Default.Send<ReInterfaceName>(new(this));
    }
}
