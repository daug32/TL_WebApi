import { Component, Output, Input, EventEmitter } from '@angular/core';
import { Task } from '../../models/task.model';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})

export class TaskComponent {
  @Input() task!: Task;
  
  @Output() onDeleteTask = new EventEmitter<Task>();
  @Output() onCompleteTask = new EventEmitter<Task>();

  delete = () => this.onDeleteTask.emit( this.task );
  complete = () => this.onCompleteTask.emit( this.task );
}
