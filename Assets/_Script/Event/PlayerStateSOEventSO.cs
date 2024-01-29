using _Script.PersonalAPI.Event;
using _Script.System.StateSystem.State.PlayerState;
using UnityEngine;

namespace _Script.Event
{
    [CreateAssetMenu(fileName = "PlayerStateSOEvent", menuName = "Events/Player State Event")]
    public class PlayerStateSOEventSO : ParamEventSO<PlayerStateSO>
    {
    }
}