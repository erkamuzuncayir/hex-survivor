using System;
using UnityEngine;

namespace _Script.PersonalAPI.Input
{
    public class HoverInputHandler : MonoBehaviour, IHoverable
    {
        public Action<Vector2> OnHoverPerformed { get; set; }
        public Action OnHoverCanceled { get; set; }
    }
}