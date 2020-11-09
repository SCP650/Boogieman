using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTracker : MonoBehaviour {

	// Input/Tracked data
	// Note - when adding/changing these, make sure to initialize them in reset_tracked_data()
	//   TODO - check this!
	[HideInInspector] public string ID;

	[HideInInspector] public int baseline_hr;
	[HideInInspector] public int baseline_so2;

	[HideInInspector] public int incongruent_correct;
	[HideInInspector] public int congruent_correct;

	[HideInInspector] public int incongruent_incorrect; // wrong direction
	[HideInInspector] public int congruent_incorrect; // wrong direction

	[HideInInspector] public int incongruent_misses; // didn't hit block
	[HideInInspector] public int congruent_misses; // didn't hit block

	[HideInInspector] public float incongruent_reaction_time_acc; // accumulates the reaction times of every incongruent block
	[HideInInspector] public float congruent_reaction_time_acc; // accumulates the reaction times of every congruent block

	[HideInInspector] public int highest_level_completed;

	[HideInInspector] public float total_time;

	[HideInInspector] public int final_hr;
	[HideInInspector] public int final_so2;

	[HideInInspector] public int enjoyment; // 1-5
	[HideInInspector] public bool would_u_play_this_again; // True = yes, False = no.
	[HideInInspector] public int positive_affect;
	[HideInInspector] public int negative_affect;
	// Note - when adding/changing these, make sure to initialize them in reset_tracked_data()


	// Learning curve data
	private string learningCurveDataString;

	// Constants
	private const string NYI = "NOT YET IMPLEMENTED"; // TODO - Implement all uses of this
	private const int NYI_int = -1; // TODO - implement all uses of this


	// Start is called before the first frame update
	void Start() {
		reset_tracked_data();
		// Testing. TODO - REMOVE THIS:
		test_save();
	}


	// Initializes all the tracked data variables to default values.
	public void reset_tracked_data() {
		ID = "[MISSING]";

		baseline_hr = NYI_int;
		baseline_so2 = NYI_int;

		incongruent_correct = 0;
		congruent_correct = 0;

		incongruent_incorrect = 0; // wrong direction
		congruent_incorrect = 0; // wrong direction

		incongruent_misses = 0; // didn't hit block
		congruent_misses = 0; // didn't hit block

		incongruent_reaction_time_acc = 0; // accumulates the reaction times of every incongruent block
		congruent_reaction_time_acc = 0; // accumulates the reaction times of every congruent block

		highest_level_completed = 0;

		total_time = 0;

		final_hr = NYI_int;
		final_so2 = NYI_int;

		enjoyment = 0; // 1-5
		would_u_play_this_again = false; // True = yes, False = no.
		positive_affect = 0;
		negative_affect = 0;
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
		//string[] DataHeader = {
		//	"Trial Date",
		//	"Baseline HR",
		//	"Baseline SO2",
		//	"Total Correct",
		//	"Total Accuracy %",
		//	"Total Trials",
		//	"Total Incorrect (wrong direction)",
		//	"Total Misses (didn't hit block)",
		//	"Total Errors (incorrect OR missed)",
		//	"Average Incongruent Reaction Time",
		//	"Avergage Congruent Reaction Time",
		//	"Average Reaction Time",
		//	"Total_Incongruent_Trials",
		//	"Total_Congruent_Trials",
		//	"Total_Errors_Incongruent",
		//	"Total_Errors_Congruent",
		//	"Total_Correct_Incongruent",
		//	"Total_Correct_Congruent",
		//	"Incongruent Accuracy %",
		//	"Congruent Accuracy %",
		//	"Highest Level Completed",
		//	"Total Time",
		//	"Heart Rate",
		//	"SO2",
		//	"HRV",
		//	"Enjoyment (1-5)",
		//	"Would you play this again? (No or Yes)",
		//	"Positive Affect",
		//	"Negative Affect"
		//};

		// Compute session data
		string trialDate = System.DateTime.Now.ToString();

		// incongruent and congruent
		int incongruent_errors = incongruent_incorrect + incongruent_misses;
		int congruent_errors =   congruent_incorrect +   congruent_misses;

		int incongruent_total = incongruent_correct + incongruent_errors;
		int congruent_total   =   congruent_correct + congruent_errors;

		float incongruent_accuracy = 100.0f * incongruent_correct / incongruent_total;
		float   congruent_accuracy = 100.0f *   congruent_correct /   congruent_total;

		float incongruent_reaction_time = incongruent_reaction_time_acc / incongruent_total; // TODO - is this over total? Or just correct?
		float   congruent_reaction_time =   congruent_reaction_time_acc /   congruent_total; // TODO - is this over total? Or just correct?

		// totals
		int total_correct = incongruent_correct + congruent_correct;
		int total_trials = incongruent_total + congruent_total;

		float total_accuracy = 100.0f * total_correct / total_trials;

		int total_incorrect = incongruent_incorrect + congruent_incorrect;
		int total_misses = incongruent_misses + congruent_misses;

		int total_errors_trials = incongruent_errors + congruent_errors;
		float avg_reaction_time = (incongruent_reaction_time_acc + congruent_reaction_time_acc) / total_trials; // TODO - is this over total? Or just correct?

		string HRV = NYI; // TODO - how to compute HRV?
		string would_play_again = would_u_play_this_again ? "yes" : "no";

		// Construct the data array
		string[] info = {
			// initial input data
			trialDate,
			s(baseline_hr),
			s(baseline_so2),
			// trial data
			s(total_correct),
			s(total_accuracy),
			s(total_trials),
			s(total_incorrect),
			s(total_misses),
			s(total_errors_trials),
			s(incongruent_reaction_time),
			s(congruent_reaction_time),
			s(avg_reaction_time),
			s(incongruent_total),
			s(congruent_total),
			s(incongruent_errors),
			s(congruent_errors),
			s(incongruent_correct),
			s(congruent_correct),
			s(incongruent_accuracy),
			s(congruent_accuracy),
			s(highest_level_completed),
			s(total_time),
			// final input data
			s(final_hr),
			s(final_so2),
			HRV,
			s(enjoyment),
			would_play_again,
			s(positive_affect),
			s(negative_affect)
		};

		DataSavingBoogie.SaveData(ID, info);
		reset_tracked_data();

		// Also save all the single trial data
		//int trialsPerGame = trialsPerBlock * blocksPerLevel * levelsPerGame;
		//DataSavingFlanker.SaveLearningCurveData(ID.text, learningCurveDataString, trialsPerGame);
		//learningCurveDataString = "";
	}


	// Call this to test the save function
	private void test_save() {
		// Reset the data first
		reset_tracked_data();

		// Fill in test data
		string nice_date_and_time = System.DateTime.Now.ToString().Replace("/", "_").Replace(":", "_");
		ID = "[TEST " + nice_date_and_time + "]";

		baseline_hr = 1;
		baseline_so2 = 1;

		incongruent_correct = 1;
		congruent_correct = 2;

		incongruent_incorrect = 3; // wrong direction
		congruent_incorrect = 4; // wrong direction

		incongruent_misses = 5; // didn't hit block
		congruent_misses = 6; // didn't hit block

		incongruent_reaction_time_acc = 7.0f; // accumulates the reaction times of every incongruent block
		congruent_reaction_time_acc = 8.0f; // accumulates the reaction times of every congruent block

		highest_level_completed = 9;

		total_time = 10.0f;

		final_hr = 11;
		final_so2 = 12;

		enjoyment = 3; // 1-5
		would_u_play_this_again = true; // True = yes, False = no.
		positive_affect = 15;
		negative_affect = 16;

		// Call the save function
		Save();
	}
}
