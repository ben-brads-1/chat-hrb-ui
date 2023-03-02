import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatGPTService {

  constructor(private httpClient: HttpClient) { }

  sendToGPT(message: string, userId: string, appId: string): Observable<any>
  { 
    let url = "https://localhost:7280/chat?input=${message}&userId=${userId}&appId=${appId}"
    let response = this.httpClient.post('https://localhost:7280/chat?input=' + message, null,  { responseType: 'text' });
    return response;
  }
}
