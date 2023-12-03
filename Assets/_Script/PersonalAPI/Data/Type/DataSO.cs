using UnityEngine;

namespace _Script.PersonalAPI.Data.Type
{
    public class DataSO<TData> : ScriptableObject
    {
        public TData Value;
    }
}