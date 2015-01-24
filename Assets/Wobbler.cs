using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Wobbler {
    private float maxYWobbble = 0.5f;
    public int wobblePhases = 30;
    private float alpha = 0;
    private float randomness = 1;

    public Wobbler(float maxYWobbble) {
        this.maxYWobbble = maxYWobbble;
        randomness = Random.Range(0.5f, 1.5f);
    }

    public float GetWobWob() {
        alpha += (Mathf.PI/wobblePhases) * randomness % (2* Mathf.PI);
        float wobbleStep = maxYWobbble * Mathf.Sin(alpha);
        return wobbleStep;
    }

}
