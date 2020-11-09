using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ===== OLD FLANKER SCRIPT - EXAMPLE FOR CALLING SaveData() FROM DataSavingFlanker (but we're now using DataSavingBoogie) =====


[SerializeField]
public class LevelManagerFlanker : MonoBehaviour {
    [SerializeField]
     GameObject jarItems;
	
	public enum Dir {left, right}
	public enum Congr {ic, c}
	private enum state {input, anim, ui}
	
	/*************
	* Variables
	**************/
 	// Public UI inputs                          //Frankie and the Flankers
	public InputField field;                     public GameObject Flankers;
	                                             public GameObject Frankie;
	public GameObject reward;				     public GameObject dolphie;
												 public GameObject Map;


	//rate of incongruent to congruent flankers  //the side frankie faces
	public float ic_rate = .68f;                 private Dir   correctSide;
                                                 private Congr congruency ;
	
	// how many times frankie has shown up       //How many trials happpen between blocks
	private int trial;                           public int trialsPerBlock;
	private int block;                           public int blocksPerLevel; [HideInInspector]
	public  int level;                           public int levelsPerGame ;              

	//Whether we should be updating------        //Whether a level is currently being played
	private state myState; 	                     [HideInInspector] public bool endLevel;
                                                 [HideInInspector] public bool endGame ;
	                                             private bool timeIsUp;                     
		
	//The frankie instance                       //reaction-time data
	private GameObject myFrankie;                private float time_incongr;
	private GameObject myFlankers;               private float time_congr;
                                               private float time_this_trial;


	//the number of successful trials this game  //The total time limit
	private int congr_successes;                 private int timeLimit = 15 * 60;
	private int incongr_successes;				 private int incongr_trials;
	private int incorrect_trials;				 private int congr_trials;
  private int timed_out_trials;
  public bool successful = false;       		 public bool doneAnim = false;
    public bool endPour = false;
    public GameObject jar;

	//The time limit for the level
	private float aliveTime = 0.0f;					 //The audio sources
	/*The time that Frankie spawned*/			 public AudioClip pos;
	private float spawnTime = 0.0f;           			 public AudioClip neg;

	[SerializeField]
	private Text devInfo;						 public List<Sprite> rewards;
	private GameObject rewardSprite;			 private int failedBlocks;
												 private int successesPerBlock;

	[SerializeField]							
	GameObject jarAnim = null;							  [SerializeField]
												  ParticleSystem particles;

	public AudioClip[] music;					public bool exercise;

	public GameObject endPanel;					public GameObject endBack;
	private string enjoyment;					public AudioClip[] pos_feedback_noises;
	public InputField ID;
	public InputField baselineHeart;			public InputField baselineSO2;
	public InputField baselineHeartExercise;	public InputField baselineSO2Exercise;
	public InputField endHeart;					public InputField endSO2;		
	private float start_time;					/*public universalGM gm;*/
	private float playAgain;					/*private music_manager mm;*/
    public GameObject next_level_ui;
    
    private string learningCurveDataString;

  /**************
	*  Functions  *
	/*************/


  public void setExercise(){exercise = true;}


	void OnEnable(){
		Cursor.visible = false;
	}
	void OnDisable(){
		Cursor.visible = true;
	}
	
	public void Start ()
	{	
		//mm = music_manager.Instance;
		//timeLimit = 100000;
		trial = 0;
		block = 0;
		level = 0;
		timeIsUp = false;
		myState = state.ui;
		//Invoke ("Spawn", 1);
		//aliveTime = blockTimes.game(block);
		foreach(Transform c in reward.transform){
			if(c.tag == "Reward"){
				rewardSprite = c.gameObject;
			}
		}

		Map.SetActive(true);
		reward.SetActive(false);
		Map.GetComponent<Animator>().SetInteger("level",1);
		Map.GetComponent<Animator>().SetBool("timeUp",false);
		Invoke("EndMap",1.0f);
		start_time = Time.time;

	}
	public void End()
	{
		myState	= state.ui;
		if(myFrankie != null)
		{
			if(!exercise) Destroy(myFlankers);
			Destroy(myFrankie);
		}
	}
 
	Congr chooseCongruent () {
		return Random.value < ic_rate ? Congr.ic : Congr.c;
	}	

	Dir   chooseSide () {

		if(exercise) return correctSide == Dir.left ? Dir.right : Dir.left;
		return Random.value < .5f     ? Dir.left : Dir.right;
	}

	void Spawn ()
	{
		if(level == levelsPerGame){
			//endLevel = true;
			timeIsUp = true;
			Invoke("DoNext", 0.1f);
		}
		trial += 1;
		congruency  = chooseCongruent();
		if(congruency == Congr.c){
			congr_trials++;
		} else {
			incongr_trials++;
		}	
		correctSide = chooseSide();
		myFrankie = (GameObject)Instantiate(Frankie);
		if (correctSide == Dir.left)
			myFrankie.GetComponent<SpriteRenderer>().flipX = true;
		if(!exercise) myFlankers = (GameObject)Instantiate(Flankers);
		if (congruency  == Congr.c && correctSide == Dir.right && !exercise){
			myFlankers.transform.localScale = Vector3.one - Vector3.right * 2;
			myFlankers.transform.position = new Vector3(-.2f,.22f,0);}
		if (congruency  == Congr.ic && correctSide == Dir.left && !exercise){
			myFlankers.transform.localScale = Vector3.one - Vector3.right * 2;
			myFlankers.transform.position = new Vector3(-.2f,.22f,0);}
		spawnTime = Time.time;
		myState	= state.input;
	}


	void ChangeTrial()
	{
		Destroy(myFrankie);
		if(!exercise) Destroy(myFlankers);
		if(trial % trialsPerBlock == 0){
			NewBlock();
		}
		else
			DoNext();
	}

	void NewBlock()
	{

		if(successesPerBlock < (1/3) * trialsPerBlock)
		{
			failedBlocks++;
		}
		successesPerBlock = 0;
		block++;
		//aliveTime = blockTimes.game(block - failedBlocks); //don't speed up if you got too many wrong;
		if(block % blocksPerLevel == 0){
			level++;	
			myState = state.anim;
			particles.textureSheetAnimation.SetSprite(0, rewards[level - 1]);
            jarAnim.GetComponent<Animator>().enabled = true;
			jarAnim.GetComponent<Animator>().SetTrigger("pouring");
			Invoke("endPouring",1f);
		} else {
			Spawn();
		}
	}

	void endPouring(){
		if(! jarAnim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("running")){	
			//BubblesTransition.instance.playAnimation ();
			Invoke("startMap",.9f);
			jar.SetActive(false);
            Invoke("disableAnimator",.1f);

            
            foreach (SpriteRenderer sr in jarItems.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.sprite = rewards[level % rewards.Count];
                endPour = true;

            }

        }
        else {
			Invoke("endPouring",.1f); 
		}

	}

	void disableAnimator(){
		jarAnim.GetComponent<Animator>().enabled = false;

	}

	void startMap(){
			Map.SetActive(true);
			reward.SetActive(false);
			Map.GetComponent<Animator>().SetInteger("level",level + 1);
			if(level != 8) Invoke("EndMap",1.0f);
            else
            {
                timeIsUp = true;
                DoNext();
            }
    }

	void DoNext()
	{
		if(timeIsUp)
		{
			myState = state.ui;
			Destroy(myFlankers);
			Destroy(myFrankie);
			Map.SetActive(true);
			reward.SetActive(false);
			jar.SetActive(false);
			Map.GetComponent<Animator>().SetBool("timeUp", true);
			Map.GetComponent<Animator>().SetInteger("level", 1);
			Invoke("gameEnd", 11.8f);
		}
		else if(endLevel != true && endGame != true)
		{
			Spawn();
		}
	}

	void gameEnd(){
		if(! Map.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("running")){
			Map.SetActive(false);
			Map.GetComponent<Animator>().SetBool("timeUp", false);
			myState = state.ui;
			endPanel.gameObject.SetActive(true);
			endBack.gameObject.SetActive(true);
			this.gameObject.SetActive(false);
		} else {
			Invoke("gameEnd",.1f);
		}
	}

	public void setEnd(){
		endGame = true;
	}


	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.BackQuote)){
			level = level < 1 ? 1 : level;
			timeIsUp = true;
			DoNext();

		}

		rewardSprite.GetComponent<SpriteRenderer>().sprite = level < rewards.Count ? rewards[level] : rewards[0];
		//if ^ gives a null ref. it's because the rewards field is not populated. populate it with the node_n sprites
		devInfo.text = "Trial: " + trial.ToString() + " , Block: " + block.ToString() + ", Level: " + level.ToString();
		if (myState == state.input)
		{
			if(congruency == Congr.c){
				time_congr   += Time.deltaTime;
			} else {
				time_incongr += Time.deltaTime;
      }
      time_this_trial += Time.deltaTime;


      bool timed_out = ((Time.time - spawnTime) > (aliveTime / 1000.0f));

      // Correct!
      if ((Input.GetKeyDown(KeyCode.LeftArrow) && (correctSide == Dir.left))
				|| (Input.GetKeyDown(KeyCode.RightArrow) && (correctSide == Dir.right)))
			{
				if(congruency == Congr.c){
						congr_successes++;
					} else {
						incongr_successes++;
					}
				successesPerBlock++;
				reward.SetActive(true);
				reward.GetComponent<Animator>().SetTrigger("posFeedback");
				//this.GetComponent<AudioSource>().PlayOneShot(pos_feedback_noises[level % (pos_feedback_noises.Length == 0 ? 1 : pos_feedback_noises.Length)], 1f);
				//mm.play(pos_feedback_noises[level % (pos_feedback_noises.Length == 0 ? 1 : pos_feedback_noises.Length)],1);
				myState = state.anim;
				Invoke("endAnim", .5f);
				if(!exercise) Destroy(myFlankers);
        Destroy(myFrankie);
        successful = true;
        SaveSingleTrial(1);
      }

      // Wrong!
      else if ((Input.GetKeyDown(KeyCode.LeftArrow) && (correctSide == Dir.right))
				|| (Input.GetKeyDown(KeyCode.RightArrow) && (correctSide == Dir.left))
				|| timed_out)
			{
        if (timed_out) {
          timed_out_trials++;
        } else {
          incorrect_trials++;
        }
				reward.SetActive(true);
				dolphie.SetActive(true);
				dolphie.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = rewards[level % rewards.Count];
				reward.GetComponent<Animator>().SetTrigger("negFeedback");
				dolphie.GetComponent<Animator>().SetTrigger("negFeedback");
				//this.GetComponent<AudioSource>().PlayOneShot(neg, 1);
				//mm.play(neg,1);
				myState = state.anim;
				Invoke("endAnim",.5f);
				if(!exercise) Destroy(myFlankers);
				Destroy(myFrankie);
        SaveSingleTrial(timed_out ? 3 : 2);
      }
		} 

		if ((Time.time - start_time) >= timeLimit)
			timeIsUp = true;
	}

	void EndMap(){
		if(! Map.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("running")){
			Map.SetActive(false);
            next_level_ui.SetActive(true);
            
            
            Cursor.visible = true;
			
		} else {
			Invoke("EndMap",.1f);
		}
	}

    public void next_level()
    {
        Cursor.visible = false;
        myState = state.input;
        Spawn();
        jar.SetActive(true);
        //if (level < 6) music_manager.Instance.set_music(music[level]);
        //else music_manager.Instance.set_music(music[level - 4]);
    }

	void endAnim() {
		if(!(reward.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("running")
		||  dolphie.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("running")))
		{
			reward.SetActive(false);
			dolphie.SetActive(false);
			myState = state.input;
			ChangeTrial();
            if(successful) doneAnim = true; //mark animation as done for Jar script
		}
		else
		{
			Invoke("endAnim",.1f);
		}
	}


	public void saveFeedback(string f){
		enjoyment = f;
		Debug.Log(enjoyment);
	}

	public void set_play_again(float v){
		playAgain = v;
	}

	string s(float v){
		return v.ToString();
	}
	string s(int v){
		return v.ToString();
	}

	string sDiv (int num, int denom) {
		float answer = ((float)num) / ((float)denom);
		return answer.ToString ();
  }

  // Save the data for one individual trial
  public void SaveSingleTrial(int response) {
    // Learning Curve Header
    //{ "Trial", "Day",
    //  "Level", "Congruent_Incongruent", "Left_Right",
    //  "Response (1=correct. 2=wrong. 3=timeout)", "Accuracy", "RT (ms)" };

    string levelString = (level + 1).ToString();
    string congruent_incongruent = (congruency == Congr.c) ? "0" : "1";
    string Left_Right = (correctSide == Dir.left) ? "Left" : "Right";
    string accuracy = (response == 1) ? "100" : "0";
    string RT = ((int)(time_this_trial * 1000f)).ToString();
    time_this_trial = 0;

    // Assemble the data array here.
    // Use trial number macro, so that DataSavingFlanker.cs can enumerate each day properly.
    string[] data = {
      /* trial.ToString(), */
      DataSavingFlanker.learningCurveTrialMacro,
      DataSavingFlanker.learningCurveDayMacro, 
      levelString, congruent_incongruent, Left_Right,
      response.ToString(), accuracy, RT
    };

    learningCurveDataString += "\n" + string.Join(",", data);
  }

  // Save all the data for this session
  public void Save() {
    if (exercise) {
      baselineHeart.text = baselineHeartExercise.text;
      baselineSO2.text = baselineSO2Exercise.text;
    }

    string[] info = {
      System.DateTime.Today.ToString(),
      baselineHeart.text,
      baselineSO2.text,
      s(congr_successes),
      exercise ? "n/a" : sDiv(congr_successes, congr_trials),
      exercise ? "n/a" : s(incongr_successes),
      exercise ? "n/a" : sDiv(incongr_successes, incongr_trials),
      s(congr_successes + incongr_successes),
      sDiv((incongr_successes + congr_successes),(congr_trials + incongr_trials)),
      s(congr_trials + incongr_trials),
      s(incorrect_trials + timed_out_trials), // This is really: Trials that were failed in any way
      s(timed_out_trials), // This is: Trials that were missed (timed out)
      s(incorrect_trials), // This is really: Trials with incorrect input
      exercise ? "n/a" : s(incongr_successes - congr_successes),
      exercise ? "n/a" : s((time_incongr / incongr_trials) - (time_congr / congr_trials)),
      exercise ? "n/a" : s(time_incongr / incongr_trials),
      exercise ? "n/a" : s(time_congr / congr_trials),
      s((time_incongr + time_congr) / (congr_trials + incongr_trials)),
            s(incongr_trials),
            s(congr_trials),
            s(incongr_trials - incongr_successes),
            s(congr_trials - congr_successes),
            s(incongr_successes),
            s(congr_successes),
            s(level),
      s(Time.time - start_time),
      endHeart.text,endSO2.text,
      enjoyment,
      s(playAgain),
      //s(gm.positiveAffect),
      //s(gm.negativeAffect)

    };

    DataSavingFlanker.SaveData(ID.text, info);

    // Also save single trial data
    int trialsPerGame = trialsPerBlock * blocksPerLevel * levelsPerGame;
    DataSavingFlanker.SaveLearningCurveData(ID.text, learningCurveDataString, trialsPerGame);
    learningCurveDataString = "";
  }

}
