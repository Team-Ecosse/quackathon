using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour {

    [Header("Required GameObject Refs.")]
    [Space]
    private static PlayerController _playerController;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriterenderer;


    [Header("Scene Ground Attributes & Checker")]
    [Space]
    public LayerMask groundLayer;
    public Transform BottomGroundCheckerTransform;
    public Transform TopGroundCheckerTransform;
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
    private bool _isGroundedBottom;
    [SerializeField]
    private bool _isGroundedTop;

    [SerializeField]
    private bool _isFlipped;

    [Space]
    [Header("Microphone States")]
    [Space]
    private AudioClip _clipRecord = new AudioClip();
    private bool _isInitialized;
    private const int _sampleWindow = 128;
    private string _device;

    public int micSensitivity;
    public int micFlipSensitivity;
    public static float micLoudness;

    void Awake()
    {
        _playerController = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriterenderer = GetComponent<SpriteRenderer>();

        InitMic();
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
        _isGroundedBottom = IsGroundedBottom();
        _isGroundedTop = IsGroundedTop();
        
        //HandleJumpGravity();
		this.moveRight ();
        HandleJumpGravity();

        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere

        micLoudness = LevelMax() * 10000;

        if (micLoudness > micSensitivity) HandleJump();
        if (micLoudness > micFlipSensitivity) FlipPlayer();
    }

	private void moveRight () {
		_rigidbody2D.position = new Vector2(this._rigidbody2D.position.x + 0.1f, this._rigidbody2D.position.y);
	}

    private bool IsGroundedBottom()
    {
        return _isGroundedBottom = Physics2D.OverlapCircle(BottomGroundCheckerTransform.position, groundCheckerRadius, groundLayer);
    }
    
    private bool IsGroundedTop()
    {
        return _isGroundedTop = Physics2D.OverlapCircle(TopGroundCheckerTransform.position, groundCheckerRadius, groundLayer);
    }

    private void HandleJump()
    {
        if (!_isJumping && _isGroundedBottom || _isGroundedTop)
        {
            _isJumping = true;
            
            if (!_isFlipped)
            {
                _rigidbody2D.velocity = Vector2.up * jumpForce;
            }
            else
            {
                _rigidbody2D.velocity = Vector2.down * jumpForce;
            }

        }
        else
        {
            _isJumping = false;
        }
    }

    private void HandleJumpGravity()
    {

        if (!_isFlipped)
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
        else
        {
            if (_rigidbody2D.velocity.y < 0)
            {
                _rigidbody2D.velocity += Vector2.down * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            }
            else if (_rigidbody2D.velocity.y > 0)
            {
                _rigidbody2D.velocity += Vector2.down * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
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

    void FlipPlayer()
    {
        _isFlipped = !_isFlipped;

        _spriterenderer.flipY = _isFlipped;

        _rigidbody2D.gravityScale *= -1;
    }
}
