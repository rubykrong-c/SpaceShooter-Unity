using Code.Application.Signals;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Application
{
    public class ApplicationController: IInitializable
    {
        
        private readonly SignalBus _signals;

        public ApplicationController(SignalBus signals)
        {
            _signals = signals;
        }
        
        public async void Initialize()
        {
            _signals.TryFire<ApplicationSignals.OnApplicationLoaded>();
        }
    }
}