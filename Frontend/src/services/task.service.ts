import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TaskDto } from 'src/dto/task.dto';
import { Task } from 'src/models/task.model';

@Injectable()
export class TaskService 
{
    private _todoConttrollerUrl = "http://localhost:5277/rest/todo";

    constructor(private http: HttpClient)
    {
    }

    public GetAll()
    {
        return this.http.get( `${this._todoConttrollerUrl}/get-all` );
    }

    public Create( taskTitle: string )
    {
        let taskDto: TaskDto = 
        {
            id: 0,
            title: taskTitle,
            isDone: false
        };
        return this.http.post( `${this._todoConttrollerUrl}/create`, taskDto );
    }

    public Complete( taskId: number )
    {
        this.http.put( `${this._todoConttrollerUrl}/complete/${taskId}`, `` ).subscribe();
    }

    public Delete( taskId: number )
    {
        this.http.delete( `${this._todoConttrollerUrl}/delete/${taskId}` ).subscribe();
    }

    public ParseDto( dto: TaskDto ) : Task
    {
        let task: Task = 
        {
            id: dto.id,
            title: dto.title,
            isDone: dto.isDone
        };
        return task;
    }

    public ToDto( task: Task ) : TaskDto
    {
        let dto: TaskDto = 
        {
            id: task.id,
            title: task.title,
            isDone: task.isDone
        };
        return dto;
    }
}