package com.hydaque.courses.Models;

import org.springframework.data.mongodb.repository.MongoRepository;

public interface CourseRepository extends MongoRepository<Course,Long> {
    
}
