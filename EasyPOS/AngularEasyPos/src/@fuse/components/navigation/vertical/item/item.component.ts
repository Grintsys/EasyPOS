import { ChangeDetectorRef, Component, HostBinding, Input, OnDestroy, OnInit } from '@angular/core';
import { merge, Subject, Subscription } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { FuseNavigationItem } from '@fuse/types';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { SharedService } from 'app/shared.service';

@Component({
    selector: 'fuse-nav-vertical-item',
    templateUrl: './item.component.html',
    styleUrls: ['./item.component.scss']
})
export class FuseNavVerticalItemComponent implements OnInit, OnDestroy {
    @HostBinding('class')
    classes = 'nav-item';

    @Input()
    item: FuseNavigationItem;

    private _unsubscribeAll: Subject<any>;
    userRole: string = 'admin';
    userRoles: string[] = [];
    subscription: Subscription;

    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseNavigationService: FuseNavigationService,
        private _sharedService: SharedService
    ) {
        // Set the private defaults
        this._unsubscribeAll = new Subject();

        this.subscription = _sharedService.userRoles$.subscribe(
            list => {
                this.userRoles = list;
            }
        );
    }

    ngOnInit(): void {
        // Subscribe to navigation item
        merge(
            this._fuseNavigationService.onNavigationItemAdded,
            this._fuseNavigationService.onNavigationItemUpdated,
            this._fuseNavigationService.onNavigationItemRemoved
        ).pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => {

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
    }



    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}
