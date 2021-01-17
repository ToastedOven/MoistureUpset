using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace MoistureUpset.Skins.Jotaro
{
    class SubtitleController : MonoBehaviour
    {
        private TextMeshPro textMeshPro;

        private float timer = 0f;


        private readonly string[] voiceLines = new string[] { "Good Grief", "Bigma Lalls", "Hahah heehee", "kill yourself", "I can't beat the shit out of you without getting closer" };

        public void DoKillVoiceLine()
        {
            SetSubtitle(voiceLines[UnityEngine.Random.Range(0, voiceLines.Length)], 4f);
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
