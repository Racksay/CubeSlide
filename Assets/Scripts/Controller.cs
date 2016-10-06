using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class Controller : MonoBehaviour
	{
		public Text ScoreText;
		public TextMesh BonusScoreText;
		public bool CanDoubleJump;
		public bool IsGrounded;
		public int TotalScore;

		private Rigidbody2D _rigidbody;
		private Vector3 _modelOffset;
		private int _score;
		private int _slideScore;
		private int _totalSlideScore;
		private bool _eligibleSlideScore;
		private Vector3 _groundedStartPos;

		public void Start ()
		{
			transform.position = new Vector3( 0, 0, -10);
			_rigidbody = GetComponent<Rigidbody2D>();
			_rigidbody.velocity = new Vector2(0,0);
			_modelOffset = transform.position - new Vector3( 0, 0.55f );
			_score = 0;
			_eligibleSlideScore = false;
			_totalSlideScore = 0;
			_groundedStartPos = new Vector3(0,0);
			IsGrounded = false;
			BonusScoreText.text = "";
		}
	
		// Update is called once per frame
		void Update ()
		{
			InputLeftRight();
			InputJump();

			CalculateScore();

			DrawUI();
		}

		private void CalculateScore()
		{
			if (IsGrounded && Math.Abs(_groundedStartPos.x) < 0.1)
			{
				_groundedStartPos = transform.position;
				_eligibleSlideScore = true;
			}

			if ((int)(transform.position.x - _groundedStartPos.x) > 0 && _score < (int)transform.position.x)
				_slideScore = (int)((transform.position.x - _groundedStartPos.x)*_rigidbody.velocity.x);

			if (_eligibleSlideScore && !IsGrounded)
			{
				_totalSlideScore += _slideScore;
				_eligibleSlideScore = false;
			}

			
			if (_score < (int)transform.position.x)
				_score = (int)transform.position.x + 1;

			if (!IsGrounded)
			{
				_groundedStartPos = new Vector3( 0, 0 );
				_slideScore = 0;
			}
		}

		private void DrawUI()
		{
			TotalScore = (_score*12 + _totalSlideScore);
			ScoreText.text = "SCORE: " + TotalScore;
			if (IsGrounded)
			{
				BonusScoreText.text = "Bonus: " + _slideScore;
			}
			else
			{
				BonusScoreText.text = "";
			}
		}

		private void InputJump()
		{
			_modelOffset = transform.position - new Vector3( 0, 0.55f );
			var groundCheck = Physics2D.Raycast( _modelOffset, Vector2.down, float.PositiveInfinity );
			IsGrounded = groundCheck.distance < 0.1;

			if (Input.GetKeyDown( KeyCode.Space ) && groundCheck.collider.tag != "WindTurbine")
			{
				if (CanDoubleJump && !IsGrounded)
				{	// Double Jump
					_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 30);
					CanDoubleJump = false;
				}
				else if(IsGrounded)
				{	// Single Jump
					_rigidbody.AddForce( new Vector2( 0, 1500 ) );
				}
			}
			if (IsGrounded)
			{
				CanDoubleJump = true;
			}
		
			// DEBUG drawing and logging
			Debug.DrawRay( _modelOffset, Vector3.down, Color.green );
			//Debug.Log( groundCheck.distance + ", " + _isGrounded + ", " + _canDoubleJump );
		
		}

		private void InputLeftRight()
		{
			if (Input.GetKey( KeyCode.A ) && _rigidbody.velocity.x > -50)
			{
				_rigidbody.AddForce(new Vector2(-20, 0));
			}
			if (Input.GetKey( KeyCode.D ) && _rigidbody.velocity.x < 50)
			{
				_rigidbody.AddForce(new Vector2(20, 0));
			}	
		}
	}
}
