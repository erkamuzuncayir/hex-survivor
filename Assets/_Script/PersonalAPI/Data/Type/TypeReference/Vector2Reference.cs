using System;
using _Script.PersonalAPI.Data.Type.ReferenceType;
using UnityEngine;

namespace _Script.PersonalAPI.Data.Type.TypeReference
{
    [Serializable]
    public class Vector2Reference
    {
        public bool UseConstant;
        public Vector2 ConstantValue;
        public Vector2SO Variable;

        public Vector2 Value =>
            UseConstant ? ConstantValue : Variable.Value;
    }
}