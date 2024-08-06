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
                    }
                });
                return await pojZippojZip.BuildAsync();
            }
        }
    }
}