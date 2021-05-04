using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Score : MonoBehaviour
{
    [SerializeField] private float _changeSpeed;

    private TMP_Text _text;

    private float _realScore = 0f;
    private float _actualScore = 0f;

    private IEnumerator _scoreCoroutine;

    public void OnAddScore(float score)
    {
        _realScore += score;

        if (_scoreCoroutine != null)
        {
            StopCoroutine(_scoreCoroutine);
        }

        _scoreCoroutine = ChangeScore();

        StartCoroutine(_scoreCoroutine);
    }

    private void Awake() => _text = GetComponent<TMP_Text>();

    private void Start() => _text.text = $"Score: {_realScore}";

    private IEnumerator ChangeScore()
    {
        while (_actualScore != _realScore)
        {
            _actualScore = Mathf.Lerp(_actualScore, _realScore, Time.deltaTime * _changeSpeed);

            _text.text = "Score: " + _actualScore.ToString("###,###,###");

            yield return null;
        }

        _scoreCoroutine = null;
    }
}
