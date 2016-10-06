using UnityEngine;

namespace Assets.Scripts
{
	public class WindTurbine : MonoBehaviour
	{
		private Rigidbody2D _rigidbody2D;

		public void OnTriggerEnter2D( Collider2D other )
		{
			_rigidbody2D = other.GetComponent<Rigidbody2D>();
			_rigidbody2D.gravityScale = 0.1f;
			_rigidbody2D.mass = 0.25f;
		}

		public void OnTriggerStay2D( Collider2D other )
		{
			if (_rigidbody2D.velocity.y < 20)
			{
				_rigidbody2D.AddForce(new Vector2(0, 20));
			}
		}

		public void OnTriggerExit2D( Collider2D other )
		{
			_rigidbody2D.gravityScale = 5.0f;
			_rigidbody2D.mass = 1f;
		}
	}
}
