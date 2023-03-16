using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Params")]
    public Vector2 values;
    public AnimationCurve curve;
    public float time;

    [Header("Objects")]
    public SpriteRenderer platform;
    public SpriteRenderer chainL;
    public SpriteRenderer chainR;

    public float Value
    {
        get { return value; }
        set
        {
            if (value < 0) value = 0.0f;
            this.value = value;

            platform.transform.localPosition = new Vector3( 0.0f, -value + 0.09375f, 0.0f);
            chainL.size = new Vector2(0.09375f, value + 0.59375f);
            chainR.size = new Vector2(0.09375f, value + 0.59375f);
        }
    }
    private float value;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > time) timer = 0.0f;

        float v = curve.Evaluate(timer / time);
        Value = Mathf.LerpUnclamped(values.x, values.y, v);
    }
}
