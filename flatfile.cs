using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Threading;

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
	private static void writeFlatFile (string inputFilename, string outputFilename)
	{
		string completeLine;

		//load all the lines of the file into memory
		string[] lines = File.ReadAllLines(inputFilename);
		using (var writer = new StreamWriter(outputFilename))
		{
			
			foreach (string line in lines)
			{
				string[] parts = line.Split(',');
				
				//Add last name to the line
				completeLine = parts[0];
				
				//Add required amount of white space
				for (int i = 1; i <= (50 -parts[0].Length); i++) {
					completeLine += " ";
				}
				
				//Add first name to the line
				completeLine +=parts[1];
				
				//Add required amount of white space
				for (int i = 1; i <= (18 - parts[1].Length); i++) {
					completeLine += " ";
				}
				
				//Add company and email address
				completeLine += "GSS";
				completeLine += parts[2];
				
				//Add required amount of white space
				for (int i = 1; i <= (70 - parts[2].Length); i++) {
					completeLine += " ";
				}
				
				//Add username to the line
				completeLine += createUsername (parts[1], parts[0]);
				
				System.Console.WriteLine(completeLine);
				writer.WriteLine(completeLine);
				
			}
		}
	}
	private static string createUsername (string firstName, string lastName)
	{
		char firstLetterLowercase;
		string username;

		//create lowercase username
		firstName = firstName.ToLower();
		firstLetterLowercase = firstName[0];
		lastName = lastName.ToLower();

		username = firstLetterLowercase + lastName;

		return username;
	}


}
