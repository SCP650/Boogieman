using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;


// ===== OLD COGNITIVE TASKS SCRIPT - EXAMPLE FOR READING, FORMATTING, AND SAVING DATA (but we're now using DataSavingBoogie) =====




public enum Task {DayNight, GoNoGo, Flanker};
public enum TrialType {Pretest, Posttest, DelayedTest, Showcase};


public static class DataSavingCogTasks {
	static string DataFilePath = @"ResearchData/PrePostTestData.csv"; // If you change this, make sure to adjust the writeData method accordingly
	static string BackupCogTasksDataPath = @"ResearchData/BackupCogTasksData/";

	// Task Data Headers -- NO IN-STRING COMMAS ALLOWED (And NO DASHES)
	static string[] InfoHeader = {		"ID", 	"Birthday", 	"Age",		"Sex"};

	static string[] DayNightHeader = {	"Trial Date",			"Card Sequence",
		"No_Conflict Correct",	"No_Conflict Error",	"No_Conflict Percent Correct",	"No_Conflict Trial Time",	"No_Conflict Avg Rxn Time",
		"Conflict Correct",		"Conflict Error",		"Conflict Percent Correct",		"Conflict Trial Time",		"Conflict Avg Rxn Time",
		"Total Correct",		"Total Error",			"Total Percent Correct",		"Combined Trial Time",		"Combined Avg Rxn Time"};

	static string[] GoNoGoHeader = {	"Trial Date",				"Order",					"Controller",				"Highest Level",			"Num Trials",
		"Num Go Correct",			"Num No Go Correct",		"Num Total Correct",		"Num Go Incorrect",			"Num No Go Incorrect",
		"Avg Reaction Time Good",	"Avg Reaction Time Bad",	"Num Misses",				"Accuracy"};

	static string[] FlankerHeader = {	"Trial Date",					"Total Trials",					"Successes",	"Failures",			"Accuracy",								"Error Rate",
		"Average reaction time (IC)",	"Average reaction time (C)",	"# RI",			"Accuracy RI",		"# RC",									"Accuracy RC", 
		"# LI",							"Accuracy LI",					"# LC",			"Accuracy LC",		"Conflict Resolution Time (IC_C)", 		"Conflict Resolution Accuracy"};

	static char[] fieldSeparator = { ',' };

	// Column pre/post/delayed test indices
	static int DNPreIndex;
	static int GNGPreIndex;
	static int FPreIndex;

	static int DNPostIndex;
	static int GNGPostIndex;
	static int FPostIndex;

	static int DNDelayIndex;
	static int GNGDelayIndex;
	static int FDelayIndex;

	public static TrialType CurrentTrialType;

	public static string birthdate;
	public static string sex;


	// Call this to save trial data into the .csv file
	public static void SaveData(string ID, string[] newData, Task task) {
		if (CurrentTrialType == TrialType.Showcase) {
			return;
		}

		if (string.Equals (ID, "")) {
			ID = "(No ID entered)";
		}

		string[] newInfoArray = createInfoArray (ID);

		// Save backup of the data
		SaveBackup (ID, newInfoArray, newData, task);

		// Read the current data and make sure the header is correct
		string[][] AllData = readCSVFile (DataFilePath);
		makeDataHeader ().CopyTo (AllData, 0);

		// Get ID
		int lineIndex = findID (AllData, ID);

		// If ID not found in the previous data, save in a new line
		if (lineIndex < 0) {
			string[][] newAllData = new string[AllData.Length + 1][];
			AllData.CopyTo (newAllData, 0);

			string[] newLine = new string[ AllData[0].Length ];
			lineIndex = newAllData.Length - 1;
			newAllData [lineIndex] = newLine;
			newInfoArray.CopyTo (newAllData [lineIndex], 0);

			AllData = newAllData;
		}


		// Find the column index for the data, and check if the newData is the right length
		int colIndex = 0;
		if (task == Task.DayNight) {
			if (newData.Length != DayNightHeader.Length) {
				Debug.LogError ("Unexpected data array length for DayNight task data saving");
			}

			if (CurrentTrialType == TrialType.Pretest) {
				colIndex = DNPreIndex;
			} else if (CurrentTrialType == TrialType.Posttest) {
				colIndex = DNPostIndex;
			} else if (CurrentTrialType == TrialType.DelayedTest) {
				colIndex = DNDelayIndex;
			}
		} else if (task == Task.GoNoGo) {
			if (newData.Length != GoNoGoHeader.Length) {
				Debug.LogError ("Unexpected data array length for GoNoGo task data saving");
			}

			if (CurrentTrialType == TrialType.Pretest) {
				colIndex = GNGPreIndex;
			} else if (CurrentTrialType == TrialType.Posttest) {
				colIndex = GNGPostIndex;
			} else if (CurrentTrialType == TrialType.DelayedTest) {
				colIndex = GNGDelayIndex;
			}
		} else if (task == Task.Flanker) {
			if (newData.Length != FlankerHeader.Length) {
				Debug.LogError ("Unexpected data array length for Flanker task data saving");
			}

			if (CurrentTrialType == TrialType.Pretest) {
				colIndex = FPreIndex;
			} else if (CurrentTrialType == TrialType.Posttest) {
				colIndex = FPostIndex;
			} else if (CurrentTrialType == TrialType.DelayedTest) {
				colIndex = FDelayIndex;
			}
		}

		// Copy into the data array
		newData.CopyTo (AllData [lineIndex], colIndex);
		string dataString = convert2DToString (AllData);

		// Write it to the file
		writeData(dataString);
	}


	// Read the data from a .csv and return it as a string[][]
	private static string[][] readCSVFile (string path) {
		string[][] allData;
		if (File.Exists (path)) {
			string[] lines = File.ReadAllLines (path);
			if (lines.Length > 1) {
				allData = new string[lines.Length][];

				for (int i=0; i < lines.Length; i++) {
					string[] fields = lines [i].Split (fieldSeparator);
					allData [i] = new string[fields.Length];
					fields.CopyTo (allData [i], 0);
				}
				return allData;
			}
		}

		// File has not been initialized
		allData = new string[1][];
		return allData;
	}

	// Search for a string[] with the same ID at index 0
	private static int findID(string[][] data, string ID) {
		for (int i = 1; i < data.Length; i++) {
			if (data [i] [0] == ID) {
				return i;
			}
		}

		return -1;
	}

	// Create the info array
	private static string[] createInfoArray (string ID) {
		string[] infoArray = new string[InfoHeader.Length];

		infoArray [0] = ID;
		infoArray [1] = birthdate;

		DateTime result;
		if (DateTime.TryParse (birthdate, out result)) {
			// Compute age
			DateTime today = DateTime.Today;
			TimeSpan dayDiff = today.Subtract (result);
			double years = dayDiff.TotalDays / 365.25;
			//float yearsF = (float)years;
			infoArray [2] = years.ToString ("##.###");
		} else {
			infoArray [2] = "";
		}

		infoArray [3] = sex;

		// Reset the global strings
		birthdate = "";
		sex = "";

		return infoArray;
	}

	/* // Update the subject info if necessary
	private static void updateInfoLine (string[] newData) {

	} */

	// Set up the header for the data CSV
	private static string[][] makeDataHeader() {
		string[][] header = new string[1][];
		int lineLength = InfoHeader.Length + (DayNightHeader.Length + GoNoGoHeader.Length + FlankerHeader.Length) * 3;
		header [0] = new string[lineLength];

		// Adding prefixes to headers --- No dashes or commas allowed
		string[] DN_PreHeader = addPrefix (DayNightHeader, "DN_Pre_");
		string[] DN_PostHeader = addPrefix (DayNightHeader, "DN_Post_");
		string[] DN_DelayHeader = addPrefix (DayNightHeader, "DN_Delay_");

		string[] GNG_PreHeader = addPrefix (GoNoGoHeader, "GNG_Pre_");
		string[] GNG_PostHeader = addPrefix (GoNoGoHeader, "GNG_Post_");
		string[] GNG_DelayHeader = addPrefix (GoNoGoHeader, "GNG_Delay_");

		string[] F_PreHeader = addPrefix (FlankerHeader, "F_Pre_");
		string[] F_PostHeader = addPrefix (FlankerHeader, "F_Post_");
		string[] F_DelayHeader = addPrefix (FlankerHeader, "F_Delay_");

		// Copy the headers at their correct indices, and store the indices along the way
		int index = 0;
		InfoHeader.CopyTo (header [0], index);				index += InfoHeader.Length;				DNPreIndex = index;

		DN_PreHeader.CopyTo (header [0], index);			index += DayNightHeader.Length;			GNGPreIndex = index;
		GNG_PreHeader.CopyTo (header [0], index);			index += GoNoGoHeader.Length;			FPreIndex = index;
		F_PreHeader.CopyTo (header [0], index);				index += FlankerHeader.Length;			DNPostIndex = index;

		DN_PostHeader.CopyTo (header [0], index);			index += DayNightHeader.Length;			GNGPostIndex = index;
		GNG_PostHeader.CopyTo (header [0], index);			index += GoNoGoHeader.Length;			FPostIndex = index;
		F_PostHeader.CopyTo (header [0], index);			index += FlankerHeader.Length;			DNDelayIndex = index;

		DN_DelayHeader.CopyTo (header [0], index);			index += DayNightHeader.Length;			GNGDelayIndex = index;
		GNG_DelayHeader.CopyTo (header [0], index);			index += GoNoGoHeader.Length;			FDelayIndex = index;
		F_DelayHeader.CopyTo (header [0], index);			index += FlankerHeader.Length;

		// Output the final result
		return header;
	}

	// Add a prefix to every string in a string[]
	private static string[] addPrefix (string[] array, string prefix) {
		string[] newArray = new string[array.Length];
		for (int i = 0; i < array.Length; i++) {
			newArray[i] = prefix + array[i];
		}
		return newArray;
	}

	// Convert string[][] to string
	private static string convert2DToString(string[][] TwoDArray) {
		string output = "";
		foreach (string[] line in TwoDArray) {
			output = output + string.Join (",", line) + "\n";
		}
		return output;
	}

	// Write the input string to the DataFilePath
	private static void writeData (string dataString) {
		if (!Directory.Exists (@"ResearchData")) {
			Debug.Log ("@\"ResearchData/\" folder does not yet exist. Creating it...");
			Directory.CreateDirectory (@"ResearchData");
		}

		if (!File.Exists (@"ResearchData/PrePostTestData.csv")) {
			Debug.Log ("@\"ResearchData/PrePostTestData.csv\" file does not yet exist. Creating it...");
			FileStream fs = File.Create (@"ResearchData/PrePostTestData.csv");
			fs.Close ();
		}

		File.WriteAllText (DataFilePath, dataString);
		Debug.Log ("Trial data successfully saved");
	}

	// Save the new line of data to the backup folder for the kiddo
	private static void SaveBackup (string ID, string[] infoLine, string[] newData, Task task) {
		string infoString = string.Join (",", infoLine);
		string dataString = string.Join (",", newData);

		dataString = string.Concat (infoString, ",", task.ToString(), ",", CurrentTrialType.ToString(), ",", dataString);

		string path = BackupCogTasksDataPath + ID + ".csv";
		if (!File.Exists (path)) {
			Directory.CreateDirectory (BackupCogTasksDataPath);
		}
		File.AppendAllText (path, dataString);
		Debug.Log ("Backup cognitive tasks data saved");
	}
}
