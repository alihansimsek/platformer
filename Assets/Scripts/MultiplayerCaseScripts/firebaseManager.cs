using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System.Threading.Tasks;
using System;

public class firebaseManager : MonoBehaviour
{
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;  //firebase
    private bool firebaseInitialized = false; //Firebase flag
    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    internal void EnemyDeath()
    {
        // Log killed enemies.
        Debug.Log("EnemyDeath() has been called. Logging an enemy death event.");
        FirebaseAnalytics.LogEvent(
          "Enemy_Killed",
          "Enemy",
          1);
    }


    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Enabling data collection.");
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

        Debug.Log("Set user properties.");
        // Set the user's sign up method.
        FirebaseAnalytics.SetUserProperty(
          FirebaseAnalytics.UserPropertySignUpMethod,
          "Google");
        // Set the user ID.
        FirebaseAnalytics.SetUserId("uber_user_510");
        // Set default session duration values.
        FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
        firebaseInitialized = true;
    }
    public void TokenCollect()
    {
        // Log collected tokens.
        Debug.Log("TokenCollect() has been called. Logging a token collect event.");
        FirebaseAnalytics.LogEvent( 
          "Token_Collected",
          "Token",
          1);
    }

    //METHODS BELOW IS NOT USED IN THIS PROJECT BUT IN CASE OF LOGGING TOTAL NUMBERS OF TOKENS AND EMENIES AFTER PLAYER DIED, THESE CAN BE USED.
    public void TokenCollect(int token)
    {
        // Log collected tokens.
        Debug.Log("TokenCollect() has been called. Logging a token collect event.");
        FirebaseAnalytics.LogEvent(
          "Token_Collected",
          "Token",
          token);
    }

    internal void EnemyDeath(int enemy)
    {
        // Log killed enemies.
        Debug.Log("EnemyDeath() has been called. Logging an enemy death event.");
        FirebaseAnalytics.LogEvent(
          "Enemy_Killed",
          "Enemy",
          enemy);
    }

    public void DeathLog(int token, int enemy)
    {
        TokenCollect(token);
        EnemyDeath(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
