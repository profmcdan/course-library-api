using System;
using System.Collections;
using System.Collections.Generic;

namespace CourseLibrary.Models
{
    public class CreateAuthorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string MainCategory { get; set; }
        public ICollection<CreateCourseDto> Courses { get; set; } = new List<CreateCourseDto>();
    }
}