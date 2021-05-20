using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirosFuego : MonoBehaviour
{
    [SerializeField] float r0 = 3;
    [SerializeField] float w = 1;
    float t = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float r = r0;
        t += Time.deltaTime;

        float x = r * Mathf.Cos(w * t);
        float y = r * Mathf.Sin(w * t);
        Vector3 pos = new Vector3(x, y, 0);
        transform.localPosition = pos;
    }
}
