import { Component, ApplicationRef, ComponentRef } from '@angular/core';
import { NDDSidebarService } from '../../shared/ndd-ng-sidebar';
import { AppComponent } from '../../app.component';

@Component({
    selector: 'ndd-layout',
    templateUrl: './layout.component.html',
})
export class LayoutComponent {
    public pinned: boolean = false;

    constructor(
        private sidebarService: NDDSidebarService,
        private appRef: ApplicationRef,
    ) { }

    public setPin(pinned: boolean): void {
        this.pinned = pinned;
    }

    public toggleSidebar(): void {
        this.sidebarService.toggleCollapse();
        /*
        const rootApp: AppComponent = this.appRef.components[0].instance as AppComponent;
        const component: ComponentRef<any> = rootApp.viewContainerRef.createComponent(undefined as any);

        return component.instance.onClose.take(1).subscribe(() => {
            component.destroy();
        });

        */
    }
}
