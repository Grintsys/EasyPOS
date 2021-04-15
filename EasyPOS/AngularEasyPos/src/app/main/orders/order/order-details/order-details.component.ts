import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Order } from '../../order.model';
import { OrderService } from '../../order.service';

import { locale as english } from '../../i18n/en';
import { locale as spanish } from '../../i18n/es';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit {

  orderForm: FormGroup;
  order: Order;
  pageType: string;

  // Private
  private _unsubscribeAll: Subject<any>;

  /**
  * Constructor
  *
  * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
  * @param {FuseTranslationLoaderService} _orderService
  * @param {FormBuilder} _formBuilder
 */
  constructor(
      private _fuseTranslationLoaderService: FuseTranslationLoaderService,
      private _orderService: OrderService,
      private _formBuilder: FormBuilder,
  )
  {
      this._fuseTranslationLoaderService.loadTranslations(english, spanish);

      // Set the default
      this.order = new Order();

      // Set the private defaults
      this._unsubscribeAll = new Subject();
  }


  /**
   * On init
   */
  ngOnInit(): void
  {
      // Subscribe to update order on changes
      this._orderService.onOrderChanged
          .pipe(takeUntil(this._unsubscribeAll))
          .subscribe(order => {

              if ( order )
              {
                  this.order = new Order({
                      subtotal: 100,
                      discount: -10,
                      tax: 10,
                      total: 100,
                      toPay: 500,
                      exchange: 400,
                      paymentType: 'Tarjeta de credito',
                      customerCode: 'C-2021-01',
                      customerName: 'Juan Antonio Paz',
                      identity: '1614188500185',
                      RTN: '16141885001850',
                      address: 'San Pedro Sula',
                      documentType: 'Credito',
                      status: 'Cancelada',
                  });
                  this.pageType = 'edit';
              }
              else
              {
                  this.pageType = 'new';
              }
              this.orderForm = this.createorderForm();
          });
  }


  /**
  * On destroy
  */
  ngOnDestroy(): void
  {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Public methods
  // -----------------------------------------------------------------------------------------------------

  /**
   * Create order form
   *
   * @returns {FormGroup}
  */
   createorderForm(): FormGroup
  {
    return this._formBuilder.group({
      subtotal          : [{value: this.order.subtotal, disabled: true}],
      discount          : [{value: this.order.discount , disabled: true} ],
      tax               : [{value: this.order.tax, disabled: true} ],
      total             : [{value: this.order.total, disabled: true} ],
      toPay             : [{value: this.order.toPay, disabled: true} ],
      exchange          : [{value: this.order.exchange, disabled: true} ],
      paymentType       : [{value: this.order.paymentType, disabled: true} ],
      customerCode      : [{value: this.order.customerCode, disabled: true} ],
      customerName      : [{value: this.order.customerName, disabled: true} ],
      identity          : [{value: this.order.identity, disabled: true} ],
      RTN               : [{value: this.order.RTN, disabled: true} ],
      address           : [{value: this.order.address, disabled: true} ],
    });
  }

}
