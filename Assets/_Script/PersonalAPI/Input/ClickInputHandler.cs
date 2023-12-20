using System;
using UnityEngine;

namespace _Script.PersonalAPI.Input
{
    public class ClickInputHandler : MonoBehaviour, IClickable
    {
        public Action OnClickPerformed { get; set; }
        public Action OnClickCanceled { get; set; }
    }
}
