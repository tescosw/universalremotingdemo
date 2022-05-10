using TescoSW.Tests.TestEFServer;

namespace URDemo.Server.Models
{
    public class TestMethods
    {
        public static object Sum(IBlManager blManager, params object[] values) => values.Select(v=>Convert.ToDecimal(v)).Sum();
    }
}
