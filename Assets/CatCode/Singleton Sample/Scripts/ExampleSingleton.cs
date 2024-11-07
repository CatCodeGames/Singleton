using UnityEngine;

namespace CatCode
{

    [ObjectCreationConfig(
        creationMode: ObjectCreationMode.CreateNewInstance| ObjectCreationMode.LoadFromResources,
        dontDestroyOnLoad: true,
        hideFlags: HideFlags.None,
        instanceName: nameof(ExampleSingleton),
        resourceName: "Singletons/" + nameof(ExampleSingleton))]

    public sealed class ExampleSingleton : MonoSingleton<ExampleSingleton>
    {
        [SerializeField] private string _message = "Hello";

        protected override void OnInitialize()
            => Debug.Log("Singleton Initialized");


        protected override void OnDeinitialize()
            => Debug.Log("Singleton Deinitialized");

        public static void InvokeStatic()
            => Instance?.Invoke();

        public void Invoke()
        {
            /// Do Something
            Debug.Log(_message);
        }
    }
}