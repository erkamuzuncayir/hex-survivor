using System;
using _Script.PersonalAPI.Data.Type.ReferenceType;
using UnityEngine;

namespace _Script.PersonalAPI.Data.Type.TypeReference
{
    [Serializable]
    public class Vector3Reference
    {
        public bool UseConstant;
        public Vector3 ConstantValue;
        public Vector3SO Variable;

        public Vector3 Value =>
            UseConstant ? ConstantValue : Variable.Value;
    }
}