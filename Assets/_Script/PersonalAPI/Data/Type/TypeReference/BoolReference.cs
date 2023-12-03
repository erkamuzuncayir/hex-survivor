using System;
using _Script.PersonalAPI.Data.Type.ValueType;

namespace _Script.PersonalAPI.Data.Type.TypeReference
{
    [Serializable]
    public class BoolReference
    {
        public bool UseConstant;
        public bool ConstantValue;
        public BoolSO Variable;

        public bool Value =>
            UseConstant ? ConstantValue : Variable.Value;
    }
}