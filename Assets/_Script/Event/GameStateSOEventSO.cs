using _Script.PersonalAPI.Event;
using _Script.System.StateSystem.State.GameState;
using UnityEngine;

namespace _Script.Event
{
    [CreateAssetMenu(fileName = "GameStateSOEvent", menuName = "Events/Game State Event")]
    public class GameStateSOEventSO : ParamEventSO<GameStateSO>
    {
    }
}