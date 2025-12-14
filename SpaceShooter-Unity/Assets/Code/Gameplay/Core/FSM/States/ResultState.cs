using Code.Base.States;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.Core.FSM.States
{
    public class ResultState : IBaseState
    {
        public async UniTask OnEnter()
        {
            UnityEngine.Debug.Log("RESULT STATE");
            await UniTask.DelayFrame(1);
        }

        public async UniTask OnExit()
        {
            await UniTask.DelayFrame(1);
        }

        #region Factory

        public class Factory : PlaceholderFactory<IBaseState>
        {
        }

        #endregion Factory
    }

}