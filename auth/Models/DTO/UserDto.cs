public class UserDto{
    public String fullName { get; set; }
    public String email { get; set; }
    public String username { get; set; }
    public String Password { get; set; }
    public String mobile { get; set; }
}
public class TeacherDto : UserDto{
    public int GraduationYear { get; set; }
    public String Major { get; set; }
}
public class StudentDto : UserDto{
    public String studentCode { get; set; }
}

public class AdminDto : UserDto{
    public String credsToken { get; set; }
}