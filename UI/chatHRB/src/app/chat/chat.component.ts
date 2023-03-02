import { Component } from '@angular/core';

@Component({
  templateUrl: './chat.component.html',
  selector: 'app-chat',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
  chatIsOpen = false;
  messages: any[] = [
    {
      text: 'Custom template was provided as a title!',
      date: new Date(),
      reply: false,
      user: {
        name: 'Bot',
        avatar: 'https://s3.amazonaws.com/pix.iemoji.com/images/emoji/apple/ios-12/256/robot-face.png',
      },
    },
  ];

  sendMessage(event: any) {
    this.messages.push({
      text: event.message,
      date: new Date(),
      reply: true,
      user: {
        name: 'John Doe',
        avatar: 'https://techcrunch.com/wp-content/uploads/2015/08/safe_image.gif',
      },
    });
  }


  toggleChat() {
    this.chatIsOpen = !this.chatIsOpen;
    console.log(this.chatIsOpen);
  }
}