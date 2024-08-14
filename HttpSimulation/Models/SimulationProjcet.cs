using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using HttpSimulation.Common;
using HttpSimulation.Models.InterfaceTypes;

namespace HttpSimulation.Models;

public sealed partial class SimulationProjcet : ICloneable
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
        using (var fs = File.Create(path))
        {
            using (
                ProjectZipArchive pojZippojZip = new ProjectZipArchive(
                    fs,
                    System.IO.Compression.ZipArchiveMode.Create,
                    false,
                    Encoding.UTF8
                )
            )
            {
                pojZippojZip.AddProjectObject(this);
                if (this.Interfaces != null)
                    pojZippojZip.AddInterfaceObject(this.Interfaces.ToList());
                var interfaceId = Guid.NewGuid().ToString("N").ToUpper();
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
            return await ParseAsync(fs);
        }
    }

    public static async Task<SimulationProjcet?> ParseAsync(Stream stream)
    {
        using (
            ProjectZipArchive pojZippojZip = new ProjectZipArchive(
                stream,
                System.IO.Compression.ZipArchiveMode.Read,
                false,
                Encoding.UTF8
            )
        )
        {
            return await pojZippojZip.GetProjectDataAsync();
        }
    }
}
