using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Wobbler {
    public float maxYWobbble = 0.5f;
    public int wobblePhases = 30;
    private float YWobbble = 0;
    private bool directionUp = true;
    private float alpha = 0;

    public float GetWobWob() {
        alpha += (Mathf.PI/wobblePhases) % (2* Mathf.PI);
        float wobbleStep = maxYWobbble * Mathf.Sin(alpha);
        if(YWobbble + wobbleStep > maxYWobbble) {
            directionUp = false;
        } else if(YWobbble - wobbleStep < -maxYWobbble) {
            directionUp = true;
        }
        return wobbleStep;
    }

}
