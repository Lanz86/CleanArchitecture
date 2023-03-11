import { Component } from '@angular/core';
import { WeatherForecast, webapiclient } from '../webapiclient';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];

  constructor(private client: webapiclient) {
    client.getWeatherForecastEndpoint().subscribe({
      next: result => {
        this.forecasts = result;
      },
      error: error => console.error(error)
    });
  }
}
