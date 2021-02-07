using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using AK;
using AK.Wwise;
/*
 *  TODO:   * Fix ClearText timer accelerating with each clear.
 *          
 * 
 * 
 */ 

namespace MoistureUpset.Skins.Jotaro
{
    class SubtitleController : MonoBehaviour
    {
        private TextMeshPro textMeshPro;

        private float timer = 0f;

        public void SetSubtitle(string text, float duration)
        {
            if (textMeshPro == null)
                textMeshPro = GetComponentInChildren<AddSubtitleBar>().subtitleBar.GetComponentInChildren<TextMeshPro>();

            textMeshPro.text = text;

            timer = duration;

            StopCoroutine(ClearText());

            StartCoroutine("ClearText");
        }

        IEnumerator ClearText()
        {

            while (timer > 0)
            {
                timer -= 0.01f;

                yield return new WaitForSeconds(0.01f);
            }

            textMeshPro.text = "";
        }
    }
}
