using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Required GameObject Refs.")]
    [Space]
    private static PlayerController _playerController;
    private Rigidbody2D _rigidbody2D;

    [Header("Scene Ground Attributes & Checker")]
    [Space]
    public LayerMask groundLayer;
    public Transform groundChecker;
    public float groundCheckerRadius;

    [Space]
    [Header("Player Jump Attributes")]
    [Space]
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    [Space]
    [Header("Player States")]
    [Space]

    [SerializeField]
    private bool _isJumping;

    [SerializeField]
    private bool _isGrounded;

    [Space]
    [Header("Microphone States")]
    [Space]
    private AudioClip _clipRecord = new AudioClip();
    private bool _isInitialized;
    private int _sampleWindow = 128;
    private string _device;

    public static float MicLoudness;

    void Awake()
    {
        _playerController = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        InitMic();
        _isInitialized = true;
    }

    void OnDisable()
    {
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }

    // Update is called once per frame
    void FixedUpdate () {
        Move();
    }

    private void Move()
    {
        _isGrounded = IsGrounded();
        HandleJumpGravity();

        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere

        MicLoudness = LevelMax() * 10000;

        if (MicLoudness > 25) HandleJump(true);
    }

    private bool IsGrounded()
    {
        return _isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, groundLayer);
    }

    private void HandleJump(bool jump)
    {

        if (jump && !_isJumping && _isGrounded)
        {
            _isJumping = true;
            _rigidbody2D.velocity = Vector2.up * jumpForce;

        }
        else
        {
            _isJumping = jump;
        }
    }

    private void HandleJumpGravity()
    {

        if (_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (_rigidbody2D.velocity.y > 0)
        {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    // mic initialization
    void InitMic()
    {
        if (_device == null) _device = Microphone.devices[0];
        _clipRecord = Microphone.Start(_device, true, 999, 44100);
    }

    // get data from microphone into audioclip
    float LevelMax()
    {
        float levelMax = 0;

        float[] waveData = new float[_sampleWindow];

        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;

        _clipRecord.GetData(waveData, micPosition);

        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    void StopMicrophone()
    {
        Microphone.End(_device);
    }
}
