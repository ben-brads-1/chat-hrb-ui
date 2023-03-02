import { Component } from '@angular/core';
import { ChatGPTService } from '../services/chat-gpt.service';

@Component({
  templateUrl: './chat.component.html',
  selector: 'app-chat',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {

  constructor(private chatGPT: ChatGPTService) {}

  chatIsOpen = false;
  messages: any[] = [
    {
      text: 'Hello, how can I help you today!',
      date: new Date(),
      reply: false,
      user: {
        name: 'Max Refund',
        avatar: 'assets/MaxRefund.png',
      },
    },
  ];

  async sendMessage(event: any) {
    this.messages.push({
      text: event.message,
      date: new Date(),
      reply: true,
      user: {
        name: 'username',
        avatar: 'https://techcrunch.com/wp-content/uploads/2015/08/safe_image.gif',
      },
    });
    this.chatGPT.sendToGPT(event.message, '1234234', '123124').subscribe(data => {
      this.messages.push({
        text: data,
        date: new Date(),
        reply: false,
        user: {
          name: 'Max Refund',
          avatar: 'assets/MaxRefund.png',
        },
      });
    });
    

  }

  toggleChat() {
    this.chatIsOpen = !this.chatIsOpen;
    console.log(this.chatIsOpen);
  }
}