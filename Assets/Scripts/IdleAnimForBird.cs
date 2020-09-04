using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimForBird : MonoBehaviour
{
    [SerializeField] private float thrust, minTiltSmooth, maxTiltSmooth, hoverDistance, hoverSpeed;
    private float timer, tiltSmooth, y;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        y = hoverDistance * Mathf.Sin(hoverSpeed * timer);
        var yx = transform.localPosition = new Vector3(0, y, 0);
    }
}
