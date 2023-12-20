using System;
using _Script.PersonalAPI.Data.Type.ValueType;

namespace _Script.PersonalAPI.Data.Type.TypeReference
{
    [Serializable]
    public class FloatReference
    {
        public bool UseConstant;
        public float ConstantValue;
        public FloatSO Variable;

        public float Value =>
            UseConstant ? ConstantValue : Variable.Value;
    }
}