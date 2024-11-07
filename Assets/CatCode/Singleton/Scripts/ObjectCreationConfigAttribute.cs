using System;
using UnityEngine;

namespace CatCode
{
    public sealed class ObjectCreationConfigAttribute : Attribute
    {
        private static readonly ObjectCreationConfigAttribute _default = new(ObjectCreationMode.FindOnScene);
        public ObjectCreationMode CreationMode { get; private set; }
        public HideFlags HideFlags { get; private set; }
        public bool DontDestroyOnLoad { get; private set; }
        public string InstanceName { get; private set; }
        public string ResourceName { get; private set; }

        public static ObjectCreationConfigAttribute Default => _default;

        public ObjectCreationConfigAttribute(ObjectCreationMode creationMode, bool dontDestroyOnLoad = true, HideFlags hideFlags = HideFlags.None, string instanceName = "", string resourceName = "")
        {
            this.CreationMode = creationMode;
            DontDestroyOnLoad = dontDestroyOnLoad;
            HideFlags = hideFlags;
            InstanceName = instanceName;
            ResourceName = resourceName;
        }
    }
}