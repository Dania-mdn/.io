using System;
using UnityEngine;

public class EventManage : MonoBehaviour
{
    public static event Action Shoot;
    public static void DoShoot()
    {
        Shoot?.Invoke();
    }
}
