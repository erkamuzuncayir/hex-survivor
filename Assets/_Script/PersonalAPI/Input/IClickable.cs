using System;

namespace _Script.PersonalAPI.Input
{
    public interface IClickable
    {
        public Action OnClickPerformed { get; set; }

        public Action OnClickCanceled { get; set; }
    }
}