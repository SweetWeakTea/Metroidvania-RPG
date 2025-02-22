using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private float colorLooseRate;

    public void SetupAfterImage(float _loosingSpeed, Sprite _spriteImage)
    {
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = _spriteImage;
        colorLooseRate = _loosingSpeed;
    }

    private void Update()
    {
        float alpha = sr.color.a - colorLooseRate * Time.deltaTime; // ��ʾ��ɫ��͸���ȣ�ֵ��Χ�� 0 �� 1��0 ��ʾ��ȫ͸����1 ��ʾ��ȫ��͸����
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha); // RGB ֵ���ֲ��䣬��͸���� alpha ������Ϊ���������ֵ

        if (sr.color.a <= 0)
            Destroy(gameObject);
    }
}
