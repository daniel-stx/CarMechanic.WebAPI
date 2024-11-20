using System.Reflection;

namespace CarMechanic.Infrastructure;

public static class InfrastructureAssembly
{
    public static Assembly Assembly => typeof(InfrastructureAssembly).Assembly;
}
