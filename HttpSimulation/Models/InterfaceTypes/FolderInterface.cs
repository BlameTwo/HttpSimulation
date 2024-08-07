using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HttpSimulation.Messagers;
using HttpSimulation.Models.Enums;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace HttpSimulation.Models.InterfaceTypes;

public partial class FolderInterface:ObservableObject,InterfaceType
{

    private string name;
    private string iD;
    private ObservableCollection<InterfaceType> interfaces;

    [JsonPropertyName("Name")]
    public string Name
    {
        get { return name; }
        set=>SetProperty(ref name, value);
    }


    [JsonPropertyName("id")]
    public string ID { get => iD; set => SetProperty(ref iD, value); }

    [JsonPropertyName("Type")]
    public RequestType Type => RequestType.Folder;


    [JsonPropertyName("interfaces")]
    public ObservableCollection<InterfaceType> Interfaces { get => interfaces; set => SetProperty(ref interfaces, value); }

    [JsonIgnore]
    public IRelayCommand ChangedInterfaceNameCommand=>new RelayCommand(ChangedInterfaceName);

    [JsonIgnore]
    public IRelayCommand RemoveInteraceCommand => new RelayCommand(RemoveInterface);

    private void RemoveInterface()
    {
        WeakReferenceMessenger.Default.Send<RemoveInterface>(new(this));
    }

    public void ChangedInterfaceName()
    {
        WeakReferenceMessenger.Default.Send<ReInterfaceName>(new(this));
    }
}