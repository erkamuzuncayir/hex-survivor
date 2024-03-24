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
        Vector2 diff = new Vector2(0, 0);
        if (startPos.x > destPos.x)
            diff.x = startPos.x - destPos.x;
        else
            diff.x = destPos.x - startPos.x;
        
        if (startPos.y > destPos.y)
            diff.y = startPos.y - destPos.y;
        else
            diff.y = destPos.y - startPos.y;

            
        // TODO: This should solve the problem
        Debug.Log(maxStep / (diff.x + diff.y) * diff);
        Debug.Log(Vector3.Lerp(startPos, destPos, maxStep / Vector3.Distance(startPos, destPos)));
    }
}