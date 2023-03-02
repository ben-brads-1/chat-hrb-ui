import { Component } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
  chatIsOpen = false;
  messages: any[] = [
    {
      text: 'text',
      date: new Date(),
      reply: false,
      user: {
        name: 'Kyle',
        avatar: 'link-to-img',
      },
    },
  ];

  sendMessage(event) {
    this.messages.push({
      text: event.message,
      date: new Date(),
      reply: true,
      user: {
        name: 'Josh',
        avatar: 'link-to-img',
      },
    });
  }

  toggleChat() {
    this.chatIsOpen = !this.chatIsOpen;
    console.log(this.chatIsOpen);
  }
}


