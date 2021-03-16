using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoistureUpset
{
    public class BonziBuddy : MonoBehaviour
    {
        #region defined positions
        public static Vector2 M1 = new Vector2(0.7779733f, 0.2007445f);
        public static Vector2 MAINMENU = new Vector2(0.6449644f, 0.8017029f);
        public static Vector2 SETTINGS = new Vector2(0.04327124f, 0.216697f);
        public static Vector2 LOGBOOK = new Vector2(0.9575167f, 0.2372429f);
        public static Vector2 MUSICANDMORE = new Vector2(0.2407375f, 0.7695388f);
        public static Vector2 ALTGAMEMODES = new Vector2(0.6959499f, 0.4022391f);
        public static Vector2 ECLIPSE = new Vector2(0.572173f, 0.7421584f);
        public static Vector2 MULTIPLAYERSETUP = new Vector2(0.7007814f, 0.7697079f);
        public static Vector2 CHARSELECT = new Vector2(0.862179f, 0.2109936f);
        #endregion
        public static BonziBuddy buddy;

        Animator a;
        GameObject textBox;
        TextMeshPro text;
        //UnityEngine.UI.Image img;
        //SpriteRenderer spr;
        bool foundMe = true;
        bool firstTime = false;
        float prevY = 0, prevX = 0;
        bool moveUp = false, moveDown = false, moveLeft = false, moveRight = false;
        bool debugging = false;
        string currentClip = "";
        public bool atDest = true;
        public Vector2 dest;
        public Vector2 screenPos;
        void Start()
        {
            a = GetComponentInChildren<Animator>();
            prevX = transform.position.x;
            prevY = transform.position.y;
            text = GetComponentInChildren<TextMeshPro>();
            textBox = text.gameObject.transform.parent.gameObject;
            text.gameObject.layer = 5;
            textBox.layer = 5;
            text.gameObject.transform.localPosition = new Vector3(0.06f, 0, -.1f);
            dest = new Vector2(.5f, .5f);
            //img = GetComponentInChildren<UnityEngine.UI.Image>();
            //spr = GetComponent<SpriteRenderer>();
            textBox.SetActive(false);
        }

        bool AlmostEqual(float a, float b, float threshold)
        {
            return Math.Abs(a - b) <= threshold;
        }
        void Update()
        {
            //img.sprite = spr.sprite;
            //every frame, set img global position to sprite screenPos * width and height
            //img scale is 2 2 2

            //if (img.sprite == null)
            //{
            //    img.transform.localScale = Vector3.zero;
            //}
            //else
            //{
            //    img.transform.localScale = new Vector3(0.02059911f, 0.02059911f, 0.02059911f);
            //}
            if (firstTime && !speaking)
            {
                Vector2 temp = RectTransformUtility.WorldToScreenPoint(Camera.current, transform.position);
                screenPos = new Vector2(temp.x / (float)Screen.width, temp.y / (float)Screen.height);
                if (a.GetCurrentAnimatorClipInfo(0).Length != 0)
                {
                    currentClip = a.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                }

                bool equalX = AlmostEqual(dest.x, screenPos.x, .0017f);
                bool equalY = AlmostEqual(dest.y, screenPos.y, .0017f);
                atDest = equalX && equalY;
                moveDown = moveUp = moveLeft = moveRight = false;
                if (!atDest && currentClip != "entrance" && currentClip != "leave" && !debugging)
                {
                    if (dest.x > screenPos.x && !equalX)
                    {
                        moveRight = true;
                        if (currentClip == "flyright")
                            transform.position += new Vector3(2 * Time.deltaTime * (Screen.width / 1920.0f), 0, 0);
                    }
                    else if (dest.x < screenPos.x && !equalX)
                    {
                        moveLeft = true;
                        if (currentClip == "flyleft")
                            transform.position -= new Vector3(2 * Time.deltaTime * (Screen.width / 1920.0f), 0, 0);
                    }
                    else if (dest.y > screenPos.y && !equalY)
                    {
                        moveUp = true;
                        if (currentClip == "flyup")
                            transform.position += new Vector3(0, 2 * Time.deltaTime * (Screen.height / 1080.0f), 0);
                    }
                    else if (dest.y < screenPos.y && !equalY)
                    {
                        moveDown = true;
                        if (currentClip == "flydown")
                            transform.position -= new Vector3(0, 2 * Time.deltaTime * (Screen.height / 1080.0f), 0);
                    }
                }
                DebugMovement();
                IdleAnimation();


                MovingAnimations();
            }
        }
        public void MainMenuMovement(string location)
        {
            switch (location)
            {
                case "MultiplayerMenu2":
                    GoTo(MULTIPLAYERSETUP);
                    break;
                case "ExtraGameModeMenu":
                    GoTo(ALTGAMEMODES);
                    break;
                case "TitleMenu":
                    GoTo(MAINMENU);
                    break;
                case "MoreMenu":
                    GoTo(MUSICANDMORE);
                    break;
                case "MainSettings":
                    GoTo(SETTINGS);
                    break;
                default:
                    break;
            }
        }
        float timer = 20f;
        private void IdleAnimation()
        {
            if (currentClip == "idle")
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    //timer = UnityEngine.Random.Range(20, 41);
                    timer = 5;
                    a.SetInteger("idle", UnityEngine.Random.Range(0, 11));
                }
            }
            else
            {
                a.SetInteger("idle", -1);
            }
        }
        private void MovingAnimations()
        {
            if (currentClip != "entrance" && currentClip != "leave")
            {
                if (prevY > transform.position.y || moveDown)
                {
                    //down
                    if (currentClip != "flydown" && currentClip != "flydownstart")
                    {
                        a.Play("flydownstart");
                    }
                    a.SetBool("moving", true);
                }
                else if (prevY < transform.position.y || moveUp)
                {
                    //up
                    if (currentClip != "flyup" && currentClip != "flyupstart")
                    {
                        a.Play("flyupstart");
                    }
                    a.SetBool("moving", true);
                }
                else if (prevX > transform.position.x || moveLeft)
                {
                    //left
                    if (currentClip != "flyleft" && currentClip != "flyleftstart")
                    {
                        a.Play("flyleftstart");
                    }
                    a.SetBool("moving", true);
                }
                else if (prevX < transform.position.x || moveRight)
                {
                    //right
                    if (currentClip != "flyright" && currentClip != "flyrightstart")
                    {
                        a.Play("flyrightstart");
                    }
                    a.SetBool("moving", true);
                }
                else
                {
                    a.SetBool("moving", false);
                }
                prevX = transform.position.x;
                prevY = transform.position.y;
            }
        }
        private void DebugMovement()
        {
            if (Input.GetKey(KeyCode.I))
            {
                moveUp = true;
                //if (currentClip == "flyup")
                transform.position += new Vector3(0, 1 * Time.deltaTime * (Screen.height / 1080.0f), 0);
            }
            if (Input.GetKey(KeyCode.J))
            {
                moveLeft = true;
                //if (currentClip == "flyleft")
                transform.position -= new Vector3(1 * Time.deltaTime * (Screen.width / 1920.0f), 0, 0);
            }
            if (Input.GetKey(KeyCode.K))
            {
                moveDown = true;
                //if (currentClip == "flydown")
                transform.position -= new Vector3(0, 1 * Time.deltaTime * (Screen.height / 1080.0f), 0);
            }
            if (Input.GetKey(KeyCode.L))
            {
                moveRight = true;
                //if (currentClip == "flyright")
                transform.position += new Vector3(1 * Time.deltaTime * (Screen.width / 1920.0f), 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                DebugClass.Log($"public static Vector2 nameathanNamestar = new Vector2({screenPos.x}f, {screenPos.y}f);");
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                debugging = !debugging;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                StartCoroutine(Speak("This is a test to see where my textbox will be."));
            }
        }
        public static void GoTo(float x, float y)
        {
            GoTo(new Vector2(x, y));
        }
        public static void GoTo(Vector2 pos)
        {
            buddy.dest = pos;
        }
        public void Setup()
        {
            if (foundMe && !firstTime)
            {
                StartCoroutine(StartAnimation());
            }
        }
        public IEnumerator StartAnimation()
        {
            a.Play("entrance");
            //yield return new WaitUntil(() => true);
            yield return new WaitUntil(() => a.GetCurrentAnimatorClipInfo(0).Length != 0);
            yield return new WaitUntil(() => a.GetCurrentAnimatorClipInfo(0)[0].clip.name == "entrance");
            firstTime = true;
            GetComponent<RectTransform>().SetAsLastSibling();
            if (SceneManager.GetActiveScene().name == "title")
            {
                GoTo(MAINMENU);
            }
        }
        bool twostep = true;
        public IEnumerator Speak(string whatToSay)
        {
            textBox.SetActive(true);
            text.text = whatToSay;
            yield return new WaitForSeconds(.1f);
            //this is a really long test 1this is a really long test2this is a really long test3this is a really long test4this is a really long test5this is a really long test6this is a really long test7this is a really long test8 this is a really long test9this is a really long test10
            int num = text.firstOverflowCharacterIndex;
            if (text.isTextOverflowing)
            {
                text.text = whatToSay.Remove(num);
                whatToSay = whatToSay.Remove(0, num);
                twostep = true;
                StartCoroutine(loadsong(text.text));
                yield return new WaitUntil(() => !speaking && !twostep);
                textBox.SetActive(false);
                StartCoroutine(Speak(whatToSay));
            }
            else
            {
                text.text = whatToSay;
                twostep = true;
                StartCoroutine(loadsong(text.text));
                yield return new WaitUntil(() => !speaking && !twostep);
                textBox.SetActive(false);
            }
        }
        public bool isLocked(FileInfo file)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                bool flag = fileStream != null;
                if (flag)
                {
                    fileStream.Close();
                }
            }
            return false;
        }
        public IEnumerator loadsong(string text)
        {
            if (!speaking)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"/C del BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav";
                process.StartInfo = startInfo;
                process.Start();

                yield return new WaitUntil(() => !File.Exists("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav"));

                process = new System.Diagnostics.Process();
                startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";

                if (BigJank.getOptionValue("Original Bonzi Buddy TTS") == 1)
                {
                    startInfo.Arguments = $"/C BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\balcon.exe -n Sidney -t \"{text}\" -p 60 -s 140 -w BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav";
                }
                else
                {
                    startInfo.Arguments = $"/C BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\balcon.exe -n \"Microsoft David Desktop\" -t \"{text}\" -p 10 -s \"-2\" -w BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav";
                }
                process.StartInfo = startInfo;
                process.Start();

                yield return new WaitUntil(() => File.Exists("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav"));
                FileInfo file = new FileInfo("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav");
                yield return new WaitUntil(() => !isLocked(file));

                if (text == "stop")
                {
                    speaking = false;
                    a.SetBool("speaking", false);
                    twostep = false;
                    AkSoundEngine.ExecuteActionOnEvent(3183910552, AkActionOnEventType.AkActionOnEventType_Stop);

                }
                else
                {
                    a.Play("speaking");
                    a.SetBool("speaking", true);
                    speaking = true;

                    //AkExternalSourceInfo source = new AkExternalSourceInfo();
                    //source.iExternalSrcCookie = AkSoundEngine.GetIDFromString("TestTTSAudio");
                    //source.szFile = "joemama.wav";
                    //source.idCodec = AkSoundEngine.AKCODECID_PCM;

                    //AkSoundEngine.PostEvent("TestTTSAudio", GameObject.FindObjectOfType<GameObject>(), 0, null, null, 1, source);
                    //Debug.Log($"--------postaudioevent");

                    AkAudioInputManager.PostAudioInputEvent("ttsInput", GameObject.FindObjectOfType<GameObject>(), WavBufferToWwise, BeforePlayingAudio);
                }
            }
        }
        private static bool speaking = false;
        private uint length = 0;

        float[] left, right;

        public static void FixTTS(bool yeet)
        {
            if (!yeet)
            {
                string s = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Windows);
                string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (!File.Exists(s + "\\Speech\\speech.dll"))
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.Verb = "runas";
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = $"/C copy \"{path}\\MetrosexualFruitcake-MoistureUpset\\speech.notadeeellell\" \"{s + "\\Speech\\speech.dll"}\"";
                    process.StartInfo = startInfo;
                    process.Start();
                }
            }
        }
        private bool WavBufferToWwise(uint playingID, uint channelIndex, float[] samples)
        {
            if (left.Length <= 0)
            {
                DebugClass.Log("There was an error playing the audio file, The audio buffer is empty!");
            }


            // probably redundant now tbh.
            if (length >= (uint)left.Length)
            {
                length = (uint)left.Length;
            }

            // DebugClass.Log($"Samples: {samples.Length}, Left: {left.Length}, Current: {length}");

            try
            {
                uint i = 0;

                for (i = 0; i < samples.Length; ++i)
                {
                    if (i + length >= left.Length)
                    {
                        speaking = false;
                        a.SetBool("speaking", false);
                        twostep = false;
                        AkSoundEngine.ExecuteActionOnEvent(3183910552, AkActionOnEventType.AkActionOnEventType_Stop);
                        length = 0;
                        left = right = new float[0];
                        break;
                    }
                    samples[i] = left[i + length];
                }
                length += i;
            }
            catch (Exception)
            {
                Debug.Log($"--------end of audio???");
                throw;
            }
            if (length >= (uint)left.Length)
            {
                length = (uint)left.Length;
                speaking = false;
                a.SetBool("speaking", false);
                twostep = false;
            }

            //DebugClass.Log($"id:{playingID}, samples: {samples}, channlIndex: {channelIndex}");

            return speaking;
        }
        private void BeforePlayingAudio(uint playingID, AkAudioFormat format)
        {
            uint samplerate, channels;

            left = right = new float[0];

            readWav("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav", out left, out right, out samplerate, out channels);

            format.channelConfig.uNumChannels = channels;
            format.uSampleRate = samplerate;
        }


        // Brought to you by StackOverflow, modified by brain damage.
        static bool readWav(string filename, out float[] L, out float[] R, out uint samplerate, out uint channels)
        {
            L = R = null;

            samplerate = 0;
            channels = 0;

            try
            {
                using (FileStream fs = File.Open(filename, FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(fs);

                    // chunk 0
                    int chunkID = reader.ReadInt32();
                    int fileSize = reader.ReadInt32();
                    int riffType = reader.ReadInt32();


                    // chunk 1
                    int fmtID = reader.ReadInt32();
                    int fmtSize = reader.ReadInt32(); // bytes for this chunk (expect 16 or 18)

                    // 16 bytes coming...
                    int fmtCode = reader.ReadInt16();
                    int Channels = reader.ReadInt16();
                    int sampleRate = reader.ReadInt32();
                    int byteRate = reader.ReadInt32();
                    int fmtBlockAlign = reader.ReadInt16();
                    int bitDepth = reader.ReadInt16();

                    samplerate = (uint)sampleRate;
                    channels = (uint)Channels;

                    if (fmtSize == 18)
                    {
                        // Read any extra values
                        int fmtExtraSize = reader.ReadInt16();
                        reader.ReadBytes(fmtExtraSize);
                    }

                    // chunk 2
                    int dataID = reader.ReadInt32();
                    int bytes = reader.ReadInt32();

                    // DATA!
                    byte[] byteArray = reader.ReadBytes(bytes);

                    int bytesForSamp = bitDepth / 8;
                    int nValues = bytes / bytesForSamp;


                    float[] asFloat = null;
                    switch (bitDepth)
                    {
                        case 64:
                            double[]
                                asDouble = new double[nValues];
                            Buffer.BlockCopy(byteArray, 0, asDouble, 0, bytes);
                            asFloat = Array.ConvertAll(asDouble, e => (float)e);
                            break;
                        case 32:
                            asFloat = new float[nValues];
                            Buffer.BlockCopy(byteArray, 0, asFloat, 0, bytes);
                            break;
                        case 16:
                            Int16[]
                                asInt16 = new Int16[nValues];
                            Buffer.BlockCopy(byteArray, 0, asInt16, 0, bytes);
                            asFloat = Array.ConvertAll(asInt16, e => e / (float)(Int16.MaxValue + 1));
                            break;
                        default:
                            return false;
                    }

                    switch (channels)
                    {
                        case 1:
                            L = asFloat;
                            R = null;
                            return true;
                        case 2:
                            // de-interleave
                            int nSamps = nValues / 2;
                            L = new float[nSamps];
                            R = new float[nSamps];
                            for (int s = 0, v = 0; s < nSamps; s++)
                            {
                                L[s] = asFloat[v++];
                                R[s] = asFloat[v++];
                            }
                            return true;
                        default:
                            return false;
                    }
                }
            }
            catch
            {
                Debug.Log("...Failed to load: " + filename);
                return false;
            }

            return false;
        }
    }
}
