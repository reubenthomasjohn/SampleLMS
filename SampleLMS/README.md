### TODO:

- [ ] Create one controller for Users
	- [ ] Should be able to CRUD Users
	- [ ] Endpoint for registration
		- [ ] Password needs to be salted and hashed
	- [ ] Endpoint to login
	- [ ] Use Identity Framework to create roles for authorization (Student, Instructor, Admin, Student)
	
- [ ] Create one controller for Courses
	- [ ] Instructors should be able to CRUD courses
		- [ ] Course Model - Title, Description, Duration, Category, CourseContent(?)

- [ ] Create one controller for Course Content
	- [ ] Should be able to CRUD Course Content
	- [ ] Within each course, instructors should be able to create assignments, quizzes, assessments
	- [ ] Support uploading different kinds of content
	- [ ] Content should be organized by modules within a course
	- [ ] CourseContent therefore is ICollection<Module>
 

