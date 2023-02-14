using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static Material hurtShader;
    private readonly int ColorHash = Shader.PropertyToID("_Color");
    private static float t;

    [SerializeField] private Color A = Color.red;
    [SerializeField] private Color B = Color.white;

    public static void Reset()
    {
        t = 0;
    }


    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        hurtShader.SetColor(ColorHash, t % 0.2f < 0.1f ? A : B);
    }
}
