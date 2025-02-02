import { CourseCategory } from "./CourseCategory";

export interface Course{
    id: number;
    title: string;
    description: string;
    status: string;
    category: CourseCategory;
}