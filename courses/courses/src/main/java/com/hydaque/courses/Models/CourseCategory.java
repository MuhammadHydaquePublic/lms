package com.hydaque.courses.Models;

import javax.persistence.Entity;

import org.springframework.data.annotation.Id;

@Entity
public class CourseCategory {
    @Id int id;
    String catName;
    String description;


    public CourseCategory() {
    }

    public int getId() {
        return this.id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getCatName() {
        return this.catName;
    }

    public void setCatName(String catName) {
        this.catName = catName;
    }

    public String getDescription() {
        return this.description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

}