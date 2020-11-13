using System;
using CourseLibrary.Entities;

namespace CourseLibrary.Models
{
    public class CreateCourseDto
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}