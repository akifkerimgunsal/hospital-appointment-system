import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { LoggingService } from './logging.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlingService {
  constructor(private loggingService: LoggingService) {}

  handleError(error: HttpErrorResponse, module: string): any {
    let errorMessage = 'Bilinmeyen bir hata oluştu!';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Hata: ${error.error.message}`;
    } else {
      errorMessage = `Sunucu Hatası: ${error.status}\nMesaj: ${error.message}`;
    }
    this.loggingService.logError(errorMessage, module); // Hataları loglama
    return throwError(() => errorMessage);
  }
}
