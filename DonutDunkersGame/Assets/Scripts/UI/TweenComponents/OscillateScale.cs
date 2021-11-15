using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class OscillateScale : MonoBehaviour {
    public float speed = 1;
    public Vector3 maxScale = Vector3.one;
    public AnimationCurve easingCurve = Tween.EaseInOut;
    void Awake() {
        Tween.LocalScale(this.transform, maxScale, speed, 0, easingCurve, Tween.LoopType.PingPong);
    }
}
