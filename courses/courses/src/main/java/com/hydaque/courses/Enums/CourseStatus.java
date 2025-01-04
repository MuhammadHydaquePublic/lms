package com.hydaque.courses.Enums;

import com.fasterxml.jackson.annotation.JsonProperty;

public enum CourseStatus {
    @JsonProperty
    inRevision,
    @JsonProperty
    active,
    @JsonProperty
    inactive,
    @JsonProperty
    suspended;
}
