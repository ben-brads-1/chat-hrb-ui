import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { defineCustomElements, applyPolyfills } from '@bds/bds-core/loader';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BdsNgModule } from '@bds/bds-ng';
import { FormsModule } from '@angular/forms';
import { ChatComponent } from './chat/chat.component';
import { NbChatModule, NbThemeModule } from '@nebular/theme';

@NgModule({
  declarations: [AppComponent, ChatComponent],
  imports: [BrowserModule, AppRoutingModule, BdsNgModule, FormsModule, NbThemeModule.forRoot(), NbChatModule],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {
  constructor() {
      applyPolyfills().then(() => {
      defineCustomElements(window);
    });
  }
}
