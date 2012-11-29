#Flat File Creator

##Description

**Language**: C#    
**IDE**: monodevelop

This application was written to simplify the creation of Flat files for the Peoplesoft Leave Self Service Account creation process. A Flat file is a carefully formated .txt file that is uploaded to an ftp server and then used in an automated account creation process. The Trick to the Flat file is to insert the proper amount of spaces after each data point.

This application was created to save time when creating large batches of accounts 50 and up. The program runs in under 10 seconds, which by hand took nearly an hour.

###Libraries Used

using System;   
using System.IO;   
using System.Windows;   
using System.Windows.Forms;   


##What kind of input does the program require?

**A Properly formatted .csv file:**
The .csv file has three columns setup as follows   
Last Name | First Name | Email Address 

and thats it :)

##Features

**File Chooser** - When the program is run a dialog window pops up and asks for you to choose your csv file

**Error Reporting** - This creates an error file that lists the problem, where it occured in the csv and the data within that line

**Username Creation** - the program auto-generates usernames for each person. 