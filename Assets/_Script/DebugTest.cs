using NaughtyAttributes;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    [Button]
    public void DebugMe()
    {
        Vector3 startPos = new Vector3(0, 0, 0);
        Vector3 destPos = new Vector3(-10, 5, 0);
        float maxStep = 3f;
        Vector2 diff = new(Mathf.Abs(startPos.x - destPos.x), Mathf.Abs(startPos.y - destPos.y));
        // TODO: This should solve the problem
        Debug.Log(maxStep / (diff.x + diff.y) * diff);
        Debug.Log(Vector3.Lerp(startPos, destPos, maxStep / Vector3.Distance(startPos, destPos)));
    }
}