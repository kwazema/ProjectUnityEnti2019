using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

[System.Serializable]
public class ControllerManager {

    static public IEnumerator ControllerVibration(int playerIndex, float leftMotor, float rightMotor, float time, float timeToStart = 0)
    {

        yield return new WaitForSeconds(timeToStart);

        GamePad.SetVibration((PlayerIndex)playerIndex, leftMotor, rightMotor);

        yield return new WaitForSeconds(time);

        GamePad.SetVibration((PlayerIndex)playerIndex, 0, 0);
    }
}
