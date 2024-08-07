using HttpSimulation.Common;
using HttpSimulation.Models.InterfaceTypes;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HttpSimulation.Models;

public sealed partial class SimulationProjcet:ICloneable
{
    public SimulationProjcet(string id)
    {
        this.ID = id;
    }

    [JsonIgnore]
    public IEnumerable<InterfaceType>? Interfaces { get; set; }

    [JsonPropertyName("projectName")]
    public string ProjectName { get; set; }

    [JsonPropertyName("id")]
    public string ID { get; }

    [JsonPropertyName("lastEditTime")]
    public DateTime LastEditTime { get; set; }

    public object Clone()
    {
        return base.MemberwiseClone();
    }

    public Task<bool> Save()
    {
        return Task.FromResult(true);
    }

    public async Task<SimulationProjcet?> SaveAsAsync(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        using(var fs = File.Create(path))
        {
            using (ProjectZipArchive pojZippojZip = new ProjectZipArchive(fs, System.IO.Compression.ZipArchiveMode.Create, false, Encoding.UTF8))
            {
                pojZippojZip.AddProjectObject(this);
                pojZippojZip.AddInterfaceObject(new List<InterfaceType>()
                {
                    new HttpInterface()
                    {
                        ID = Guid.NewGuid().ToString("N").ToUpper(),
                        Name = "默认接口",
                        Method = "GET"
                    },
                    new FolderInterface()
                    {
                        ID = Guid.NewGuid().ToString("N").ToUpper(),
                        Name = "Post请求",
                        Interfaces = new System.Collections.ObjectModel.ObservableCollection<InterfaceType>()
                        {
                            new HttpInterface()
                            {
                                ID=Guid.NewGuid().ToString("N").ToUpper(),
                                Name = "接口1",
                                Method = "Post"
                            },
                            new HttpInterface()
                            {
                                ID=Guid.NewGuid().ToString("N").ToUpper(),
                                Name = "接口2",
                                Method = "Post"
                            },
                            new HttpInterface()
                            {
                                ID=Guid.NewGuid().ToString("N").ToUpper(),
                                Name = "接口3",
                                Method = "Post"
                            },new HttpInterface()
                            {
                                ID=Guid.NewGuid().ToString("N").ToUpper(),
                                Name = "接口4",
                                Method = "Post"
                            }
                        }
                    },
                    new FolderInterface()
                    {
                        ID = Guid.NewGuid().ToString("N").ToUpper(),
                        Name = "Get请求",
                        Interfaces = new System.Collections.ObjectModel.ObservableCollection<InterfaceType>()
                        {
                            new HttpInterface()
                            {
                                ID=Guid.NewGuid().ToString("N").ToUpper(),
                                Name = "接口1",
                                Method = "Get"
                            },
                            new HttpInterface()
                            {
                                ID=Guid.NewGuid().ToString("N").ToUpper(),
                                Name = "接口2",
                                Method = "Get"
                            },
                            new HttpInterface()
                            {
                                ID=Guid.NewGuid().ToString("N").ToUpper(),
                                Name = "接口3",
                                Method = "Get"
                            },new HttpInterface()
                            {
                                ID=Guid.NewGuid().ToString("N").ToUpper(),
                                Name = "接口4",
                                Method = "Get"
                            }
                        }
                    }
                });
                return await pojZippojZip.BuildAsync();
            }
        }
    }


    public static async Task<SimulationProjcet?> ParseAsync(string path)
    {
        if (!File.Exists(path))
            return null;
        using (var fs = File.OpenRead(path))
        {
            using (ProjectZipArchive pojZippojZip = new ProjectZipArchive(fs, System.IO.Compression.ZipArchiveMode.Read, false, Encoding.UTF8))
            {
                return await pojZippojZip.GetProjectDataAsync();
            }
        }
    }

    public static async Task<SimulationProjcet?> ParseAsync(Stream stream)
    {
        using (ProjectZipArchive pojZippojZip = new ProjectZipArchive(stream, System.IO.Compression.ZipArchiveMode.Read, false, Encoding.UTF8))
        {
            return await pojZippojZip.GetProjectDataAsync();
        }
    }
}