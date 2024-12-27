using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InGameManager : MonoBehaviour
{
   public GameObject rocketPrefab;
   public Transform rocketSpawnPoint1, rocketSpawnPoint2;

   public float fireInterval = 0.01f;
   private bool canFire = true;

   public Image healthBarFill;
   public float healthBarChangeTime = 0.5f;

   public GameObject pauseMenu;
   public GameObject deathMenu;

   public AudioSource missileLaunchAudio;


   public void ChangeHealthbar(int maxHealth, int currentHealth)
   {
		  if(currentHealth < 0)
		  return;

		  if(currentHealth == 0)
		  {
				Invoke("OpenDeathMenu", healthBarChangeTime);
		  }

		  float healthPct = currentHealth / (float)maxHealth;
		  StartCoroutine(SmoothHealthbarChange(healthPct));
   }

   private IEnumerator SmoothHealthbarChange(float newFillAmt)
   {
		float elapsed = 0f;
		float oldFillAmt = healthBarFill.fillAmount;
		while(elapsed <= healthBarChangeTime)
		{
			elapsed += Time.deltaTime;
			float currentFillAmt = Mathf.Lerp(oldFillAmt, newFillAmt, elapsed / healthBarChangeTime);
			healthBarFill.fillAmount = currentFillAmt;
			yield return null;

		}
   }
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
		missileLaunchAudio.Play();
		Instantiate(rocketPrefab, rocketSpawnPoint1.position, Quaternion.identity);
		Instantiate(rocketPrefab, rocketSpawnPoint2.position, Quaternion.identity);

   }

   private IEnumerator ReloadDelay()
   {
		 yield return new WaitForSeconds(fireInterval);
		 canFire = true;
	}
	
	public void OnMenuButtonClicked()
   {
		Time.timeScale = 1f;  
		SceneManager.LoadScene("MainMenu");
   }
   public void OnQuitButtonClicked()
   {
		  Debug.Log("Quit App");

		  Application.Quit();
   }
   public void OnPauseButtonClicked()
   {
		  Time.timeScale = 0f;
		  pauseMenu.SetActive(true);
   }
   public void OnContinueButtonClicked()
   {
		   Time.timeScale = 1f;
		   pauseMenu.SetActive(false);
   }
   public void OnRestartButtonClicked()
   {
		  Time.timeScale = 1f;
		  SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
   public void OpenDeathMenu()
   {
		  Time.timeScale = 0f;
		  deathMenu.SetActive(true);
   }

}
