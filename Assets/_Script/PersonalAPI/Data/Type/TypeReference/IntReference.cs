using System;
using _Script.PersonalAPI.Data.Type.ValueType;

namespace _Script.PersonalAPI.Data.Type.TypeReference
{
    [Serializable]
    public class IntReference
    {
        public bool UseConstant;
        public int ConstantValue;
        public IntSO Variable;

        public int Value =>
            UseConstant ? ConstantValue : Variable.Value;
    }
}