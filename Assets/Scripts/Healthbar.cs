using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Transform m_Healthbar;

    private void Start()
    {
        transform.localScale = new Vector3(
            0.5f / transform.parent.localScale.x,
            0.1f / transform.parent.localScale.y,
            1f / transform.parent.localScale.z);
        ChangeAlpha(0f);
        StartCoroutine(FadeIn());
        Vector3 _position = transform.position;
        _position.y -= 0.18f / transform.parent.localScale.y;
        transform.position = _position;
    }

    private IEnumerator FadeIn()
    {
        float alpha = 0f;
        while (alpha <= 1f)
        {
            alpha += Time.deltaTime * 4f;
            ChangeAlpha(alpha);
            yield return null;
        }
    }

    public void UpdateHealth(float healthPercentage)
    {
        Debug.Log(healthPercentage);
        m_Healthbar.localPosition = new Vector3(
            -(1f - healthPercentage) / 2f,
            0,
            0);
        m_Healthbar.localScale = new Vector3(healthPercentage, 1, 1);
    }

    private void ChangeAlpha(float alpha)
    {
        Color temp = GetComponent<SpriteRenderer>().color;
        temp.a = alpha;
        GetComponent<SpriteRenderer>().color = temp;
        temp = m_Healthbar.GetComponent<SpriteRenderer>().color;
        temp.a = alpha;
        m_Healthbar.GetComponent<SpriteRenderer>().color = temp;
    }
}
