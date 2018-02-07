using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpeechRecognitionEngine : MonoBehaviour
{
    public string[] keywords = new string[] { "Alfa", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliett", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "Xray", "Yankee", "Zulu" };
    public string[] armyAlphabet = new string[] { "Alfa", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliett", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "Xray", "Yankee", "Zulu", "Wow", "Wauw", "Waw", "IJsfontein", "Fuck", "Shit", "Motel", "Pappa", "Poppa", "Wise key", "Sulu", "Silly", "Nice", "Lime", "Alfa Romeoo", "Tongo" };
    public string[] armyAlphabetWithDistractions = new string[] { "Alfa", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliett", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "Xray", "Yankee", "Zulu", "Wow", "Wauw", "Waw", "IJsfontein", "Fuck", "Shit", "Motel", "Pappa", "Poppa", "Wise key", "Sulu", "Silly", "Nice", "Lime", "Alfa Romeoo", "Tongo" };
    public ConfidenceLevel confidence = ConfidenceLevel.Low;
    public float speed = 1;

    public Text results;
    public Image target;

    protected PhraseRecognizer recognizer;
    protected string word = "right";

    private void Start()
    {
        if (armyAlphabetWithDistractions != null)
        {
            recognizer = new KeywordRecognizer(armyAlphabetWithDistractions, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        results.text = "You said: <b>" + word + "</b>";
    }

    private void Update()
    {
        var x = target.transform.position.x;
        var y = target.transform.position.y;

        switch (word)
        {
            case "up":
                y += speed;
                break;
            case "down":
                y -= speed;
                break;
            case "left":
                x -= speed;
                break;
            case "right":
                x += speed;
                break;
        }

        target.transform.position = new Vector3(x, y, 0);
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
