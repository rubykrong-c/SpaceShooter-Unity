using Cysharp.Threading.Tasks;

namespace Code.Base.States
{
    public interface IBaseState
    {
        UniTask OnEnter();

        UniTask OnExit();
    }
}