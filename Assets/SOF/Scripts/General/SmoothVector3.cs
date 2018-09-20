using UnityEngine;

[System.Serializable]
class SmoothVector3
{
    public Vector3 currentValue = Vector3.zero;
    public Vector3 targetValue = Vector3.zero;
    public float transitionSpeed = 0.1f;
    public float epsilon = 0.001f;

    public SmoothVector3(
        Vector3 currentValue = new Vector3(),
        Vector3 targetValue = new Vector3(),
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
        var z = new SmoothFloat(currentValue.z, targetValue.z, transitionSpeed, epsilon);
        x.Update(dTime);
        y.Update(dTime);
        z.Update(dTime);
        currentValue = new Vector3(x, y, z);
    }

    public static implicit operator Vector3(SmoothVector3 value)
    {
        return value.currentValue;
    }
}
