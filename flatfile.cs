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
		//                if (fileOpen.ShowDialog () == DialogResult.Cancel)
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
		int LAST_NAME_CHAR_COUNT = 50;
		int FIRST_NAME_CHAR_COUNT = 18;
		int EMAIL_CHAR_COUNT = 70;

		//load all the lines of the file into memory
		string[] lines = File.ReadAllLines(inputFilename);
		using (var writer = new StreamWriter(outputFilename))
		{
			
			foreach (string line in lines)
			{
				string[] parts = line.Split(',');
				
				//Add last name to the line
				writer.Write(parts[0]);
				//Add required amount of white space
				writer.Write(addWhitespace(LAST_NAME_CHAR_COUNT, parts[0].Length));


				//Add first name to the line
				writer.Write(parts[1]);
				writer.Write(addWhitespace(FIRST_NAME_CHAR_COUNT, parts[1].Length));
				
				//Add company and email address
				writer.Write("GSS" + parts[2]);
				writer.Write(addWhitespace(EMAIL_CHAR_COUNT, parts[2].Length));
				
				//Add username to the line
				writer.WriteLine(createUsername (parts[1], parts[0]));
				
			}
		}
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
