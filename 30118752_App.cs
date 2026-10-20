/*
 * Assessment Manager
 * Cameron Kew 
 * 30118752
 * 
 * This application is used to manage assessment details.
 * Users can View, Add, and Delete Assessments.
 * Each assessment stores the unit/cluster name, assessment name, assessment type, and due date.
 * 
 * Assessment data is saved to a text file
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;

namespace _30118752_AT2_App
{
    internal class AssessmentManager
    {
        static void Main(string[] args)
        {
            //name of the file used to permanently assessments
            string textFile = "assessments.txt";

            //load any existing assessments from the text file
            List<string> assessments = LoadAssessmentsFromFile(textFile);

            //controls whether the program should continue running 
            bool exit = false; 

            //continue displaying the menu until the user chooses to exit 
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("==== Assessment Manager ====");
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. View Assessments");
                Console.WriteLine("2. Add New Assessment");
                Console.WriteLine("3. Delete Assessment");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Select an Option:");

                //read the user's menu selection
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        //display all assessments
                        DisplayAssessments(assessments);
                        Pause("Press any key to continue...");
                        break;
                    case "2":
                        //add a new assessment
                        AddAssessment(assessments, textFile);
                        break;
                    case "3":
                        //display assessments before asking which one to delete
                        DisplayAssessments(assessments);
                        DeleteAssessments(assessments, textFile);
                        break;
                    case "4":
                        //end the program
                        exit = true;
                        break;
                    default:
                        //handle an invlaid menu selection
                        Console.WriteLine("Invalid Option. Please try again.");
                        break;
                }

            }


        }

        static List<string> LoadAssessmentsFromFile(string filePath)
        {
            //create an empty list to store the assessment records
            List<string> assessmentList = new List<string>();

            //check whether the assessment file exists
            if (File.Exists(filePath))
            {
                //open the file in read mode
                using (StreamReader reader = new StreamReader(filePath))
                {
                    //continue reading until the end of the file is reached
                    while (!reader.EndOfStream)
                    {
                        //read the next line from the file
                        string? line = reader.ReadLine();

                        //only add lines that contain assessment information
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            assessmentList.Add(line);
                        }
                    }
                }
            }
            //return the collection containing the loaded assessments
            return assessmentList;
        }

        static void DisplayAssessments(List<string> assessments)
        {
            //check if any assessments currently exist
            if (assessments.Count == 0)
            {
                Console.WriteLine("No assessments recorded.");
            }
            else
            {
                //loop through every assessment in the collection
                for (int i = 0; i < assessments.Count; i++)
                {
                    //separate the assssment information
                    string[] parts = assessments[i].Split('|');

                    //check that the assessment contain all four required fields
                    if (parts.Length == 4)
                    {
                        //display assessment information to the user
                        Console.WriteLine(
                            $"{i + 1}. Unit/Cluster: {parts[0]}, " +
                            $"Assessment: {parts[1]}," +
                            $"Type: {parts[2]}, " +
                            $"Due Date: {parts[3]} "
                            );
                    }
                }
            }
        }

        static void WriteAssessmentsToFile(List<string> assessmentList, string filePath)
        {
            //open the file in write mode
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                //write each assessment to the file one line at a time
                foreach (string assessment in assessmentList)
                {
                    writer.WriteLine(assessment);
                }
            }
        }

        static void AddAssessment(List<string> assessments, string filePath)
        {
            //ask user to enter the unit or cluster name
            Console.Write("Enter unit/cluster: ");
            string? unitName = Console.ReadLine();

            //ask the user to enter the assessment name
            Console.Write("Enter the assessment name: ");
            string? assessmentName = Console.ReadLine();

            //check whether the assessment name has already been used
            bool duplicateName = false;

            foreach (string assessment in assessments)
            {
                //separate the existing assessment into its individual fields
                string[] parts = assessment.Split('|');

                //check that the record contains the expected fields 
                if (parts.Length == 4)
                {
                    //compare the existing assessment name with the new name
                    if (parts[1].Equals(assessmentName, StringComparison.OrdinalIgnoreCase))
                    {
                        duplicateName = true;
                        break;
                    }
                }
            }

            //prevent the assessment from being added if the name already exists
            if (duplicateName)
            {
                Console.WriteLine("An assessment with this name already exists.");
                Pause("Press any key to continue...");
                return;
            }

            //ask the user to enter the assessment type
            Console.Write("Enter the assessment type: ");
            string? assessmentType = Console.ReadLine();

            //ask the user to enter the due date
            Console.Write("Enter the due date (DD-MM-YYYY): ");
            string? dateInput = Console.ReadLine();

            //check that the date entered follows the required format
            if (!DateTime.TryParseExact(
                dateInput,
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime assessmentDate))
            {
                Console.WriteLine("Invalid date format.");
                Pause("Press any key to continue...");
                return;
            }

            //combine 4 pieces of information into one text record
            string newAssessment =
                $"{unitName}|{assessmentName}|{assessmentType}|{assessmentDate:dd-MM-yyyy}";

            //add the new assessment to the collection
            assessments.Add(newAssessment);

            //save the updated collection to the text file
            WriteAssessmentsToFile(assessments, filePath);

            Console.WriteLine("Assessment added successfully");

            //display the updated assessment list
            DisplayAssessments(assessments);

            Pause("Press any key to continue...");
        }

        static void DeleteAssessments(List<string> assessments, string filePath)
        {
            //ask the user which assessment number they want to delete
            Console.Write("Enter the number of assessment to delete:");

            //check the input is a valid number and is within the list range
            if ((int.TryParse(Console.ReadLine(), out int assessmentNumber) && assessmentNumber > 0) && (assessmentNumber <= assessments.Count))
            {
                //remove the selected assessment from collection
                assessments.RemoveAt(assessmentNumber - 1);

                //save the updated collection to the text file
                WriteAssessmentsToFile(assessments, filePath);

                Console.WriteLine("Assessment deleted successfully!");
            }
            else
            {
                //display an error when selected number is invalid
                Console.WriteLine("Invalid assessment number.");
            }

            //display the updated assessment list
            DisplayAssessments(assessments);

            Pause("Press any key to continue...");
        }

        static void Pause(string message)
        {
            //display the supplied message
            Console.WriteLine(message);

            //wait for the user to press a key
            Console.ReadKey();
        }
    }
}
