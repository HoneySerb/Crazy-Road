using UnityEngine.SceneManagement;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerInfo;
    [SerializeField] private GameObject[] _pauseObjects;

    private bool _isPaused = false;


    public void ChangePauseState()
    {
        _isPaused = !_isPaused;

        foreach (GameObject obj in _playerInfo)
        {
            obj.SetActive(!_isPaused);
        }

        foreach (GameObject obj in _pauseObjects)
        {
            obj.SetActive(_isPaused);
        }

        Time.timeScale = _isPaused ? 0f : 1f;
    }

    public void Restart() => SceneManager.LoadScene("Play");

    public void Exit() => Application.Quit();

    private void Start() => ChangePauseState();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePauseState();
        }
    }
}
