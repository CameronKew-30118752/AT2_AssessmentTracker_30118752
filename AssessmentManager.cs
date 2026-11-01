/* Project: Assessment Tracker
 * Cameron Kew
 * 30118752
 * C#One 
 * AT2.3
 * File: AssessmentManager.cs
 * 
 * Description:
 * This file manages the assessment collection and file operations.
 * It loads assessmens from a text file, adds and removes assessments
 * through the shared collection, and saves assessment information
 * back to the text file.
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

//Manages the assessment collection and its text file
public class AssessmentManager
{
    // Stores the name of the text file used to save assessment records
    // Readonly keyword prevents the file path from being changed after being initialised
    private readonly string filePath = "assessments.txt";

    //Stores assessments currently loaded into the application
    public List<Assessment> Assessments { get; private set; } = new List<Assessment>();


    //Loads existing assessment records from the text file, if exists 
    public void LoadAssessmentsFromFile()
    {
        // Clears the current collection before loading records, preventing duplicates
        Assessments.Clear();

        // Check whether the assessment text file exists
        // If it doesn't, there is nothing to load so the method returns
        if (!File.Exists(filePath))
        {
            return;
        }

        // Open text file in read mode
        using (StreamReader reader = new StreamReader(filePath))
        {
            // Stores each line read from the text file
            string? line;

            // Read each line sequentially until end of file reached
            while ((line = reader.ReadLine()) != null)
            {
                // Check whether the current line is empty 
                // Empty lines are skipped so they are not processed as an assessment
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                // Split each part of the record using | e.g C#One|AT2.3
                string[] parts = line.Split('|');

                // Check that the line contains exactly 4 values
                if (parts.Length == 4 && DateTime.TryParseExact(parts[3], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDate))
                {
                    // Create a new assessment object using values read from the file
                    Assessment assessment = new Assessment
                    {
                        UnitName = parts[0],
                        AssessmentName = parts[1],
                        AssessmentType = parts[2],
                        DueDate = dueDate
                    };

                    // Add the new assessment to the collection 
                    Assessments.Add(assessment);
                }
            }
        }
    }

    //Saves all assessment records to the text file 
    public void SaveAssessmentsToFile()
    {
        // Open text file in write mode
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Assessment assessment in Assessments)
            {
                // Write each assessment as a single line in the text file
                writer.WriteLine(
                    $"{assessment.UnitName}|{assessment.AssessmentName}|" +
                    $"{assessment.AssessmentType}|{assessment.DueDate:dd-MM-yyyy}");
            }
        }
    }
}