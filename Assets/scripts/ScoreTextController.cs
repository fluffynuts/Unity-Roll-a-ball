using UnityEngine;

public class ScoreTextController : MonoBehaviour
{
    private Vector3 _offset;
    public GameObject Player;

    // Use this for initialization
    private void Start()
    {
        _offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = Player.transform.position + _offset;
    }
}