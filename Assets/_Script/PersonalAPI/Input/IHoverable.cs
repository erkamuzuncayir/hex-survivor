using System;

namespace _Script.PersonalAPI.Input
{
    public interface IHoverable
    {
        public Action OnHover { get; set; }
    }
}