
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Task } from '../models/task.model';
import { TaskService } from 'src/services/task.service';
import { takeLast } from 'rxjs';
import { TaskDto } from 'src/dto/task.dto';

@Component({
    selector: 'app-root',                 // HTML tag for this component
    templateUrl: './app.component.html',  // template connection for this component
    styleUrls: ['./app.component.css']    // styles for the template
})

export class AppComponent 
{
    tasks: Array<Task> = [];
    completedTasks: Array<Task> = [];

    public constructor( private _taskService: TaskService ) 
    {
        _taskService.GetAll().subscribe( raw => 
        {
            let array = <Array<TaskDto>>raw;
            array.forEach( raw => 
            {
                let task = _taskService.ParseDto( <TaskDto>raw );
                if ( !task.isDone ) this.tasks.push( task );
                else this.completedTasks.push( task );
            });
        });
    }

    public addTask( myForm: NgForm ) 
    {
        this._taskService.Create( myForm.value.task ).subscribe( raw => 
        {
            let task = this._taskService.ParseDto( <TaskDto>raw );
            this.tasks.push( task ) 
        });
    }

    public onCompleteTask( task: Task )
    {
        let index = this.tasks.findIndex( el => el.id == task.id );
        if ( index < 0 ) return;

        task.isDone = true;
        this.tasks.splice( index, 1 );
        this.completedTasks.push( task );
        this._taskService.Complete( task.id );
    }

    public onDeleteTask( task: Task ): void 
    {
        this._taskService.Delete( task.id );
        if ( !task.isDone )
            this.tasks = this.tasks.filter( el => el.id != task.id );
        else 
            this.completedTasks = this.completedTasks.filter( el => el.id != task.id );
    }
}
