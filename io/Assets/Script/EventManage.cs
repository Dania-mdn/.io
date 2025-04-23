using System;
using UnityEngine;

public class EventManage : MonoBehaviour
{
    public static event Action Shoot;
    public static event Action adBool;
    public static event Action adScore;
    public static event Action Die;
    public static void DoShoot()
    {
        Shoot?.Invoke();
    }
    public static void DoadBool()
    {
        adBool?.Invoke();
    }
    public static void DoadScore()
    {
        adScore?.Invoke();
    }
    public static void DoDie()
    {
        Die?.Invoke();
    }
}
