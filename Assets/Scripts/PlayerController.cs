using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour {

    public MicrophoneInitializer _micInit;

    public int sensitivity = 5000;

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

    public EventList flipEventList = new EventList();

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
        
		this.moveRight ();

        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere

        micLoudness = _micInit.GetAveragedVolume() * sensitivity;

        //Debug.Log(micLoudness);

        if (micLoudness > micSensitivity && !_isJumping) HandleJump();
        HandleJumpGravity();
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
            _isJumping = false;
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

    public void FlipPlayer()
    {
        if (_isGroundedBottom || _isGroundedTop)
        {   
            flipEventList.Execute();

            _isFlipped = !_isFlipped;

            _spriterenderer.flipY = _isFlipped;

            _rigidbody2D.gravityScale *= -1;
        }        
    }

    public bool isPlayerInRedPitch(float[] heightsDistribution)
    {
        float[] redPitches = { heightsDistribution[8], heightsDistribution[9], heightsDistribution[10] };
        float[] normalPitches = {
            heightsDistribution[0],
            heightsDistribution[1],
            heightsDistribution[2],
            heightsDistribution[3],
            heightsDistribution[4],
            heightsDistribution[5],
            heightsDistribution[6],
            heightsDistribution[7],
            heightsDistribution[11],
            heightsDistribution[12],
            heightsDistribution[13],
            heightsDistribution[14],
            heightsDistribution[15],
            heightsDistribution[16],
            heightsDistribution[17],
            heightsDistribution[18],
            heightsDistribution[19],
            heightsDistribution[20],
            heightsDistribution[21],
            heightsDistribution[22],
            heightsDistribution[23],
            heightsDistribution[24],
            heightsDistribution[25],
            heightsDistribution[26],
            heightsDistribution[27],
            heightsDistribution[28],
            heightsDistribution[29]
        };

        bool redPitchesUberAlles = true;
        foreach (float currentNormalPitch in normalPitches)
        {
            bool isAbovePitch = false;
            foreach (float currentRedPitch in redPitches)
            {
                if (currentRedPitch > currentNormalPitch * 1.5)
                {
                    isAbovePitch = true;
                }
            }
            if (!isAbovePitch)
            {
                redPitchesUberAlles = false;
            }
        }
        return redPitchesUberAlles;
    }
}
