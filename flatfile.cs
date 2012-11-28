using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Threading;

public class flatfile
{


	static void Main()
	{
		char firstLetterLowercase;
		string lastNamelowercase, firstName, completeLine;
		string inputFilename, outputFilename;

		OpenFileDialog fileOpen = new OpenFileDialog ();
			    

		fileOpen.InitialDirectory = ".\\";
		fileOpen.Filter = "Comma Delimited (*.csv)|*.csv|All files (*.*)|*.*";
		fileOpen.FilterIndex = 0;
		fileOpen.RestoreDirectory = false; //true;
		//                if (fileOpen.ShowDialog () == DialogResult.Cancel)
		fileOpen.ShowDialog();

		//Collect the input filename
		inputFilename = fileOpen.FileName;

		string path = Path.GetDirectoryName(inputFilename);
		string todaysDate = DateTime.Now.ToString("yyMMdd");
		outputFilename = path + @"\" + todaysDate + ".txt";
	
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
				
				//create lowercase username and add it to the end of the line
				firstName = parts[1].ToLower();
				firstLetterLowercase = firstName[0];
				lastNamelowercase =parts[0].ToLower();
				
				completeLine += firstLetterLowercase + lastNamelowercase;
				
				System.Console.WriteLine(completeLine);
				writer.WriteLine(completeLine);
				
			}



		}
		}
}
