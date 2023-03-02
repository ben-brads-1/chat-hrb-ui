import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatGPTService {

  constructor(private httpClient: HttpClient) { }

  sendToGPT(message: string): Observable<any>
  { 
    let response = this.httpClient.post('https://localhost:7280/chat?input=' + message, null,  { responseType: 'text' });
    return response;
  }
}
