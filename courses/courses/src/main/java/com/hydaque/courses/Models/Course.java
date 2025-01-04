package com.hydaque.courses.Models;


import java.util.Date;

import org.springframework.data.mongodb.core.mapping.Document;

import com.hydaque.courses.Enums.CourseStatus;

import org.springframework.data.annotation.Id;

@Document(collection = "CoursesCollection")

public class Course {
    @Id private Long id;
    private String title;
    private String description;
    private Date createdDate;
    private CourseStatus courseStatus;
    private CourseCategory category;


    public Course(Long id, String title, String description, Date createdDate, CourseStatus courseStatus, CourseCategory category) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.createdDate = createdDate;
        this.courseStatus = courseStatus;
        this.category = category;
    }


    public Long getId() {
        return this.id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getTitle() {
        return this.title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getDescription() {
        return this.description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public Date getCreatedDate() {
        return this.createdDate;
    }

    public void setCreatedDate(Date createdDate) {
        this.createdDate = createdDate;
    }

    public CourseStatus getCourseStatus() {
        return this.courseStatus;
    }

    public void setCourseStatus(CourseStatus courseStatus) {
        this.courseStatus = courseStatus;
    }

    public CourseCategory getCategory() {
        return this.category;
    }

    public void setCategory(CourseCategory category) {
        this.category = category;
    }

}
