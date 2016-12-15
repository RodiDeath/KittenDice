using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LifesManager : MonoBehaviour
{
    LanguageManager languageManager;

    static int lifes;
    Text textLifes;
    Text textTimeRemaining;

    // Atributos modificables
    int minutesForLife = 1;
    int maxLifes = 5;
    /*************/

    public int minutesRemaining;
    float secondsCount = 0;

    void Start()
    {
        GetLifeDta();
    }

    private void GetLifeDta()
    {
        LocalNotification.CancelNotification(1);

        languageManager = FindObjectOfType<LanguageManager>();

        DateTime lastCon = Storage.GetDateLastConnection();
        TimeSpan timePassed = DateTime.Now - lastCon;

        //Storage.SaveLifes(0);
        lifes = Storage.GetLifes();
        textLifes = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        textTimeRemaining = transform.GetChild(0).GetChild(1).GetComponent<Text>();

        minutesRemaining = (maxLifes - lifes) * minutesForLife;
        minutesRemaining -= Convert.ToInt32(timePassed.TotalMinutes);

        if (minutesRemaining <= 0)
        {
            minutesRemaining = 0;
        }

        lifes = maxLifes - (minutesRemaining / minutesForLife);

        if (lifes < maxLifes)
        {
            double decimalPart = timePassed.TotalMinutes - Convert.ToDouble(timePassed.TotalMinutes.ToString().Split('.')[0]);
            double secondsPassedDouble = decimalPart * 60;
            int secondsPassed = Convert.ToInt32(secondsPassedDouble.ToString().Split('.')[0]);

            //Debug.Log("seconds: " + secondsPassed);

            secondsCount = Storage.GetSecondsPassed() - secondsPassed;
        }
        else
        {
            secondsCount = 59;
        }

        textLifes.text = "x" + lifes;
        textTimeRemaining.text = languageManager.GetString("full");
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoseLife();
        }

        if (lifes < maxLifes)
        {
            secondsCount -= Time.deltaTime;

            if (secondsCount <= 0)
            {
                minutesRemaining--;
                secondsCount = 59;

                if ((minutesRemaining) % minutesForLife == 0)
                {
                    AddLife();
                }
            }
            textTimeRemaining.text = minutesRemaining-1 + ":" + secondsCount.ToString("00");
            //lifes = maxLifes - (minutesRemaining / minutesForLife) - 1;
            //textLifes.text = "x" + lifes;
        }
        else
        {
            lifes = maxLifes;
            textTimeRemaining.text = languageManager.GetString("full");
        }
    }

#if UNITY_EDITOR
    void OnDestroy()
    {
        if (lifes < maxLifes)
        {
            int totalSecondsRemaining = (minutesRemaining * 60) + Convert.ToInt32(secondsCount) - 60;
            Debug.Log("Seconds Remaining: " + totalSecondsRemaining);

            LocalNotification.CancelNotification(1);
            LocalNotification.SendNotification(1, totalSecondsRemaining, "Kitten Dice", "Your mana is full!!", new Color32(0xff, 0x44, 0x44, 255));
        }

        Storage.SaveLifes(lifes);
        Storage.SaveDateLastConnection();
        Storage.SaveSecondsPassed(Convert.ToInt32(secondsCount));
        
    }

#endif


    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            if (lifes < maxLifes)
            {
                int totalSecondsRemaining = (minutesRemaining * 60) + Convert.ToInt32(secondsCount) - 60;
                Debug.Log("Seconds Remaining: " + totalSecondsRemaining);

                LocalNotification.CancelNotification(1);
                LocalNotification.SendNotification(1, totalSecondsRemaining, "Kitten Dice", "Your mana is full!!", new Color32(0xff, 0x44, 0x44, 255));
            }

            Storage.SaveLifes(lifes);
            Storage.SaveDateLastConnection();
            Storage.SaveSecondsPassed(Convert.ToInt32(secondsCount));
        }
        else
        {
            GetLifeDta();
        }
    }

    public void AddLife()
    {
        if (lifes < maxLifes)
        {
            lifes++;
            textLifes.text = "x" + lifes;
        }
    }

    public void LoseLife()
    {
        if (lifes > 0)
        {
            lifes--;
            textLifes.text = "x" + lifes;
            minutesRemaining += minutesForLife;
        }
    }



}
