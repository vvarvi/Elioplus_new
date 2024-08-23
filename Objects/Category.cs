using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    public class TrainingCategory
    {
        public TrainingCategory()
        {
            Courses = new List<Course>();
        }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public List<Course> Courses { get; set; }        
    }

    public class Course
    {
        public Course()
        {
            Chapters = new List<Chapter>();
            Permissions = new Permissions();
        }

        public int CourseId { get; set; }

        public string CourseDescription { get; set; }

        public string OverviewText { get; set; }

        public string OverviewImagePath { get; set; }

        public string OverviewImageType { get; set; }

        public string OverviewImageName { get; set; }

        public List<Chapter> Chapters { get; set; }

        public Permissions Permissions { get; set; }
    }

    public class Permissions
    {
        public Permissions()
        {
            Countries = new List<string>();
        }

        public int CourseId { get; set; }

        public int TrainingGroupId { get; set; }

        public string TrainingGroupName { get; set; }

        public int TierId { get; set; }

        public string TierName { get; set; }

        public List<string> Countries { get; set; }
    }

    public class Chapter
    {
        public int ChapterId { get; set; }

        public int CourseId { get; set; }

        public string ChapterTitle { get; set; }

        public string ChapterText { get; set; }

        public string ChapterFilePath { get; set; }

        public string ChapterFileName { get; set; }

        public string ChapterLink { get; set; }

        public string ChapterStep { get; set; }

        public int IsViewed { get; set; }
    }
}