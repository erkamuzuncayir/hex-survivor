using System;
using _Script.PersonalAPI.Input;
using UnityEngine;

public class HoverInputHandler : MonoBehaviour, IHoverable
{
    public Action<Vector2> OnHoverPerformed { get; set; }
    public Action OnHoverCanceled { get; set; }
}