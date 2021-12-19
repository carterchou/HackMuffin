using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//let camera follow target
public class CameraFollow : MonoBehaviour
{
    public playController target;
    public float lerpSpeed = 1.0f;

    private Vector3 offset;

    private Vector3 targetPos;

    private void FixedUpdate()
    {
        target = GameManager.GetInstance().GetRoot();
        if (target == null) return;
        //offset = transform.position - target.transform.position;
        targetPos = target.transform.position;//
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }
}