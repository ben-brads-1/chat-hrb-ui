import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ChatGPTService {

  constructor(private httpClient: HttpClient) { }

  async sendToGPT(message: string)
  { 
    let response = this.httpClient.post('https://localhost:7280/chat?input=' + message, null);
    return response;
  }
}
