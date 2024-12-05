import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { LoggingService } from './logging.service';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlingService {

  constructor(private loggingService: LoggingService) { }

  handleError(error: HttpErrorResponse, module: string): void {
    let errorMessage = 'Bilinmeyen bir hata oluştu!';
    
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Client-side hata: ${error.error.message}`;
      this.loggingService.logError(error.error.message, module);
    } else {
      // Server-side error
      errorMessage = `Server-side hata: ${error.status}\nMesaj: ${error.message}`;
      this.loggingService.logError(`${error.status} - ${error.message}`, module);
    }

    // Hata mesajını log'layarak kullanıcıya geri bildirimde bulun
    console.error(errorMessage);
    // Burada kullanıcıya bir toast, alert veya başka bir bildirim gönderebilirsin.
    alert(errorMessage);
  }
}
