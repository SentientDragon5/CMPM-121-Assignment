using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System;

public class HealthBar : MonoBehaviour
{
    public GameObject slider;

    public Hittable hp;
    public float scale;
    public TextMeshProUGUI text;
    public TextMeshPro textAlt;
    float old_perc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scale = (scale <= 0)? 1 : scale;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp == null) return;
        float perc = hp.hp * 1.0f / hp.max_hp;
        if (Mathf.Abs(old_perc - perc) > 0.01f)
        {
            slider.transform.localScale = new Vector3(perc, 1, 1);
            slider.transform.localPosition = new Vector3(-(1 - perc) * scale / 2, 0, 0);
            if (text != null)
                text.text = "<color=\"white\">"+Convert.ToString(hp.hp);
            else if (textAlt != null)
                textAlt.text = "<color=\"white\">"+Convert.ToString(hp.hp);
            old_perc = perc;
        }
    }

    public void SetHealth(Hittable hp)
    {
        this.hp = hp;
        float perc = (hp.hp * 1.0f / hp.max_hp) + 0.01f;
        if (perc != float.NaN)
        {
            perc = 0;
            //Debug.LogWarning("Maximum HP is zero or HP divided by max HP evaluates to NaN.");
        }
        perc = Mathf.Clamp01(perc);

        slider.transform.localScale = new Vector3(perc, 1, 1);
        slider.transform.localPosition = new Vector3(-(1 - perc) / 2, 0, 0);
        old_perc = perc;
    }


}
