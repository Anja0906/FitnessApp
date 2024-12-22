import { Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '', loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule) },
  { path: 'home', loadChildren: () => import('./features/home/home.module').then(m => m.HomeModule), canActivate: [AuthGuard] },
  { path: 'workouts', loadChildren: () => import('./features/workouts/workouts.module').then(m => m.WorkoutsModule), canActivate: [AuthGuard] },
  { path: 'progress', loadChildren: () => import('./features/progress/progress.module').then(m => m.ProgressModule), canActivate: [AuthGuard] },
  { path: 'profile', loadChildren: () => import('./features/user/profile/profile.module').then(m => m.ProfileModule), canActivate: [AuthGuard] },
];
