using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
class SmoothFloat
{
    public float currentValue = 0.0f;
    public float targetValue = 0.0f;
    public float transitionSpeed = 0.1f;
    public float epsilon = 0.001f;

    public SmoothFloat(
        float currentValue = 0.0f,
        float targetValue = 0.0f,
        float transitionSpeed = 0.1f,
        float epsilon = 0.001f)
    {
        this.currentValue = currentValue;
        this.targetValue = targetValue;
        this.transitionSpeed = transitionSpeed;
        this.epsilon = epsilon;
    }

    public void Update(float dTime)
    {
        var diff = Mathf.Abs(targetValue - currentValue);
        if (Mathf.Abs(targetValue - currentValue) > epsilon)
            currentValue += Mathf.Min(transitionSpeed * dTime, diff) * (targetValue > currentValue ? 1 : -1);
        else
            currentValue = targetValue;
    }

    public static implicit operator float(SmoothFloat value)
    {
        return value.currentValue;
    }
}
