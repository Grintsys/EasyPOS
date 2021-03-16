import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { ProductDto, ProductService } from '@proxy/product';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
  providers: [ListService]
})
export class ProductComponent implements OnInit {
  product = { items: [], totalCount: 0 } as PagedResultDto<ProductDto>;

  constructor(public readonly list: ListService, private productService: ProductService) {}

  ngOnInit(): void {
    const productStreamCreator = (query) => this.productService.getList(query);

    this.list.hookToQuery(productStreamCreator).subscribe((response) => {
      this.product = response;
    })
  }

}
