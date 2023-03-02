import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class ChatGPTService {

  public appId: string = '';
  public userId: string = '';

  constructor(private httpClient: HttpClient, private route: ActivatedRoute) { 
    this.route.queryParams.subscribe(params => {
      this.appId = params['appId'];
      this.userId = params['userId'];
    });
  }

  sendToGPT(message: string): Observable<any>
  { 
    let response = this.httpClient.post(`https://localhost:7280/chat?appId=${this.appId}&userId=${this.userId}&input=` + message, null,  { responseType: 'text' });
    return response;
  }
}
