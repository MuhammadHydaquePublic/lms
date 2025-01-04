package com.hydaque.courses;

import java.util.List;
import java.util.Optional;

import org.apache.catalina.connector.Response;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

import com.hydaque.courses.Models.Course;
import com.hydaque.courses.Models.CourseRepository;

import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.PutMapping;




@RestController
@RequestMapping("/")
public class CourseController {
    @Autowired
    private final CourseRepository repo;

    public CourseController(CourseRepository repo){
        this.repo = repo;
    }

    @GetMapping()
    public ResponseEntity getCourse() {
        return ResponseEntity.ok(repo.findAll());
    }

    @GetMapping("/{id}")
    public ResponseEntity getCourse(@PathVariable Long id) {
        Optional<Course> foundCourse = repo.findById(id); 
        if(foundCourse.isPresent()){
        return ResponseEntity.ok(foundCourse.get());
        }
        return ResponseEntity.notFound().build();
    }
    
    @PostMapping()
    public ResponseEntity<Course> postCourse(@RequestBody @Validated Course course) {
        repo.save(course);
        return ResponseEntity.ok(course);
    }
    
    @PutMapping("/{id}")
    public ResponseEntity putCourse(@PathVariable Long id, @RequestBody Course course) {
        if(repo.existsById(id)){
            Course courseToUpdate = repo.findById(id).get();
            courseToUpdate = course;
            repo.save(courseToUpdate);
            return ResponseEntity.noContent().build();
        }
        return ResponseEntity.notFound().build();
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<String> deleteCourse(@PathVariable Long id) {
        if(repo.existsById(id)){
            repo.deleteById(id);
            return ResponseEntity.ok("{\"Result\":\"Course Deleted\"}");
        }
        return ResponseEntity.notFound().build();
        
    } 
}