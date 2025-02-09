using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using Microsoft.EntityFrameworkCore;

[Index(nameof(email), nameof(username),IsUnique =true)]
public class User{
    [Key]
    public int id { get; set; }
    public String fullName { get; set; }
    [EmailAddress]
    public String email { get; set; }
    [MinLength(8)]
    [MaxLength(20)]
    public String username { get; set; }
    public String password { get; set; }
    [RegularExpression("^01\\d{9}$", ErrorMessage = "Mobile number must start with 01 and be 11 digits long.")]
    public String mobile { get; set; }
    public DateTime createdDate { get; set; }
    public int createdByUserId { get; set; }
    
}

public class Teacher : User{
    public String Major { get; set; }
    [Range(1900,2024)]
    public int GraduationYear { get; set; }
}
public class Student : User{
    public String studentCode { get; set; }
}
public class Admin : User{
    public String credsToken { get; set; }
}