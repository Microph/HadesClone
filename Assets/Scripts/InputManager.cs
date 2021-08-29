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
    private ButtonState projectileAttackButtonState = ButtonState.Default;

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
        if (Input.GetButtonDown("Fire2"))
        {
            projectileAttackButtonState = ButtonState.OnDowned;
        }
    }

    //TODO: Maybe refactor these common patterns
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

    public bool HasProjectileAttackButtonOnDown()
    {
        return projectileAttackButtonState == ButtonState.OnDowned;
    }

    public void ResetProjectileAttackButtonState()
    {
        projectileAttackButtonState = ButtonState.Default;
    }
}
