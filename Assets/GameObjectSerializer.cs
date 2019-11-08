using UnityEngine;
using System.Collections;

public abstract class GameObjectSerializer : MonoBehaviour
{
    public abstract Save Serialize(Save save);
    public abstract void CreateObject(Save save);
}
