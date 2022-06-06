using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] private Transform m_Healthbar;

    private void Start()
    {
        transform.localScale = new Vector3(
            1f / transform.parent.localScale.x,
            1f / transform.parent.localScale.y,
            1f / transform.parent.localScale.z);

        StartCoroutine(MoveToPosition(5f));
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

    IEnumerator MoveToPosition(float damageTaken)
    {


        TextMeshPro _textMeshPro = GetComponent<TextMeshPro>();
        _textMeshPro.text = damageTaken.ToString();
        float _offsetY = 0f;
        float _offsetX = 0f;
        float _xOffsetFactor = UnityEngine.Random.Range(-1f, 1f);
        while (transform.position.y <= transform.position.y + 1f * transform.localScale.y)
        {
            transform.position = transform.position;
            transform.position = new Vector3(
                transform.position.x + _offsetX,
                transform.position.y + _offsetY,
                transform.position.z
                );
            _offsetX += _xOffsetFactor * Time.deltaTime;
            _offsetY += 1f * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
