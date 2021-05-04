using UnityEngine;
using TMPro;

public class Speed : MonoBehaviour
{
    [SerializeField] private Car _car;

    private TMP_Text _text;


    private void Awake() => _text = GetComponent<TMP_Text>();

    private void Update()
    {
        float speed = _car.Speed * 3.6f;

        _text.text = $"{(int)speed} KM/H";
    }
}
