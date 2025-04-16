using System;
using UnityEngine;

public class EventManage : MonoBehaviour
{
    public static event Action Shoot;
    public static event Action adBool;
    public static void DoShoot()
    {
        Shoot?.Invoke();
    }
    public static void DoadBool()
    {
        adBool?.Invoke();
    }
}
