using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runtime2DMovement : MonoBehaviour
{
    private bool _moveRight;
    private bool _moveLeft;
    private bool _isJumping;
    private bool _isGrounded;
    private bool _stopMovement = false;

    private Rigidbody2D rb;
    private float jumpTimeCounter;
    private Vector2 _velocity = new Vector2(0.0f, 0.0f);
    private float _timeLeft;
    private float _declaration = 0.0f;
    private float _elaspedTimeSinceButtonPress;
    private bool _isDead = false;
    [HideInInspector]
    public bool _invincible = false;

    // VARIALBES THE USER CAN EDIT TO CREATE DIFFERENT JUMP ARCS/MOVEMENT
    public string _walkableSurfaceTagName;
    public int gravityScale;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.Space;
    public float impluseJumpVel = 4.0f;
    public float TimeToReachMaxHeight = 0.5f;
    public float _movementTime = 0.100f;
    public float _MAX_WALKING_SPEED = 5.0f;
    public float acclearation = 17.0f;
    public float _LOWEST_WALKING_SPEED = 0.3f;
    public int _health = 10;
    public int _MAX_HEALTH = 15;
    public float _hurtTimer = 0.25f;
    public float _invincibleTimer = 2.0f;
    public float _damagedFlashRate = 0.25f;
    // VARIALBES THE USER CAN EDIT TO CREATE DIFFERENT JUMP ARCS/MOVEMENT

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        // CHECK IF THE GAMEOBJECT HAS A RIGIDBODY IF NOT THEN CREATE ONE
        if (!rb)
        {
            gameObject.AddComponent<Rigidbody2D>();
            rb = GetComponent<Rigidbody2D>();
            rb.angularDrag = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.gravityScale = gravityScale;
        }
        else
        {
            rb.angularDrag = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.gravityScale = gravityScale;
        }

        // CHECK IF THE GAME OBJECT HAS A BOX COLLDER ATTACHED, IF NOT THEN CREATE ONE.
        if (!this.GetComponent<BoxCollider2D>())
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == _walkableSurfaceTagName && !_isGrounded)
        {
             _isGrounded = true;
            _isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            decreaseHealth(2);
        }
    }


    private void OnCollisionExit2D(Collision2D walkableSurface)
    {
        if (walkableSurface.gameObject.tag == _walkableSurfaceTagName && _isGrounded)
        {
            _isGrounded = false;
        }
    }

    public void setStopMovement(bool t_stopMovement)
    {
        _stopMovement = t_stopMovement;
    }
	
	public void setWalkRight(bool t_moveRight)
	{
		_moveRight = t_moveRight;
	}
	
	public void setWalkLeft(bool t_moveLeft)
	{
		_moveLeft = t_moveLeft;
	}

    public void setIsJumping(bool t_isJumping)
    {
        _isJumping = t_isJumping;
    }

    public bool getIsGrounded()
    {
        return _isGrounded;
    }

    public void setIsGrounded(bool t_isGrounded)
    {
        _isGrounded =t_isGrounded;
    }

    public bool getIsMovingRight()
    {
        return _moveRight;
    }

    public bool getIsMovingLeft()
    {
        return _moveLeft;
    }

    public bool getIsJumping()
    {
        return _isJumping;
    }

    public float getTimeSinceLastButtonPress()
    {
        return _elaspedTimeSinceButtonPress;
    }

    public void setTimeSinceLastButtonPress(float t_timeSinceLastUpdate)
    {
        _elaspedTimeSinceButtonPress = t_timeSinceLastUpdate;
    }

    public float getTimeLeft()
    {
        return _timeLeft;
    }

    public void setTimeLeft(float t_timeLeft)
    {
        _timeLeft = t_timeLeft;
    }

    public Vector2 getVelocity()
    {
        return _velocity;
    }

    public void setVelocity(Vector2 t_velocity)
    {
        _velocity = t_velocity;
    }

    public float getDeclaration()
    {
        return _declaration;
    }

    public void setDeclaration(float t_declaration)
    {
        _declaration = t_declaration;
    }

    public Rigidbody2D getRigidBody()
    {
        return rb;
    }

    public void setRigidBodyVelocity(Vector2 t_velocity)
    {
        rb.velocity = t_velocity;
    }

    public void setJumpTimeCounter(float t_jumpTimeCounter)
    {
        jumpTimeCounter = t_jumpTimeCounter;
    }

    public float getJumpTimeCounter()
    {
        return jumpTimeCounter;
    }

    public void addHealth(int t_healthAddition)
    {
        if (_health + t_healthAddition >= _MAX_HEALTH) { _health = _MAX_HEALTH; }
        else
        {
            _health += t_healthAddition;
        }
    }
    public bool getIsDead()
    {
        return _isDead;
    }

    public void setIsDead(bool t_isDead)
    {
        _isDead = t_isDead;
    }

    public void decreaseHealth(int t_healthSubtraction)
    {
        if (!_invincible)
        {
            if (_health - t_healthSubtraction <= 0)
            {
                _health = 0;
            }
            else
            {
                _health -= t_healthSubtraction;
                _invincible = true;
                StartCoroutine(damagedStateTime());
            }
        }
    }

    IEnumerator invincibilityTime()
    {
        StartCoroutine(invincibilityFlash());

        yield return new WaitForSeconds(_invincibleTimer);

        _invincible = false;
    }

    IEnumerator damagedStateTime()
    {
        yield return new WaitForSeconds(_hurtTimer);

        StartCoroutine(invincibilityTime());
    }

    IEnumerator invincibilityFlash()
    {
        while (_invincible)
        {
            GetComponent<Renderer>().enabled = false;

            yield return new WaitForSeconds(_damagedFlashRate);

            GetComponent<Renderer>().enabled = true;

            yield return new WaitForSeconds(_damagedFlashRate);
        }
        GetComponent<Renderer>().enabled = true;
    }
}
