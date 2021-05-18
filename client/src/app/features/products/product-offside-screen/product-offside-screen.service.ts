import { Injectable, ApplicationRef, ComponentRef, ComponentFactoryResolver, ComponentFactory, Injector, Type } from '@angular/core';
import { AppComponent } from '../../../app.component';

@Injectable()
export class ProductOffsideScreenService {

    private component: ComponentRef<any>;

    constructor(
        private appRef: ApplicationRef,
        private componentFactoryResolver: ComponentFactoryResolver,
    ) { }

    public createComponent<T>(component: Type<T>): ComponentRef<T> {
        const rootApp: AppComponent = this.appRef.components[0].instance as AppComponent;

        const componentFactory: ComponentFactory<any> =
                this.componentFactoryResolver.resolveComponentFactory(component);

         this.component = rootApp.viewContainerRef.createComponent(componentFactory);

        return this.component.instance;
    }

    public close(): void {
        this.component.destroy();
    }

}
