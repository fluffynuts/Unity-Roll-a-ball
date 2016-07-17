using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class PlayerController : MonoBehaviour
{
    private const string VERTICAL_AXIS = "Vertical";
    private const string HORIZONTAL_AXIS = "Horizontal";
    public Text ScoreText;
    public Text WinText;
    public float Speed = 5F;
    public float TableFriction = 5F;
    public float FrictionRampSpeed = 1F;
    public float FrictionRamp = 2.5F;

    private Rigidbody _player;

    // Use this for initialization
    private int _score;
    private Vector3 _playerStart;
    private GameObject[] _allPickups;
    private Color _winTextColor;

    private void Start()
    {
        _player = GetComponent<Rigidbody>();
        _playerStart = _player.position;
        Log("Player created");
        _allPickups = GameObject.FindGameObjectsWithTag("pickup");
        _winTextColor = WinText.color;
        HideWinText();
    }

    private void HideWinText()
    {
        WinText.color = new Color(0, 0, 0, 0);
    }

    private void ShowWinText()
    {
        WinText.color = _winTextColor;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Reset();
        }
    }

    private void Reset()
    {
        _score = 0;
        _player.velocity = new Vector3(0, 0, 0);
        _player.position = _playerStart;
        foreach (var pickup in _allPickups)
        {
            pickup.SetActive(true);
        }
        UpdateScoreDisplay();
        HideWinText();
    }

    void OnTriggerEnter(Collider other)
    {
        var gameObject = other.gameObject;
        Log("Colliding with: {0}", gameObject.tag);
        if (gameObject.CompareTag("pickup"))
        {
            IncrementScore();
            gameObject.SetActive(false);
        }
    }

    private void IncrementScore()
    {
        _score++;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (ScoreText)
        {
            ScoreText.text = string.Format("Score: {0}", _score);
            if (_score == _allPickups.Length)
            {
                ShowWinText();
            }
        }
    }

    // also called once per frame but guaranteed to be called last
    public void FixedUpdate()
    {
        UpdatePlayerForceFromInput();
        ApplyTableSurfaceFriction();
    }

    private void UpdatePlayerForceFromInput()
    {
        var moveX = Input.GetAxis(HORIZONTAL_AXIS);
        var moveY = Input.GetAxis(VERTICAL_AXIS);

        var force = new Vector3(moveX, 0, moveY);
        _player.AddForce(Speed * force);
    }

    private void ApplyTableSurfaceFriction()
    {
        var velocity = _player.velocity;
        var decelX = GetDecellerationFor(velocity.x);
        var decelZ = GetDecellerationFor(velocity.z);
        _player.AddForce(new Vector3(decelX, 0, decelZ));
    }

    private void Log(string message, params object[] args)
    {
        Debug.Log(string.Format(message, args));
    }

    private float GetDecellerationFor(float f)
    {
        var absoluteForce = Math.Abs(f);
        var situationMultiplier = absoluteForce < FrictionRampSpeed ? FrictionRamp : 1;   // the slower you're going, the more friction of the table matters
        var direction = f < 0 ? 1 : -1; // should oppose the current movement
        var max = (float)(TableFriction * direction * situationMultiplier * 0.1);
        return Math.Abs(max) < absoluteForce ? -1 * absoluteForce : max;
    }
}
