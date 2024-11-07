using UnityEngine;

namespace CatCode
{
    public sealed class SingletonTester : MonoBehaviour
    {
        private void Awake()
        {
            ExampleSingleton.InvokeStatic();
        }

        private void OnDestroy()
        {
            ExampleSingleton.InvokeStatic();
        }
    }
}