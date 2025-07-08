
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lostbyte.Toolkit.FactSystem
{
    public class EnumFactDefinition : FactDefinition<Enum>
    {
        [field: SerializeField, SerializeReference, SerializedEnum] public override Enum DefaultValue { get; set; }
        [field: SerializeField] public List<string> Values { get; private set; } //TODO internal
        public Type EnumType => DefaultValue?.GetType() ?? Type.GetType($"GameFacts.Enums+{FactUtils.MakeSafeIdentifier(name)}, GameFacts", false); //FIXME + Double check
        public Enum DefaultEnumValue
        {
            get
            {
                if (DefaultValue != null) return DefaultValue;
                var type = EnumType;
                if (type == null) return null;
                var arr = Enum.GetValues(EnumType);
                if (arr == null || arr.Length == 0) return null;
                return arr.GetValue(0) as Enum;
            }
        }
    }
}