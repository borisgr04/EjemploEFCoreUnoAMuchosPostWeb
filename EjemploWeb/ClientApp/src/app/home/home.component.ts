import { Component } from '@angular/core';
import { DatePipe } from '@angular/common';
//Para envio de FEcha
import { Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//capturar errores
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [
    DatePipe
  ]
})
export class HomeComponent {

  fecha: any;
  formatedDate: any;
  fechaRecibida: any;
  baseUrl: any;
  constructor(private datePipe: DatePipe, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { this.baseUrl = baseUrl;}

  enviar() {
    
     /*this.http.post<string>(this.baseUrl + 'api/Factura/QueryDate', { Fecha: this.formatedDate }).subscribe(result => {
      this.fechaRecibida = result;
      }, error => console.error(error));
      */
    this.enviarPrivate().subscribe(result =>
    {
      alert("Listo");
      this.fechaRecibida = result;

    });
  }

  enviarPrivate(): Observable<QueryFacturasResponse> {
    return this.http.post<QueryFacturasResponse>(this.baseUrl + 'api/Factura/QueryDate', { Fecha: this.formatedDate }).pipe(
      tap(result => this.log(result.fecha)),
      catchError(this.handleError<QueryFacturasResponse>('envío de Fecha'))
    );
  }

  formatear() {
    this.formatedDate = this.datePipe.transform(this.fecha, "yyyy-MM-dd");
  }


  /**
* Handle Http operation that failed.
* Let the app continue.
* @param operation - name of the operation that failed
* @param result - optional value to return as the observable result
*/
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      alert(error.status); // log to console instead
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a HeroService message with the MessageService */
  private log(message: string) {
       alert(message);
    // this.messageService.add(`HeroService: ${message}`);
  }
}

interface QueryFacturasResponse
{
  fecha: string 
}

