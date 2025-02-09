using System;

public class UserFactory
{
    IUtilityService utilityService;
    public UserFactory(IUtilityService utilityService){
        this.utilityService = utilityService;
    }
    public User CreateUser(UserDto userDto)
    {
        var user = GetDerivedUser(userDto);
        user.fullName = userDto.fullName;
        user.email = userDto.email;
        user.username = userDto.username;
        user.password = utilityService.HashPassword(userDto.Password);
        user.mobile = userDto.mobile;
        user.createdDate = DateTime.UtcNow;
        return user;
    }
    private User GetDerivedUser(UserDto userDto){
        
        return userDto switch
        {
            TeacherDto teacherDto => new Teacher() { Major = teacherDto.Major, GraduationYear = teacherDto.GraduationYear },
            StudentDto studentDto => new Student() {  studentCode = studentDto.studentCode },
            AdminDto adminDto => new Admin() { credsToken = adminDto.credsToken },
            _ => throw new ArgumentException("Invalid DTO type."),
        };
    }
}