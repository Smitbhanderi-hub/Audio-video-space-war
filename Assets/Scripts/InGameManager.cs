using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InGameManager : MonoBehaviour
{
   public GameObject rocketPrefab;
   public Transform rocketSpawnPoint1, rocketSpawnPoint2;

   public float fireInterval = 2f;
   private bool canFire = true;

   public void OnFiretButtonClicked()
   {
		  if(canFire)
		  {
				FireRockets();

				canFire = false;

				StartCoroutine(ReloadDelay());
		  }
   }
   private void FireRockets()
   {
		Instantiate(rocketPrefab, rocketSpawnPoint1.position, Quaternion.identity);
		Instantiate(rocketPrefab, rocketSpawnPoint2.position, Quaternion.identity);

   }

   private IEnumerator ReloadDelay()
   {
		 yield return new WaitForSeconds(fireInterval);
		 canFire = true;
	}
	
	public void OnExitButtonClicked()
   {
		  SceneManager.LoadScene("SampleScene");
   }
}
