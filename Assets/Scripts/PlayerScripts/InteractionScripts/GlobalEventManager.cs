using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent<string> onLocationDoorClicked = new UnityEvent<string>();

    public static void PressedChengeLocation(string doorName)
    {
        onLocationDoorClicked.Invoke(doorName);
    }
}
