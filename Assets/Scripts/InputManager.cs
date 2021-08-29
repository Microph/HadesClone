using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum ButtonState
    {
        Default,
        OnDowned,
    }

    private ButtonState dashButtonState = ButtonState.Default;

    void Update()
    {
        bool isDashButtonDown = Input.GetButtonDown("Dash");
        //Debug.Log("isDashButtonDown" + isDashButtonDown);
        if (isDashButtonDown) 
            dashButtonState = ButtonState.OnDowned;
    }

    public bool HasDashButtonOnDown()
    {
        return dashButtonState == ButtonState.OnDowned;
    }

    public void ResetDashButtonState()
    {
        dashButtonState = ButtonState.Default;
    }
}
