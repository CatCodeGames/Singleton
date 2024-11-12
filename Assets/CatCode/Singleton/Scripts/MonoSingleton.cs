using System;
using UnityEngine;

namespace CatCode
{

    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private bool _initialized = false;
        private static bool _destroyed = false;

        protected static T _instance;

        public static event Action<T> InstanceChanged;
        public static bool HasInstance
            => _instance != null;

        public static T Instance
        {
            get
            {
                if (_instance == null && !_destroyed)
                {
                    if (!SingletonCreator<T>.TryGetInstance(out T instance))
                        throw new Exception($"Instance {typeof(T).Name} - Not Created");
                    SetInstance(instance);
                }
                return _instance;
            }
        }

        public static bool TryGetInstance(out T instance)
        {
            instance = _instance;
            return _instance != null;
        }



        private void Awake()
        {
            if (_instance == null)
            {
                SetInstance(this as T);
                return;
            }
            if (_instance == this)
                return;

            if (Application.isPlaying)
                Destroy(gameObject);
            else
                DestroyImmediate(gameObject);
        }

        private void OnDestroy()
        {
            if (_instance != this)
                return;

            Deinitialize();
            _destroyed = true;
            _instance = null;
            InstanceChanged?.Invoke(null);
        }

        private static void SetInstance(T instance)
        {
            _instance = instance;
            instance.Initialize();
            InstanceChanged?.Invoke(instance);
        }

        private void Initialize()
        {
            if (_initialized)
                return;
            OnInitialize();
            _initialized = true;
        }

        private void Deinitialize()
        {
            if (!_initialized)
                return;
            OnDeinitialize();
            _initialized = false;
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnDeinitialize() { }
    }
}