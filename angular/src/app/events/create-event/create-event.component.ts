import { Component, ViewChild, Injector, Output, EventEmitter, ElementRef, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { EventServiceProxy, CreateEventInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'create-event-modal',
    templateUrl: './create-event.component.html'
})
export class CreateEventComponent extends AppComponentBase implements OnInit {

    @ViewChild('createEventModal')
    modal: ModalDirective;
    @ViewChild('modalContent')
    modalContent: ElementRef;

    active: boolean = false;
    saving: boolean = false;
    event = new CreateEventInput;

    @Output()
    modalSave: EventEmitter<any> = new EventEmitter<any>();

    constructor(injector: Injector, private _eventService: EventServiceProxy) {
        super(injector);
        console.log('moment',moment());
    }

    ngOnInit(): void {
        console.log('ngOnInit fired in CreateRoleComponent');
    }

    show(): void {
        this.active = false;
        this.event = new CreateEventInput;
        this.modal.show();
    }

    onShown(): void {
        $.AdminBSB.input.activate($(this.modalContent.nativeElement));
    }

    save(): void {
        console.log('this.event', this.event);

      //var momentDate =  moment(this.event.date.toString()).startOf('day');
      this.event.date = moment(this.event.date.toString());//cut time on serverside

        //var dateSplit = this.event.date.split('-'); 
        //new Date(Date.UTC(e[0], e[1] - 1, e[2]));
        this.saving = true;
        this._eventService.create(this.event).finally(() => { this.saving = false }).subscribe(() => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
            this.modalSave.emit(null);
        });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    getTodayDate(): string {
        var mnow = moment.now();
        console.log(mnow);

        return '2018-01-21';
    }

    today: string = new Date().toJSON().split('T')[0];
}
