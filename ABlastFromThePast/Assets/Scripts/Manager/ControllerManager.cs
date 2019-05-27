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

    static public IEnumerator ControllerVibrationSpam(int playerIndex, float leftMotor, float rightMotor, float timeVibration, float timeStop, int bucles, float timeToStart = 0)
    {

        yield return new WaitForSeconds(timeToStart);

        for (int i = 0; i < bucles; i++)
        {
            GamePad.SetVibration((PlayerIndex)playerIndex, leftMotor, rightMotor);

            yield return new WaitForSeconds(timeVibration);

            GamePad.SetVibration((PlayerIndex)playerIndex, 0, 0);

            yield return new WaitForSeconds(timeStop);
        }
    }

    void ExampleInputs()
    {

        // In function Update() -->

        //prevState = state;
        //state = GamePad.GetState(playerIndex);

        // Detect if a button was pressed this frame
        //if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        //{
        //    GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        //}

    }
}
