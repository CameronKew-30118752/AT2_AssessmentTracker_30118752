/* Project: Assessment Tracker
 * Cameron Kew
 * 30118752
 * C#One 
 * AT2.3
 * File: Assessment.cs
 * 
 * Description:
 * This file defines the assessment class, which represents an individual
 * assessment. It stores the unit name, assessment name, assessment type, 
 * and due date using properties.
 */


using System;

//Stores the details of one assessment
public class Assessment
{
    //Gets or sets the unit/cluster name
    public string UnitName { get; set; } = "";

    //Gets or sets the assessment name
    public string AssessmentName { get; set; } = "";

    //Gets or sets the assessment type
    public string AssessmentType { get; set; } = "";

    //Gets or sets the due date
    public DateTime DueDate { get; set; } 

}