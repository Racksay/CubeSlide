using UnityEngine;

namespace Assets.Scripts
{
	public class Finish : MonoBehaviour
	{
		public void OnTriggerEnter2D( Collider2D other )
		{
			Debug.Log("Entered");
			gameObject.GetComponent<MeshRenderer>().enabled = true;
			gameObject.GetComponent<TextMesh>().text = "YOU WIN!!!\nFINAL SCORE:\n" + other.gameObject.GetComponent<Controller>().TotalScore;
		}

	}
}
