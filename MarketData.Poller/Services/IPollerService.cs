using System.Threading.Tasks;

namespace MarketData.Poller.Services
{
  public interface IPollerService
  {

    Task ExecuteAsync();

  }
}
