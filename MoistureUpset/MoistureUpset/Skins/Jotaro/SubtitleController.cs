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

        private static readonly string[] voiceLines = new string[] { "Good Grief", "Bigma Lalls", "Hahah heehee", "kill yourself", "I can't beat the shit out of you without getting closer" };

        public void EventCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
        {
            if (in_type != AkCallbackType.AK_Marker)
                return;
            AkMarkerCallbackInfo akMarker = (AkMarkerCallbackInfo)in_info;

            DebugClass.Log(akMarker.strLabel);

            //SetSubtitle(voiceLines[UnityEngine.Random.Range(0, voiceLines.Length)], 4f);

            SetSubtitle(akMarker.strLabel, 4f);
        }

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
