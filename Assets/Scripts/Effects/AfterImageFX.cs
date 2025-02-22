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
        float alpha = sr.color.a - colorLooseRate * Time.deltaTime; // 表示颜色的透明度，值范围从 0 到 1。0 表示完全透明，1 表示完全不透明。
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha); // RGB 值保持不变，但透明度 alpha 被更新为计算出的新值

        if (sr.color.a <= 0)
            Destroy(gameObject);
    }
}
