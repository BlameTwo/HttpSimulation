using System.IO.Compression;
using System.Text;
using System.Text.Json;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using static CommunityToolkit.Mvvm.ComponentModel.__Internals.__TaskExtensions.TaskAwaitableWithoutEndValidation;

namespace HttpSimulation.Common;

public sealed class ProjectZipArchive : ZipArchive
{
    public ProjectZipArchive(Stream stream)
        : base(stream) { }

    public ProjectZipArchive(Stream stream, ZipArchiveMode model, bool flage, Encoding encoding)
        : base(stream, model, flage, encoding) { }

    public SimulationProjcet Project { get; protected set; }
    public IEnumerable<InterfaceType> InteraceTypes { get; private set; }

    public bool AddProjectObject(SimulationProjcet project)
    {
        this.Project = project;
        return true;
    }

    public async Task<SimulationProjcet> BuildAsync()
    {
        var projectStream = this.CreateEntry("Project.json");
        using (var writer = new StreamWriter(projectStream.Open(), Encoding.UTF8))
        {
            this.Project.LastEditTime = DateTime.Now;
            await writer.WriteAsync(JsonSerializer.Serialize(this.Project));
        }
        SimulationProjcet copyProject = (SimulationProjcet)this.Project.Clone();
        var options = new JsonSerializerOptions();

        var interfaceStream = this.CreateEntry("Interface.json");
        using (var writer = new StreamWriter(interfaceStream.Open(), Encoding.UTF8))
        {
            options.Converters.Add(new InterfaceJsonConverter());
            var json = JsonSerializer.Serialize(this.InteraceTypes, options);
            await writer.WriteAsync(json);
        }

        return copyProject;
    }

    internal void AddInterfaceObject(IEnumerable<InterfaceType> interfaceTypes)
    {
        this.InteraceTypes = interfaceTypes;
    }

    internal async Task<SimulationProjcet?> GetProjectDataAsync()
    {
        if (this.Entries == null || this.Entries.Count == 0)
            return null;
        SimulationProjcet? project = null;
        IEnumerable<InterfaceType>? interfaces = null;
        var option = new JsonSerializerOptions();
        option.Converters.Add(new InterfaceJsonConverter());
        foreach (var item in this.Entries)
        {
            if (item.FullName.Contains("Project"))
            {
                project = await JsonSerializer.DeserializeAsync<SimulationProjcet>(
                    item.Open(),
                    option
                );
            }
            if (item.FullName.Contains("Interface"))
            {
                interfaces = await JsonSerializer.DeserializeAsync<IEnumerable<InterfaceType>>(
                    item.Open(),
                    option
                );
            }
        }
        if (project != null)
            project.Interfaces = interfaces;
        return project;
    }
}
