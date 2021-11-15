import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  constructor() { }

  addError(error: string): void {
    Swal.fire({
      position: 'center',
      icon: 'error',
      title: error,
      showConfirmButton: false,
    });
  }

  addMessage(message: string): void {
    Swal.fire({
      position: 'center',
      icon: 'success',
      title: message,
      showConfirmButton: false,
      timer: 1200
    });
  }
}
