using Wolverine.Http;

namespace Presentation.Http
{
    public static class TestEndpoint
    {
        [WolverineGet("/test")]
        public static string GetTest()
        {
            Console.WriteLine("Wolverine endpoint executed!");
            return "Wolverine is handling HTTP";
        }
    }
}
