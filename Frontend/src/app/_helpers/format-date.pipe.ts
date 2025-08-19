import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'formatDate' })
export class FormatDatePipe implements PipeTransform {
  transform(value: string): string {
    const date = new Date(value);
    return date.toLocaleString(); // format as needed
  }
}


@Pipe({ name: 'dateUtils' })
export class DateUtils implements PipeTransform {
  transform(value: string): string {
    const date = new Date(value);
    return date.toLocaleString(); // format as needed
  }
}