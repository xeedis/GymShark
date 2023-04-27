import { Component, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-opinions',
  templateUrl: './opinions.component.html',
  styleUrls: ['./opinions.component.css']
})
export class OpinionsComponent implements OnInit {
  @ViewChild('editForm') editForm:NgForm;
  @Input() canComment: boolean;
  @Output() close = true;

  constructor() { }

  ngOnInit(): void {
  }
  addOpinion(){
    this.close = !this.close;
  }

}
