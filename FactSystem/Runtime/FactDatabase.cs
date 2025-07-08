using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lostbyte.Toolkit.FactSystem
{
    [CreateAssetMenu(fileName = "FactDatabase", menuName = "Facts/Fact Database")]
    public class FactDatabase : ScriptableObject
    {
        [SerializeField] private List<KeyContainer> m_rootKeys = new();
        [field: SerializeField] internal List<FactDefinition> FactStorage { get; private set; } = new();
        [field: SerializeField] internal List<EventDefinition> EventStorage { get; private set; } = new();

        private List<KeyContainer> _rootKeys;
        public List<KeyContainer> RootKeys
        {
            get
            {
#if UNITY_EDITOR
                return Application.isPlaying ? _rootKeys ??= m_rootKeys.ToList() : m_rootKeys;
#else
                    return _rootKeys ??= m_rootKeys.ToList();
#endif
            }
        }

        private readonly Dictionary<int, KeyContainer> _keysById = new();
        private readonly Dictionary<int, FactDefinition> _factById = new();
        private readonly Dictionary<int, EventDefinition> _eventById = new();
        private static FactDatabase _instance;
        public static FactDatabase Instance
        {
            get
            {
                if (_instance == null) Initialize();
                return _instance;
            }
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        static void OnRuntimeMethodLoad()
        {
            Initialize();
        }
        private static void Initialize()
        {
            var instance = FactSettings.GetOrCreateSettings().Database;
            instance._rootKeys = null;
            instance._keysById.Clear();
            foreach (var key in instance.RootKeys)
            {
                key.Load();
                instance.AddKey(key);
            }
            instance._factById.Clear();
            foreach (var fact in instance.FactStorage)
                instance._factById[fact.GetInstanceID()] = fact;
            instance._eventById.Clear();
            foreach (var @event in instance.EventStorage)
                instance._eventById[@event.GetInstanceID()] = @event;
            _instance = instance;
        }
        public KeyContainer GetKey(int id) => _keysById[id];
        public FactDefinition GetFact(int id) => _factById[id];
        public EventDefinition GetEvent(int id) => _eventById[id];

        private void AddKey(KeyContainer key)
        {
            _keysById[key.GetInstanceID()] = key;
            foreach (var child in key.Children)
                AddKey(child);
        }
        public KeyContainer RequestTempKey(string name, List<FactValueOverride> overrides = null)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Debug.LogError("Requesting temp key is only allowed at runtime!");
                return null;
            }
#endif
            var key = CreateInstance<KeyContainer>();
            key.name = FactUtils.GenerateValidName(name + "_temp");
            key.IsSerializable = false;
            RootKeys.Add(key);
            overrides?.ForEach(o =>
            {
                if (o.Fact != null && o.Wrapper != null)
                {
                    key.ValueOverrides.Add(o.Copy());
                    if (!key.Facts.Contains(o.Fact))
                        key.Facts.Add(o.Fact);
                }
            });
            key.Load();
            return key;
        }
    }
}