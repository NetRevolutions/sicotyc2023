import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styles: [
  ]
})
export class PaginationComponent {
  @Input() totalItems: number = 0;
  @Input() itemsPerPage: number = 5;
  @Output() paginaCambiada = new EventEmitter<number>();

  currentPage: number = 1;

  constructor() { }

  onPageChange(page: number) {
    this.currentPage = page;
    this.paginaCambiada.emit(page);
  }

  totalPages(): number {
    return Math.ceil(this.totalItems / this.itemsPerPage) || 1;
  }

  range(): number[] {
    const rangeSize = 5;
    const totalPages = this.totalPages();
    const currentPage = this.currentPage;
    let startPage = 1;
    let endPage = totalPages;

    if (totalPages > rangeSize) {
      startPage = Math.max(currentPage - Math.floor(rangeSize / 2), 1);
      endPage = startPage + rangeSize - 1;

      if (endPage > totalPages) {
        endPage = totalPages;
        startPage = endPage - rangeSize + 1;
      }
    }

    const pages = [];
    for (let i = startPage; i <= endPage; i++) {
      pages.push(i);
    }
    return pages;
  }
}
