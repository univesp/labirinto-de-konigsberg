using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timer = 30;

    [SerializeField] private GameObject _clockPointer;
    private float _angleToRotate;

    [SerializeField] private AudioClip _timeUpSFX;
    private void Update()
    {
        _timer -= Time.deltaTime;

        _angleToRotate = 360.0f * _timer / 30.0f - 360.0f;
        _clockPointer.transform.rotation = Quaternion.Euler(0, 0, _angleToRotate);

        if(_timer <= 0)
        {
            AudioPlayer.Instance.PlaySFX(_timeUpSFX);
            GameState.Instance.EndGame();
        }
    }
}