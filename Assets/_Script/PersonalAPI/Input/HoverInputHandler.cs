using System;
using _Script.PersonalAPI.Input;
using UnityEngine;

public class HoverInputHandler : MonoBehaviour, IHoverable
{
    public Action OnHover { get; set; }
}