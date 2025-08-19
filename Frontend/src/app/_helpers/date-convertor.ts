
import * as moment from 'moment-timezone';
 import { DurationInputArg2, unitOfTime } from 'moment';
import { DateTimeFormat } from '../modules/date';

export class DateUtils {

    static formatDateAndTime(date:any, format: DateTimeFormat) {
        return moment(date).format(format);
    }

    static convertTimeToLocalTimeZone(date: Date, local?: boolean): any {
    date = DateUtils.validateDate(date);
    const utcMoment = moment.utc(date);
    return moment(utcMoment).local(local);
  }

   static validateDate(d): Date {
    if (d instanceof Date) {
      return d;
    } else {
      return new Date(d);
    }
  }
}