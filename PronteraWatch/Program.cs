using PronteraWatch;
using Serilog;
using System.Threading.Tasks;

namespace PronteraWatch
{
    class Program
    {
        public static Task Main(string[] args) => new PronteraWatchApp().Run();
    }
}
