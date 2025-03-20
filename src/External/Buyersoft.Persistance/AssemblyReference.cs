using System.Reflection;

namespace Buyersoft.Persistance;
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}