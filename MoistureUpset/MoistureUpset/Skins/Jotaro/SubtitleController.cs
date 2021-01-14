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

        private readonly string[] voiceLines = new string[] { "Good Grief", "Bigma Lalls", "Hahah heehee", "kill yourself" };
        public void DoKillVoiceLine()
        {
            SetSubtitle(voiceLines[UnityEngine.Random.Range(0, 4)], 4f);
        }

        public void SetSubtitle(string text, float duration)
        {
            if (textMeshPro == null)
                textMeshPro = GetComponentInChildren<AddSubtitleBar>().subtitleBar.GetComponentInChildren<TextMeshPro>();

            textMeshPro.text = text;
            DebugClass.Log(text);
            timer = duration;
            StartCoroutine("ClearText");
        }

        IEnumerator ClearText()
        {
            while (timer > 0)
            {
                timer -= 0.05f;
                yield return new WaitForSeconds(0.05f);
            }

            textMeshPro.text = "";
        }
    }
}
