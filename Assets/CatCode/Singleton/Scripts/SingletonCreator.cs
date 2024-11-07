using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace CatCode
{
    public delegate bool OutFunc<T>(out T instance);

    public static class SingletonCreator<T> where T : Component
    {
        private static readonly List<OutFunc<T>> _creationFunctions = new();

        public static bool TryGetInstance(out T instance)
        {
            var attribute = typeof(T).GetCustomAttribute<ObjectCreationConfigAttribute>(false)
                ?? ObjectCreationConfigAttribute.Default;

            _creationFunctions.Clear();
            if (HasFlag(attribute.CreationMode, ObjectCreationMode.FindOnScene))
                _creationFunctions.Add(TryFindOnScene);
            if (HasFlag(attribute.CreationMode, ObjectCreationMode.LoadFromResources))
                _creationFunctions.Add((out T instance) => TryLoadFromResources(attribute.ResourceName, out instance));
            if (HasFlag(attribute.CreationMode, ObjectCreationMode.CreateNewInstance))
                _creationFunctions.Add((out T instance) => TryCreate(attribute.InstanceName, out instance));

            for (int i = 0; i < _creationFunctions.Count; i++)
                if (_creationFunctions[i].Invoke(out instance))
                {
                    if (attribute.DontDestroyOnLoad && Application.isPlaying)
                        Object.DontDestroyOnLoad(instance);
                    instance.gameObject.hideFlags = attribute.HideFlags;
                    return true;
                }

            instance = null;
            return false;
        }

        public static bool TryFindOnScene(out T instance)
        {
            instance = Object.FindFirstObjectByType<T>();
            return instance != null;
        }

        public static bool TryCreate(string name, out T instance)
        {
            GameObject gameObject = new(name, typeof(T));
            instance = gameObject.GetComponent<T>();
            return instance != null;
        }

        public static bool TryLoadFromResources(string resourceName, out T instance)
        {
            var prefab = Resources.Load<T>(resourceName);
            if (prefab == null)
            {
                instance = null;
                return false;
            }
            instance = Object.Instantiate(prefab);
            return instance != null;
        }
        private static bool HasFlag(in ObjectCreationMode source, in ObjectCreationMode flag)
          => (source & flag) != 0;
    }
}