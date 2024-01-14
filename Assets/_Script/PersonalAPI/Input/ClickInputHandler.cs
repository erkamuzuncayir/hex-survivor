using System;
using UnityEngine;

namespace _Script.PersonalAPI.Input
{
    public class ClickInputHandler : MonoBehaviour, IClickable
    {
        public Action<Vector2> OnClickPerformed { get; set; }
        public Action<Vector2> OnClickCanceled { get; set; }
    }
}
