using System.Reflection;

namespace Buyersoft.Infrastructure;
public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}
