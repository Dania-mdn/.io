using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public PlayerMow player;

    public void Update()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

        if(direction == Vector3.zero)
        {
            player.stop = true;
        }
        else
        {
            player.stop = false;
        }
        Quaternion toRotation = Quaternion.LookRotation(direction);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, toRotation, Time.deltaTime * 10f);
    }
    public void shut()
    {
        player.shot();
    }
}