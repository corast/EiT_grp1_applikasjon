using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour {

    public Material mat;
    public float fadeTime = 0.05f;
    bool fadeFlag = false;
    float val = 0.0f;

    public void FadeOn()
    {
        fadeFlag = !fadeFlag;
    }

    public void FadeOff()
    {
        fadeFlag = false;
    }

    void Update()
    {
        if (fadeFlag) {
            val += Time.deltaTime * fadeTime;
        }
        else {
            val -= Time.deltaTime * fadeTime;
        }

        if (val > 1.0f)
        {
            val = 1.0f;
        } else if (val <0.0f)
        {
            val = 0.0f;
        }
        mat.SetFloat("_LerpValue", val);
    }
}
