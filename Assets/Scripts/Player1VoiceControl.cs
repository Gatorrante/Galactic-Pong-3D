using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;

public class Player1VoiceControl : MonoBehaviour
{
    public Transform player1;
    public Ball ball;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> actions = new Dictionary<string, System.Action>();

    void Start()
    {
        actions.Add("arriba", MoveUp);
        actions.Add("medio", MoveMiddle);
        actions.Add("abajo", MoveDown);
        actions.Add("tiro", ResetBall);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log("Comando reconocido: " + speech.text);
        actions[speech.text].Invoke();
    }

    void MoveUp()
    {
        player1.position = new Vector3(player1.position.x, player1.position.y, 3f);
    }

    void MoveMiddle()
    {
        player1.position = new Vector3(player1.position.x, player1.position.y, 0f);
    }

    void MoveDown()
    {
        player1.position = new Vector3(player1.position.x, player1.position.y, -3f);
    }

    void ResetBall()
    {
        ball.ResetBall();
    }
}
