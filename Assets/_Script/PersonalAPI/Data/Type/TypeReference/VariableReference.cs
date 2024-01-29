using System;

namespace _Script.PersonalAPI.Data.Type.TypeReference
{
    [Serializable]
    public class VariableReference<T>
    {
        public bool UseConstant;
        public T ConstantValue;
        public DataSO<T> Variable;

        public T Value =>
            UseConstant ? ConstantValue : Variable.Value;
    }
}