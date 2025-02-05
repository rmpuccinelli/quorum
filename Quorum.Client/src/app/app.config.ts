import { provideHttpClient, withFetch } from '@angular/common/http';

import { ApplicationConfig } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideRouter } from '@angular/router';
import { provideToastr } from 'ngx-toastr';
import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withFetch()),
    provideAnimations(),
    provideToastr({
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      closeButton: true,
      maxOpened: 1,
      autoDismiss: true,
      progressBar: true,
      onActivateTick: true,
      timeOut: 3000,
    })
  ]
};
