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
    private ButtonState basicAttackButtonState = ButtonState.Default;

    void Update()
    {
        if (Input.GetButtonDown("Dash"))
        {
            dashButtonState = ButtonState.OnDowned;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            basicAttackButtonState = ButtonState.OnDowned;
        }
    }


    //TODO: Refactor these common patterns
    public bool HasDashButtonOnDown()
    {
        return dashButtonState == ButtonState.OnDowned;
    }

    public void ResetDashButtonState()
    {
        dashButtonState = ButtonState.Default;
    }

    public bool HasBasicAttackButtonOnDown()
    {
        return basicAttackButtonState == ButtonState.OnDowned;
    }

    public void ResetBasicAttackButtonState()
    {
        basicAttackButtonState = ButtonState.Default;
    }
}
