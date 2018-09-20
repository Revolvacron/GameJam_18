using UnityEngine;

[System.Serializable]
class SmoothVector2
{
    public Vector2 currentValue = Vector2.zero;
    public Vector2 targetValue = Vector2.zero;
    public float transitionSpeed = 0.1f;
    public float epsilon = 0.001f;

    public SmoothVector2(
        Vector2 currentValue = new Vector2(),
        Vector2 targetValue = new Vector2(),
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
        var x = new SmoothFloat(currentValue.x, targetValue.x, transitionSpeed, epsilon);
        var y = new SmoothFloat(currentValue.y, targetValue.y, transitionSpeed, epsilon);
        x.Update(dTime);
        y.Update(dTime);
        currentValue = new Vector2(x, y);
    }

    public static implicit operator Vector2(SmoothVector2 value)
    {
        return value.currentValue;
    }
}
