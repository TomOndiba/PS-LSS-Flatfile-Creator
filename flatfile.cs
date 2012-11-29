using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;


public class flatfile
{
	static void Main()
	{
		//Collect the input filename
		string inputFilename = fileOpenPrompt ();
		writeFlatFile (inputFilename, createOutputFilename(inputFilename));
	}

	private static string fileOpenPrompt ()
	{
		OpenFileDialog fileOpen = new OpenFileDialog ();

		fileOpen.InitialDirectory = ".\\";
		fileOpen.Filter = "Comma Delimited (*.csv)|*.csv|All files (*.*)|*.*";
		fileOpen.FilterIndex = 0;
		fileOpen.RestoreDirectory = false; //true;
		fileOpen.ShowDialog();

		return fileOpen.FileName;
	}

	private static string createOutputFilename (string inputFilename)
	{ 
		string path = Path.GetDirectoryName(inputFilename);
		string todaysDate = DateTime.Now.ToString("yyMMdd");

		return path + @"\" + todaysDate + ".txt";
	}


	public static void writeFlatFile (string inputFilename, string outputFilename)
	{
		//constants
		int LAST_NAME_CHAR_COUNT = 50;
		int FIRST_NAME_CHAR_COUNT = 18;
		int EMAIL_CHAR_COUNT = 70;

		//local variables
		int lineNumber = 1;
		string errorFilename = outputFilename + "errors.txt";


		//load all the lines of the file into memory
		string[] lines = File.ReadAllLines (inputFilename);
		StreamWriter writer = new StreamWriter (outputFilename);
		StreamWriter errorWriter = new StreamWriter (errorFilename);
		foreach (string line in lines) {
			string[] parts = line.Split (',');

			//check to see if the line will write to the flat file properly
			if (!checkForErrors (errorWriter, parts, lineNumber)) {
				//Add last name to the line
				writer.Write (parts [0]);
				//Add required amount of white space
				writer.Write (addWhitespace (LAST_NAME_CHAR_COUNT, parts [0].Length));


				//Add first name to the line
				writer.Write (parts [1]);
				writer.Write (addWhitespace (FIRST_NAME_CHAR_COUNT, parts [1].Length));
					
				//Add company and email address
				writer.Write ("GSS" + parts [2]);
				writer.Write (addWhitespace (EMAIL_CHAR_COUNT, parts [2].Length));
					
				//Add username to the line
				writer.WriteLine (createUsername (parts [1], parts [0]));

				lineNumber++;
			}
		}

		//Close the Writing Stream
		writer.Close ();
		errorWriter.Close ();

		//Delete Error Log if empty
		if (new FileInfo (errorFilename).Length == 0) {
			try {
				System.IO.File.Delete(errorFilename);
			}
			catch (System.IO.IOException e) {
				Console.WriteLine(e.Message);
				return;
			}
		}

	}
	//method returns false if there are no errors
	//if there are errors it writes them to an error file
	private static Boolean checkForErrors (StreamWriter errorWriter, string[] parts, int lineNumber)
	{
		if (parts [0].Length > 50) {
			errorWriter.WriteLine ("Line Number: "+ lineNumber + " - Last Name is too Long: " + parts [0] + ", " + parts [1] + ", " + parts [2]);
		} else if (parts [1].Length > 18) {
			errorWriter.WriteLine ("Line Number: "+ lineNumber + " - First Name is too Long: " + parts [0] + ", " + parts [1] + ", " + parts [2]);
		} else if (parts [2].Length > 70) {
			errorWriter.WriteLine ("Line Number: "+ lineNumber + " - Email is too long: " + parts [0] + ", " + parts [1] + ", " + parts [2]);
		} else if (parts [0].Length == 0) {
			errorWriter.WriteLine ("Line Number: "+ lineNumber + " - No Last Name: " + parts [0] + ", " + parts [1] + ", " + parts [2]);
		} else if (parts [1].Length == 0) {
			errorWriter.WriteLine ("Line Number: "+ lineNumber + " - No First Name: " + parts [0] + ", " + parts [1] + ", " + parts [2]);
		} else if (parts [2].Length == 0) {
			errorWriter.WriteLine ("Line Number: "+ lineNumber + " - No Email: " + parts [0] + ", " + parts [1] + ", " + parts [2]);
		} else {
			return false;
		}
		return true;
	}

	private static string addWhitespace (int limit, int stringLength)
	{
		string whitespace = "";
		for (int i = 1; i <= (limit -stringLength); i++) {
			whitespace+= " ";
		}
		return whitespace;
	}

	private static string createUsername (string firstName, string lastName)
	{
		char firstLetterLowercase;

		//create lowercase username
		firstName = firstName.ToLower();
		firstLetterLowercase = firstName[0];


		return firstLetterLowercase + lastName.ToLower();;
	}


}
