using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpeechRecognitionEngine : MonoBehaviour
{
    public string[] codes;
    public string[] armyAlphabetWithDistractions = new string[] { "Alfa", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliett", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "Xray", "Yankee", "Zulu", "Wow", "Wauw", "Waw", "IJsfontein", "Fuck", "Shit", "Motel", "Pappa", "Poppa", "Wise key", "Sulu", "Silly", "Nice", "Lime", "Alfa Romeo", "Tongo" };
    public ConfidenceLevel confidence = ConfidenceLevel.Low;
    public float speed = 1;

    public Image background;
    public Text results;
    public Text[] targets;
    public AudioClip doorOpenSound;
    public AudioClip[] lockSounds;
    public AudioClip[] positiveSounds;
    public AudioClip[] negativeSounds;
    public AudioSource soundPlayer;

    protected PhraseRecognizer recognizer;
    protected string word;
    protected int codeIndex = 0;

    private void Start()
    {
        if (armyAlphabetWithDistractions != null)
        {
            recognizer = new KeywordRecognizer(armyAlphabetWithDistractions, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();

        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            InsertNextCode();
        }

    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        results.text = "You said: <b>" + word + "</b>";

        if (codes[codeIndex] == word)
        {
            InsertNextCode();
        }
        else
        {
            soundPlayer.PlayOneShot(negativeSounds[Random.Range(0, negativeSounds.Length)]);
        }

    }

    private void InsertNextCode()
    {
        targets[codeIndex].text = codes[codeIndex].Substring(0,1);
        targets[codeIndex].gameObject.SetActive(true);
        codeIndex++;


        if (codeIndex == 9)
        {
            background.color = Color.green;
            soundPlayer.PlayOneShot(lockSounds[1]);
            soundPlayer.PlayOneShot(doorOpenSound);

            results.text = "Door unlocked!";

            recognizer.Stop();

            soundPlayer.PlayOneShot(positiveSounds[Random.Range(0, positiveSounds.Length)]);
        }
        else
        {
            soundPlayer.PlayOneShot(lockSounds[0]);
        }

    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }
}
