using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   private static bool GamePaused = false;

   public GameObject pauseUI;

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         if (GamePaused)
         {
            Resume();
         }
         else
         {
            Pause();
         }
      }
   }

   private void Resume()
   {
      pauseUI.SetActive(false);
      GamePaused = false;
   }

   private void Pause()
   {
      pauseUI.SetActive(true);
      GamePaused = true;
   }

   public void Exit()
   {
      Debug.Log("Exit");
      Application.Quit();
   }
}
