using UnityEngine;

namespace Assets.Scripts
{
	public class OutOfBounds : MonoBehaviour
	{

		public void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log( "Entered trigger" );
			other.gameObject.GetComponent<Controller>().Start();
		}

	}
}
