using _Script.PersonalAPI.Event;
using UnityEngine;

namespace _Script.Event
{
    [CreateAssetMenu(fileName = "IntEvent", menuName = "Events/Int Event")]
    public class IntEventSO : ParamEventSO<int>
    {
    }
}