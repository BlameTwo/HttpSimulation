using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace HttpSimulation.Common;

public sealed class ProjectZipArchive : ZipArchive
{
    public ProjectZipArchive(Stream stream) : base(stream)
    {

    }

    public ProjectZipArchive(Stream stream,ZipArchiveMode model,bool flage,Encoding encoding):base(stream,model,flage,encoding)
    {
        
    }

    public SimulationProjcet Project { get; protected set; }
    public List<InterfaceType> InteraceTypes { get; private set; }

    public bool AddProjectObject(SimulationProjcet project)
    {
        this.Project = project;
        return true;
    }

    public async Task<SimulationProjcet> BuildAsync()
    {
        var projectStream = this.CreateEntry("Project.json");
        using(var writer = new StreamWriter(projectStream.Open(), Encoding.UTF8))
        {
            this.Project.LastEditTime  = DateTime.Now;
            await writer.WriteAsync(JsonSerializer.Serialize(this.Project));
        }
        SimulationProjcet copyProject = (SimulationProjcet)this.Project.Clone();

        var interfaceStream = this.CreateEntry("Interface.json");
        using(var writer = new StreamWriter(interfaceStream.Open(), Encoding.UTF8))
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new InterfaceJsonConverter());
            var json = JsonSerializer.Serialize(this.InteraceTypes,options);
            var types = JsonSerializer.Deserialize<List<InterfaceType>>(json,options);
            await writer.WriteAsync(json);
        }
        return copyProject;
    }

    internal void AddInterfaceObject(List<InterfaceType> interfaceTypes)
    {
        this.InteraceTypes = interfaceTypes;
    }
}
