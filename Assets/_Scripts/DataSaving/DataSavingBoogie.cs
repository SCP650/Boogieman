using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum BoogieType { Intervention, Showcase };
public enum ExperimentalCondition { Sedentary, Exergame, Exercise, Control }

public static class DataSavingBoogie {
	static string DataFilePath = @"ResearchData/InterventionData.csv";
	static string BackupInterventionDataPath = @"ResearchData/BackupInterventionData/";
	static string LearningCurveDataPath = @"ResearchData/LearningCurve/";

	// Data Header -- NO IN-STRING COMMAS ALLOWED (it's in a .csv)
	static string[] InfoHeader = { "ID", "Condition" };

	static string[] DataHeader = {
		"Trial Date", "Baseline HR", "Baseline SO2",
		"Total Correct", "Total Accuracy %", "Total Trials", "Total Incorrect (wrong direction)", "Total Misses (didn't hit block)", "Total Errors (incorrect OR missed)",
	    "Average Incongruent Reaction Time", "Avergage Congruent Reaction Time", "Average Reaction Time",
	    "Total_Incongruent_Trials", "Total_Congruent_Trials", "Total_Errors_Incongruent", "Total_Errors_Congruent", "Total_Correct_Incongruent", "Total_Correct_Congruent",
		"Incongruent Accuracy %", "Congruent Accuracy %",
		"Highest Level Completed", "Total Time", "Heart Rate", "SO2", "HRV",
		"Enjoyment (1-5)", "Would you play this again? (No or Yes)", "Positive Affect", "Negative Affect"
	};

	// Learning Curve settings
	static string[] LearningCurveHeader = { "Trial", "Day", "Level", "Congruent_Incongruent", "Left_Right",
											"Response (1=correct. 2=wrong. 3=timeout)", "Accuracy", "RT (ms)" };
	static string learningCurveHeaderString = string.Join(",", LearningCurveHeader);

	public const string learningCurveDayMacro = "[day]";
	public const string learningCurveTrialMacro = "[trial]";
	public static string[] learningCurveTrialSep = { learningCurveTrialMacro };
	public const System.StringSplitOptions lCTSplitOptions = System.StringSplitOptions.None;


	// Other variables
	public static BoogieType CurrentBoogieType = BoogieType.Intervention;


	// Call this to save trial data into the .csv file
	public static void SaveData(string ID, ExperimentalCondition condition, string[] newData) {
		if (CurrentBoogieType == BoogieType.Showcase) {
			return;
		}

		if (string.Equals(ID, "")) {
			ID = "(No ID entered)";
		}

		string[] newInfoArray = createInfoArray(ID, condition);

		// Save backup of data
		SaveBackup(ID, newInfoArray, newData);

		// Read the current data and make sure the header is correct
		string[][] AllData = readCSVFile(DataFilePath);

		// Get ID
		int lineIndex = findID(AllData, ID);

		// If ID not found in the previous data, save in a new line
		if (lineIndex < 0) {
			string[][] newAllData = new string[AllData.Length + 1][];
			AllData.CopyTo(newAllData, 0);

			string[] newLine = new string[newInfoArray.Length]; // Does this give the right length?
			lineIndex = newAllData.Length - 1;
			newAllData[lineIndex] = newLine;
			newInfoArray.CopyTo(newAllData[lineIndex], 0);

			AllData = newAllData;
		}

		// Make sure the newData is the right length
		if (newData.Length != DataHeader.Length) {
			Debug.LogError("Unexpected data array length for intervention data saving");
		}

		// Copy into the data array
		string[] prevData = AllData[lineIndex];
		AllData[lineIndex] = new string[prevData.Length + newData.Length];
		prevData.CopyTo(AllData[lineIndex], 0);
		newData.CopyTo(AllData[lineIndex], prevData.Length);

		// Create the data header and copy over the top of the file
		int lineLength = maxDataRowLength(AllData);
		makeDataHeader(lineLength).CopyTo(AllData, 0);

		// Convert the data to one string and write it to the file
		string dataString = convertToString(AllData);
		File.WriteAllText(DataFilePath, dataString);
		Debug.Log("Intervention data successfully saved");
	}


	// Read the data from a .csv and return it as a string[][]
	private static string[][] readCSVFile(string path) {
		string[][] allData;
		if (File.Exists(path)) {
			string[] lines = File.ReadAllLines(path);
			if (lines.Length > 1) {
				allData = new string[lines.Length][];

				for (int i = 0; i < lines.Length; i++) {
					string[] fields = lines[i].Split(new char[] { ',' });
					allData[i] = new string[fields.Length];
					fields.CopyTo(allData[i], 0);
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
			if (data[i][0] == ID) {
				return i;
			}
		}

		return -1;
	}

	// Create the info array
	private static string[] createInfoArray(string ID, ExperimentalCondition condition) {
		string[] infoArray = new string[InfoHeader.Length];

		infoArray[0] = ID;
		infoArray[1] = condition.ToString();

		return infoArray;
	}


	// Set up the header for the data CSV
	private static string[][] makeDataHeader(int lineLength) {
		string[][] header = new string[1][];
		//header [0] = new string[lineLength + DataHeader.Length];
		int numberOfHeaders = Mathf.Min((lineLength / DataHeader.Length) + 1, 6);
		header[0] = new string[InfoHeader.Length + DataHeader.Length * numberOfHeaders];

		// Header line 1 (Column titles)
		InfoHeader.CopyTo(header[0], 0);

		for (int i = 0; i < numberOfHeaders; i++) {
			string prefix = "Day " + (i + 1).ToString() + "_";
			string[] prefixedHeader = addPrefix(DataHeader, prefix);
			prefixedHeader.CopyTo(header[0], InfoHeader.Length + i * DataHeader.Length);
		}

		return header;
	}

	// Add a prefix to every string in a string[]
	private static string[] addPrefix(string[] array, string prefix) {
		string[] newArray = new string[array.Length];
		for (int i = 0; i < array.Length; i++) {
			newArray[i] = prefix + array[i];
		}
		return newArray;
	}


	// Convert string[][] to string
	private static string convertToString(string[][] TwoDArray) {
		string output = "";
		foreach (string[] line in TwoDArray) {
			output = output + string.Join(",", line) + "\n";
		}
		return output;
	}


	// Get the max data row length
	private static int maxDataRowLength(string[][] currentData) {
		int maxLength = 1;

		for (int i = 1; i < currentData.Length; i++) {
			for (int j = 1; j < currentData[i].Length; j++) {
				if (currentData[i][j] != "" && j > maxLength) {
					maxLength = j;
				}
			}
		}

		return maxLength;
	}


	// Save the new line of data to the backup folder for the kiddo
	private static void SaveBackup(string ID, string[] infoLine, string[] newData) {
		string infoString = string.Join(",", infoLine);
		string dataString = string.Join(",", newData);

		dataString = string.Concat(infoString, ",", dataString, "\n");

		string path = BackupInterventionDataPath + ID + ".csv";
		if (!File.Exists(path)) {
			Directory.CreateDirectory(BackupInterventionDataPath);
		}
		File.AppendAllText(path, dataString);
		Debug.Log("Backup intervention data saved");
	}


	// Append a single trial to the learning curve data
	public static void SaveLearningCurveData(string ID, string dataString, int trialsPerGame) {
		if (!File.Exists(LearningCurveDataPath)) {
			Directory.CreateDirectory(LearningCurveDataPath);
		}

		string day = "2";
		string path = LearningCurveDataPath + ID + "_LearningCurve.csv";
		if (!File.Exists(path)) {
			File.AppendAllText(path, learningCurveHeaderString);
			day = "1";
			dataString = replaceTrialMacros(dataString, 0);
		} else {
			dataString = replaceTrialMacros(dataString, trialsPerGame);
		}

		dataString = dataString.Replace(learningCurveDayMacro, day);
		File.AppendAllText(path, dataString);
	}

	// Substitute each trial macro with the corresponding trial number
	private static string replaceTrialMacros(string dataString, int firstTrial) {
		string[] splits = dataString.Split(learningCurveTrialSep, lCTSplitOptions);

		for (int i = 1; i < splits.Length; i++) {
			splits[i] = (firstTrial + i).ToString() + splits[i];
		}

		return string.Join("", splits);
	}
}
