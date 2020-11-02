using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTracker : MonoBehaviour {

	// Input data
	private string baseline_hr = NYI;
	private string baseline_so2 = NYI;

	// Tracked data
	private int incongruent_correct;
	private int congruent_correct;
	// Note - when adding/changing these, make sure to initialize them in reset_tracked_data()



	// Learning curve data
	private string learningCurveDataString;

	// Constants
	private const string NYI = "NOT YET IMPLEMENTED"; // TODO - Implement all uses of this
	private const int NYI_int = -1; // TODO - implement all uses of this



	// Start is called before the first frame update
	void Start() {
		reset_tracked_data();
	}

	// Initializes all the tracked data variables to default values
	public void reset_tracked_data() {
		// TODO
		// also TODO - call this between songs
	}


	// ----- STRING UTIL -----

	string s(float v) {
		return v.ToString();
	}
	string s(int v) {
		return v.ToString();
	}

	// ----- DATA SAVING FUNCTIONS -----

	// Save the data for one individual trial
	public void SaveSingleTrial(int response) {
		// Learning Curve Header
		//{ TODO };

		// TODO - compute trial data here

		// Assemble the data array here.
		// Use trial number macro, so that DataSavingBoogie.cs can enumerate each day properly.
		string[] data = {
			/* trial.ToString(), */
			DataSavingBoogie.learningCurveTrialMacro,
			DataSavingBoogie.learningCurveDayMacro,
			/* TODO */
		};

		learningCurveDataString += "\n" + string.Join(",", data);
	}

	// Save all the data for this session
	public void Save() {
		// TODO - compute session data here

		//string[] DataHeader = {
		//	"Trial Date", "Baseline HR", "Baseline SO2",
		//	"Incongruent Correct (total correct trials)", "Incongruent Accuracy %",
		//	"Congruent Correct (total correct trials)",   "Congruent Accuracy %",
		//	"Total Correct", "Total Correct %", "Total Trials", "Total Incorrect (wrong direction)", "Total Misses (out of time)", "Total Failed Trials",
		//	"Average Incongruent Reaction Time", "Avergage Congruent Reaction Time", "Average Reaction Time",
		//	"Total_Incongruent_Trials", "Total_Congruent_Trials", "Total_Errors_Incongruent", "Total_Errors_Congruent", "Total_Correct_Incongruent", "Total_Correct_Congruent",
		//	"Highest Level Completed", "Total Time", "Heart Rate", "SO2", "HRV",
		//	"Enjoyment (1-5)", "Would you play this again? (No or Yes)", "Positive Affect", "Negative Affect"
		//};

		string[] info = {
			System.DateTime.Today.ToString(),
			baseline_hr,
			baseline_so2,
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
