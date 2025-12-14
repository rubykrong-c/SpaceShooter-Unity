using UnityEngine;

namespace Code.Base.Pool
{
    public class PoolObject : MonoBehaviour
    {
        [HideInInspector] public Vector3 initScale;
        private void Start()
        {
            initScale = transform.localScale;
        }
        
    }
}