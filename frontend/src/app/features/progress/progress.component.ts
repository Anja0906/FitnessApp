import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import {
  Chart,
  BarController,
  BarElement,
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  CategoryScale,
  Title,
  Tooltip,
  Legend
} from 'chart.js';
import { WeeklyProgressResponseDto } from './models/weekly-progress.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WorkoutService } from '../workouts/services/workout.service';

Chart.register(
  BarController,
  BarElement,
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  CategoryScale,
  Title,
  Tooltip,
  Legend
);


@Component({
  selector: 'app-progress',
  templateUrl: './progress.component.html',
  styleUrls: ['./progress.component.css'],
  imports:[CommonModule, FormsModule]
})
export class ProgressComponent implements OnInit {
  totalDurationChartInstance!: Chart;
  workoutCountChartInstance!: Chart;
  averageIntensityChartInstance!: Chart;
  averageFatigueChartInstance!: Chart;

  @ViewChild('totalDurationChart', { static: true }) totalDurationChart!: ElementRef<HTMLCanvasElement>;
  @ViewChild('workoutCountChart', { static: true }) workoutCountChart!: ElementRef<HTMLCanvasElement>;
  @ViewChild('averageIntensityChart', { static: true }) averageIntensityChart!: ElementRef<HTMLCanvasElement>;
  @ViewChild('averageFatigueChart', { static: true }) averageFatigueChart!: ElementRef<HTMLCanvasElement>;

  months: string[] = [
    'January', 'February', 'March', 'April', 'May', 'June',
    'July', 'August', 'September', 'October', 'November', 'December'
  ];
  years: number[] = Array.from({ length: 10 }, (_, i) => new Date().getFullYear() - i);
  selectedMonth: number = new Date().getMonth() + 1; 
  selectedYear: number = new Date().getFullYear(); 
  weeklyProgress : WeeklyProgressResponseDto[] = [];
  userId!: number;

  constructor(private workoutService: WorkoutService){}
  ngOnInit(): void {
    const storedUserId = localStorage.getItem('userId');
    if (storedUserId) {
      this.userId = parseInt(storedUserId, 10); 
    } else {
      console.error('Korisnički ID nije pronađen u localStorage!');
      return; 
    }
    this.fetchMonthlyProgress(this.userId, this.selectedYear, this.selectedMonth);
  }

  onDateChange(): void {
    this.fetchMonthlyProgress(this.userId, this.selectedYear, this.selectedMonth);
  }

  fetchMonthlyProgress(userId: number, year: number, month: number): void {
    this.workoutService.getMonthlyProgress(userId, year, month).subscribe({
      next: (response) => {
        this.weeklyProgress = response;
        console.log('Monthly Progress:', response);
        this.createTotalDurationChart();
        this.createWorkoutCountChart();
        this.createAverageIntensityChart();
        this.createAverageFatigueChart();
      },
      error: (error) => {
        console.error('Error fetching progress:', error);
      },
    });
  }

  createTotalDurationChart(): void {
    if (this.totalDurationChartInstance) {
      this.totalDurationChartInstance.destroy(); 
    }
  
    const labels = this.weeklyProgress.map(p => `Week ${p.week}`);
    const data = this.weeklyProgress.map(p => p.totalDuration);

    this.totalDurationChartInstance = new Chart(this.totalDurationChart.nativeElement, {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [
          {
            label: 'Total Duration (min)',
            data: data,
            backgroundColor: '#42A5F5'
          }
        ]
      },
      options: {
        responsive: true,
        plugins: {
          legend: { display: true }
        }
      }
    });
  }

  createWorkoutCountChart(): void {
    if (this.workoutCountChartInstance) {
      this.workoutCountChartInstance.destroy(); 
    }
    const labels = this.weeklyProgress.map(p => `Week ${p.week}`);
    const data = this.weeklyProgress.map(p => p.workoutCount);

    this.workoutCountChartInstance = new Chart(this.workoutCountChart.nativeElement, {
      type: 'line',
      data: {
        labels: labels,
        datasets: [
          {
            label: 'Workout Count',
            data: data,
            borderColor: '#FFA726',
            fill: false
          }
        ]
      },
      options: {
        responsive: true,
        plugins: {
          legend: { display: true }
        }
      }
    });
  }

  createAverageIntensityChart(): void {
    if(this.averageIntensityChartInstance){
      this.averageIntensityChartInstance.destroy();
    }
    const labels = this.weeklyProgress.map(p => `Week ${p.week}`);
    const data = this.weeklyProgress.map(p => p.averageIntensity);

    this.averageIntensityChartInstance = new Chart(this.averageIntensityChart.nativeElement, {
      type: 'line',
      data: {
        labels: labels,
        datasets: [
          {
            label: 'Average Intensity',
            data: data,
            borderColor: '#66BB6A',
            fill: false
          }
        ]
      },
      options: {
        responsive: true,
        plugins: {
          legend: { display: true }
        }
      }
    });
  }

  createAverageFatigueChart(): void {
    if(this.averageFatigueChartInstance){
      this.averageFatigueChartInstance.destroy();
    }

    const labels = this.weeklyProgress.map(p => `Week ${p.week}`);
    const data = this.weeklyProgress.map(p => p.averageFatigueLevel);

    this.averageFatigueChartInstance = new Chart(this.averageFatigueChart.nativeElement, {
      type: 'line',
      data: {
        labels: labels,
        datasets: [
          {
            label: 'Average Fatigue',
            data: data,
            borderColor: '#EF5350',
            fill: false
          }
        ]
      },
      options: {
        responsive: true,
        plugins: {
          legend: { display: true }
        }
      }
    });
  }
}
