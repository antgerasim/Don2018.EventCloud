import { Component, Injector, ViewChild, OnInit } from '@angular/core';
import { EventServiceProxy, EventListDto } from "shared/service-proxies/service-proxies";
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from 'shared/app-component-base';
import { CreateEventComponent } from 'app/events/create-event/create-event.component';

@
Component({
    templateUrl: './events.component.html',
    animations: [appModuleAnimation()]
})
export class EventsComponent extends AppComponentBase implements OnInit {

    //viewChild
    @ViewChild('createEventModal') createEventModal: CreateEventComponent;

    events: EventListDto[] = [];
    // filters: Object = { includeCancelledEvents: true };
    filters= { includeCancelledEvents: true };
    isTableLoading: boolean = false;

    constructor(
        injector: Injector,
        private _eventService: EventServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        //throw new Error("Not implemented");
        this.loadEvents();
    }

    protected loadEvents() {
        //debugger;
        this.isTableLoading = true;
        this._eventService.getEvents(this.filters.includeCancelledEvents).subscribe(result => {
            //debugger;
            console.log(result.items);
            this.events = result.items;
            this.isTableLoading = false;
        });
    }

    protected watchIncludeCancelledEventsCheckBox(event) {
        //console.log(event); // logs model value
        console.log(this.filters.includeCancelledEvents);
        this.loadEvents();

    }

    // Show Modals
   protected createEvent(): void {
        this.createEventModal.show();
    }
/*
    protected refresh(): void {
        this.getDataPage(this.pageNumber);
    }*/

}
