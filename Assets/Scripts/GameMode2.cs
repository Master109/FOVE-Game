using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameMode2 : MonoBehaviour {

    private int counter=0; //counts thru an interval
    private int frequency = 10; //determins length of interval
    private int CounterCounter = 0; //counts intervals
    private int FlashDurationPrimary = 40; //determins initial flash duration of rings
    private int FlashDurationSecondary = 10; //determins flash duration of second flash on ring
    private System.Random rnd = new System.Random(); //random number generator for picking rings
    private int choice = 0; //determins which rings light up
    private bool FlashingPrimary = false; //determins if first flash is active
    private bool FlashingSecondary = false; //determins if second flash is active

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        choice = rnd.Next(8);// cycles thru random numbers for determining which rings flash
        RingFlashTiming(); // assumes ~60 frames per second
        isFlashingPrimary(FlashingPrimary);
        isFlashingSecondary(FlashingSecondary);
	}

    public void RingFlashTiming() 
    {
       if(counter>=frequency)//determines what happens at end of interval
        {
            counter = 0; //reset counter
            RingChoice(choice);//randomly choose which rings to light up
            //call ring flash function here

            CounterCounter++; //note interval pass
            if(CounterCounter>frequency*10) //slowly increase the length of the interval
            {
                frequency += 5;
            }
        }
       else
        {
            counter++;
        }
    }
    public void Flashing()
    {
        //add code to reference ring object here

        //add code to change the color of said ring object here

        FlashDurationPrimary = 40 - (CounterCounter / 100);//this is to slowly decrement the duration of the flash
        if(FlashDurationPrimary<1||FlashDurationSecondary<1) //reset when flash duration reaches 0
        {
            FlashDurationPrimary = 40;
            FlashDurationSecondary = 10;
            CounterCounter = 0;
            
        }
        if(FlashDurationPrimary<=FlashDurationSecondary)
        {
            FlashDurationSecondary--;
        }
    }
    public void RingChoice(int i)
    {
        if(i==0)
        {
            //light up left ring
        }
        else if(i==1)
        {
            //light up middle ring
        }
        else if(i==2)
        {
            //light up right ring
        }
        else if(i==3)
        {
            //light up left and middle ring
        }
        else if(i==4)
        {
            //light up left and right ring
        }
        else if(i==5)
        {
            //light up middle and right ring
        }
        else if(i==6)
        {
            //light up left right and middle ring
        }
        else if(i==7)
        {
            //light up no rings
        }
        else
        {
            Application.Quit();
        }
    }
    public void isFlashingPrimary(bool b)
    {

    }
    public void isFlashingSecondary(bool b)
    {

    }

}
