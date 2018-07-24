using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using ClassExtensions;
using Fove;

public class GameMode2 : ProceduralLevel {

   
    private System.Random rnd = new System.Random(); //random number generator for picking rings
    private int choice = 0; //determines which rings light up
    private bool FlashingPrimary = false; //determines if first flash is active
    private bool FlashingSecondary = false; //determines if second flash is active
    public GameObject ring1;//set up reference to rings
    public GameObject ring2;
    public GameObject ring3;
    private int target=0;//which ring lights up for player to fly thru
    public Color RingDefault;//color references 
    public Color RingLight1;
    public Color RingLight2;
    private float StartFlashTime = 0;//timer for current itereation
    private float FlashInterval = 0;//timmer to compare first flash duration
    private float FlashInterval2 = 0;//timer to compare second flash duration
    private readonly float FlashGap = 1.1f; //time gap before first flash
    private readonly float FlashDurPrim = 0.8f;//duration of first flash
    private readonly float FlashDurSec = 0.1f;//duration of second flash
    private float FrameRate = 0;//output for delay between frames
    private readonly string Path = "Assets/FrameDelay.txt";//path for file to save frame delay in
    private Vector3 RingMovement = new Vector3(0, 0, 20);
    public Transform Ring1trs;
    public Transform Ring2trs;
    public Transform Ring3trs;

    // Use this for initialization
    public override void Start ()
    {
        
        RingDefault = Color.yellow;//assign colors, can change later if needed
        RingLight1 = Color.red;
        RingLight2 = Color.cyan;
       
        //copy of gillys code for tunnel
        tunnelMat.color = ColorExtensions.RandomColor().SetAlpha(tunnelMat.color.a);
        StartCoroutine(PickNewTunnelColor());
      
    }
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update ();
        choice = rnd.Next(8);// cycles thru random numbers for determining which rings flash
        Timing(); // assumes ~60 frames per second
        OutputFramerate();//determines seconds per frame
        //MoveRings();//move rings infront of player if player passes
        
        
    }
    
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
        else//should never reach this else, if it does, something is wrong
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
        //Vector3 v = (Vector3)FoveInterface2.instance.GetGazeConvergence_Raw();
    }

    public Vector3 getRingPosition()//may need to change return type depending of fove output
    {
        //add code to get reference to ring that is lighting up
        if (target == 0)
        {
            Vector3 v = ring1.transform.position;
            return v;
        }
        else if (target == 1)
        {
            Vector3 v = ring2.transform.position;
            return v;
        }
        else if (target == 2)
        {
            Vector3 v = ring3.transform.position;
            return v;
        }
        else//should never reach this else, if it does, something is wrong
        {
            Vector3 v = new Vector3(0, 0, 0);
            return v;
        }
    }
    //seconds timing ver
    public void Timing()
    {
        StartFlashTime += Time.deltaTime;
        if(StartFlashTime>=FlashGap && StartFlashTime<FlashGap+FlashDurPrim)
        {
            if (!FlashingPrimary)//flash if not already flashing
            {
                RingChoice(choice);
            }
            FlashInterval += Time.deltaTime;
        }
        
        if (FlashInterval >= FlashDurPrim)//reset rings after flashing
        {
            ResetRings();
        }
        if(StartFlashTime>=FlashGap+FlashDurPrim)//second flash
        {
            if (!FlashingSecondary)// flash if not flashing
            {
                PickTarget();
            }
            FlashInterval2 += Time.deltaTime;
        }
        if(FlashInterval2>=FlashDurSec)//reset rings after second flash
        {
            ResetRings();
        }
        if(StartFlashTime>FlashGap+FlashDurPrim+FlashDurSec && (PlayerShip.instance.trs.position.z > Ring1trs.position.z || PlayerShip.instance.trs.position.z > Ring2trs.position.z || PlayerShip.instance.trs.position.z > Ring3trs.position.z))//reset timers for next set of rings
        {
            //if(PlayerShip.instance.trs.position.x == ring2.transform.position.x)
                ResetInterval();
            MoveRings();// move rings infront of player
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

    public void PickTarget()//choose which ring player needs to fly thru
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
        else//should never reach this else, if it does, something is wrong
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
        string temp = "seconds per frame: " + FrameRate.ToString() + Environment.NewLine;
        File.AppendAllText(Path, temp);

    }
    
    
    public void MoveRings()
    {
        
        if (PlayerShip.instance.trs.position.z > Ring1trs.position.z|| PlayerShip.instance.trs.position.z > Ring2trs.position.z|| PlayerShip.instance.trs.position.z > Ring3trs.position.z)
        {
            
            Ring1trs.position += RingMovement;
            Ring2trs.position += RingMovement;
            Ring3trs.position += RingMovement;
        }
    }

    public void ResetInterval()
    {
        StartFlashTime = 0;
        FlashInterval = 0;
        FlashInterval2 = 0;
    }
    
    /*
    public void Test()
    {
        FoveInterface2.instance.GetGazeConvergence();
        FoveUnityUtils.GetUnityVector(FoveInterface2.instance.GazeConvergenceData);
    }
    */
}
