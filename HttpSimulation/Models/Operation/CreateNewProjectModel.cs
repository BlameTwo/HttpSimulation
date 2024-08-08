namespace HttpSimulation.Models.Operation
{
    public record class CreateProjectParam(string name);

    public record class CreateProjectResult(string path, SimulationProjcet project);
}
