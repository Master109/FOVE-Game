using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class GameMode2 : MonoBehaviour {

    private int counter=0; //counts thru an interval
    private int frequency = 10; //determines length of interval
    private int CounterCounter = 0; //counts intervals
    private int FlashDurationPrimary = 40; //determines initial flash duration of rings
    private int FlashDurationSecondary = 10; //determines flash duration of second flash on ring
    private System.Random rnd = new System.Random(); //random number generator for picking rings
    private int choice = 0; //determines which rings light up
    private bool FlashingPrimary = false; //determines if first flash is active
    private bool FlashingSecondary = false; //determines if second flash is active
    public GameObject ring1;//set up reference to rings
    public GameObject ring2;
    public GameObject ring3;
    private int target=0;//which ring lights up for player to fly thru
    Color RingDefault;//color references 
    Color RingLight1;
    Color RingLight2;
    private float StartFlashTime = 0;
    private float FlashInterval = 0;
    private float FlashInterval2 = 0;
    private readonly float FlashGap = 1.1f;
    private readonly float FlashDurPrim = 0.8f;
    private readonly float FlashDurSec = 0.1f;
    private float FrameRate = 0;
    private readonly string Path = "Assets/FrameDelay.txt";
	// Use this for initialization
	void Start ()
    {
        ring1 = GetComponent<GameObject>().gameObject;//need to reference these to rings in unity editor
        ring2 = GetComponent<GameObject>().gameObject;
        ring3 = GetComponent<GameObject>().gameObject;
        RingDefault = Color.yellow;//assign colors, can change later if needed
        RingLight1 = Color.red;
        RingLight2 = Color.cyan;
        ring1.GetComponent<Renderer>().material.color = RingDefault;//sets rings to default color
        ring2.GetComponent<Renderer>().material.color = RingDefault;
        ring3.GetComponent<Renderer>().material.color = RingDefault;
    }
	
	// Update is called once per frame
	void Update ()
    {
        choice = rnd.Next(8);// cycles thru random numbers for determining which rings flash
        Timing(); // assumes ~60 frames per second
        OutputFramerate();//determines seconds per frame


    }
    /* //frame timing ver
    public void RingFlashTiming() 
    {
       if(counter>=frequency)//determines what happens at end of interval
        {
            counter = 0; //reset counter
            RingChoice(choice);//randomly choose which rings to light up
            
            

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
       if(counter>=FlashDurationPrimary)
        {
            ring1.GetComponent<Renderer>().material.color = RingDefault;
            ring2.GetComponent<Renderer>().material.color = RingDefault;
            ring3.GetComponent<Renderer>().material.color = RingDefault;
            FlashingPrimary = false;
        }
       if(CounterCounter>FlashDurationSecondary)//seting up secondary flash on 1 ring
        {
            target = rnd.Next(3);
            if(target==0)
            {
                ring1.GetComponent<Renderer>().material.color = RingLight2;
                FlashingSecondary = true;
            }
            else if(target==1)
            {
                ring2.GetComponent<Renderer>().material.color = RingLight2;
                FlashingSecondary = true;
            }
            else if(target==2)
            {
                ring3.GetComponent<Renderer>().material.color = RingLight2;
                FlashingSecondary = true;
            }
            else
            {
                Application.Quit();
            }
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
        if(FlashDurationPrimary<=FlashDurationSecondary)//ensure second flash is always shorter than initial flash
        {
            FlashDurationSecondary--;
        }
    }
    */
    public void RingChoice(int i)
    {
        if(i==0)
        {
            //light up left ring
            ring1.GetComponent<Renderer>().material.color = RingLight1;
            FlashingPrimary = true;
        }
        else if(i==1)
        {
            //light up middle ring
            ring2.GetComponent<Renderer>().material.color = RingLight1;
            FlashingPrimary = true;
        }
        else if(i==2)
        {
            //light up right ring
            ring3.GetComponent<Renderer>().material.color = RingLight1;
            FlashingPrimary = true;
        }
        else if(i==3)
        {
            //light up left and middle ring
            ring1.GetComponent<Renderer>().material.color = RingLight1;
            ring2.GetComponent<Renderer>().material.color = RingLight1;
            FlashingPrimary = true;
        }
        else if(i==4)
        {
            //light up left and right ring
            ring1.GetComponent<Renderer>().material.color = RingLight1;
            ring3.GetComponent<Renderer>().material.color = RingLight1;
            FlashingPrimary = true;
        }
        else if(i==5)
        {
            //light up middle and right ring
            ring2.GetComponent<Renderer>().material.color = RingLight1;
            ring3.GetComponent<Renderer>().material.color = RingLight1;
            FlashingPrimary = true;
        }
        else if(i==6)
        {
            //light up left right and middle ring
            ring1.GetComponent<Renderer>().material.color = RingLight1;
            ring2.GetComponent<Renderer>().material.color = RingLight1;
            ring3.GetComponent<Renderer>().material.color = RingLight1;
            FlashingPrimary = true;
        }
        else if(i==7)
        {
            //light up no rings
            FlashingPrimary = false;
        }
        else
        {
            Application.Quit();
        }
    }
    public bool isFlashingPrimary()
    {
        return FlashingPrimary;
    }
    public bool isFlashingSecondary()
    {
        return FlashingSecondary;
    }

    public void getEyePosition()//need to change return type and add return statement
    {
        FoveInterface2.instance.GetGazeConvergence_Raw();
    }

    public Vector3 getRingPosition()//may need to change return type depending of fove output
    {
        //add code to get reference to ring that is lighting up
        if (target == 0)
        {
            Vector3 v = ring1.GetComponent<GameObject>().transform.position;
            return v;
        }
        else if (target == 1)
        {
            Vector3 v = ring2.GetComponent<GameObject>().transform.position;
            return v;
        }
        else if (target == 2)
        {
            Vector3 v = ring3.GetComponent<GameObject>().transform.position;
            return v;
        }
        else
        {
            Vector3 v = new Vector3(0, 0, 0);
            return v;
        }
    }
    //seconds timing ver
    public void Timing()
    {
        StartFlashTime += Time.deltaTime;
        if(StartFlashTime>FlashGap)
        {
            if (!FlashingPrimary)//flash if not already flashing
            {
                RingChoice(choice);
            }
            FlashInterval += Time.deltaTime;
        }
        
        if (FlashInterval > FlashDurPrim)//reset rings after flashing
        {
            ResetRings();
        }
        if(StartFlashTime>FlashGap+FlashDurPrim)//second flash
        {
            if (!FlashingSecondary)// flash if not flashing
            {
                PickTrget();
            }
            FlashInterval2 += Time.deltaTime;
        }
        if(FlashInterval2>FlashDurSec)//reset rings after second flash
        {
            ResetRings();
        }
        if(StartFlashTime>FlashGap+FlashDurPrim+FlashDurSec)//reset timers for next set of rings
        {
            StartFlashTime = 0;
            FlashInterval = 0;
            FlashInterval2 = 0;
            GameManager.instance.RestartScene();
        }


    }

    public void ResetRings()//resets ring values to default state
    {
        ring1.GetComponent<Renderer>().material.color = RingDefault;
        ring2.GetComponent<Renderer>().material.color = RingDefault;
        ring3.GetComponent<Renderer>().material.color = RingDefault;
        FlashingPrimary = false;
        FlashingSecondary = false;
    }

    public void PickTrget()//choose which ring player needs to fly thru
    {
        target = rnd.Next(3);
        if (target == 0)
        {
            ring1.GetComponent<Renderer>().material.color = RingLight2;
            FlashingSecondary = true;
        }
        else if (target == 1)
        {
            ring2.GetComponent<Renderer>().material.color = RingLight2;
            FlashingSecondary = true;
        }
        else if (target == 2)
        {
            ring3.GetComponent<Renderer>().material.color = RingLight2;
            FlashingSecondary = true;
        }
        else
        {
            Application.Quit();
        }
    }

    public void CompareAttention()
    {
        //add code here to compare the eye position against the ring position
    }
    public void OutputFramerate()
    {
        FrameRate = Time.deltaTime;
        string temp = "seconds per frame: " + FrameRate.ToString();
        File.AppendAllText(Path, temp);

    }
}
