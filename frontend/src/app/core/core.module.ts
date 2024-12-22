import { NgModule } from '@angular/core';
import { AuthGuard } from './guards/auth.guard';
import { AuthService } from './services/auth.service';

@NgModule({
  providers: [
    AuthService, 
    AuthGuard,
  ],
})
export class CoreModule {}
