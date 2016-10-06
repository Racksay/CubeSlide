using UnityEngine;

namespace Assets.Scripts
{
	public class Rotate : MonoBehaviour
	{

		public float RotationSpeed;

		void Update ()
		{
			transform.Rotate( new Vector3( 0, 0, RotationSpeed ) );
		}
	}
}
