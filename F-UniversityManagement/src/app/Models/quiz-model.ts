import { Question } from "./question-model"

export interface Quiz{
    id:string,
    name : string,
    description : string,
    questionDTOs : Question[]
    courseId : string,
    status : number
}